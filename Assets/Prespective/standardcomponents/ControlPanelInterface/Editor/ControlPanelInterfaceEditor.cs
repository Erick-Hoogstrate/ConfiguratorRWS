#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using u040.prespective.core.editor;
using System.Collections.Generic;

namespace u040.prespective.prepair.inspector.editor
{
    [CustomEditor(typeof(ControlPanelInterface))]
    public class ControlPanelInterfaceEditor : PrespectiveEditor
    {
        private ControlPanelInterface component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;

        private int componentSelectionIndex = 0;
        private bool editTitle = false;

        private List<Component> suitableComponents = new List<Component>();
        private List<IControlPanel> controlPanels = new List<IControlPanel>();
        private IControlPanel controlPanel;

        internal void OnEnable()
        {
            if (target == null) { return; }
            component = (ControlPanelInterface)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
            //if (component.SelectedGameObject != null && component.ControlPanel == null)
            //{
            //    component.UpdateControlPanel();
            //}
        }

        public override void OnInspectorGUI()
        {
            checkForLicense();

            soTarget.Update();
            //DrawDefaultInspector();


            EditorGUI.BeginChangeCheck();

            //Set widths for title
            float inspectorWidth = Screen.width;
            float buttonWidth = 45f;
            float indentWidth = 30f;

            //Draw Title
            EditorGUILayout.BeginHorizontal();
            if (editTitle)
            {
                //Draw title edit field
                component.Title = ReturnTextField(component.Title, () => this.editTitle = false, GUILayout.Width(inspectorWidth - buttonWidth - indentWidth));
            }

            //Draw button to ping the selected component
            else if (GUILayout.Button(component.Title, new GUIStyle(EditorStyles.boldLabel) { fontSize = 14 }, GUILayout.Width(inspectorWidth - buttonWidth - indentWidth)))
            {
                EditorGUIUtility.PingObject(component.SelectedComponent);
            }

            //Draw edit button
            if (GUILayout.Button((this.editTitle ? "Apply" : "Edit"), GUILayout.Width(buttonWidth)))
            {
                this.editTitle = !this.editTitle;
            }
            EditorGUILayout.EndHorizontal();

            //Draw toolbar
            toolbarTab.intValue = GUILayout.Toolbar(toolbarTab.intValue, new string[] { "Control Panel", "Properties" });

            //Draw tabs
            switch (toolbarTab.intValue)
            {
                case 0: //Control Panel
                    //Warning to apply component
                    if (component.SelectedGameObject != null && component.SelectedComponent == null)
                    {
                        EditorGUILayout.HelpBox("A GameObject has been selected but no component has been applied. No Control Panel can be shown until a component has been applied.", MessageType.Warning);
                    }

                    //Draw control panel
                    drawControlPanel();
                    break;

                case 1: //Properties

                    EditorGUI.BeginDisabledGroup(Application.isPlaying); //Disable editting properties during play mode

                    //If no component selected
                    if (component.SelectedComponent == null)
                    {
                        //Select game object
                        component.SelectedGameObject = (GameObject)EditorGUILayout.ObjectField("GameObject", component.SelectedGameObject, typeof(GameObject), true);

                        //If gameobject selected
                        if (component.SelectedGameObject != null)
                        {
                            //Select a component
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Selected Control Panel");

                            //Get all suitable components from selected gameobject
                            getSuitableComponents();

                            //If there are any suitable components
                            if (suitableComponents.Count > 0)
                            {
                                //Draw dropdown list of all component names
                                componentSelectionIndex = EditorGUILayout.Popup(componentSelectionIndex, suitableComponents.ConvertAll<string>(_e => _e.GetType().Name).ToArray());

                                //Draw apply button
                                if (GUILayout.Button("Apply"))
                                {
                                    //Set selected components
                                    component.SelectedComponent = suitableComponents[componentSelectionIndex];
                                    controlPanel = null;
                                }
                            }

                            //if no suitable components available
                            else
                            {
                                //Draw default dropdown with placeholder text
                                EditorGUILayout.Popup(0, new string[] { "No suitable components" });
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    //If a selected components is set
                    else
                    {
                        //Show component
                        EditorGUILayout.ObjectField("Selected Component", component.SelectedComponent, typeof(Component), false);
                    }

                    //If a gameobject of componet was set
                    if (component.SelectedGameObject != null || component.SelectedComponent != null)
                    {
                        //Draw clear button
                        if (GUILayout.Button("Clear"))
                        {
                            component.Clear();
                        }
                    }
                    EditorGUI.EndDisabledGroup();
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties();
            }
            base.OnInspectorGUI();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        /// <summary>
        /// Draw an inspector button to generate a Control Panel for a given component
        /// </summary>
        /// <param name="_component"></param>
        public static void ShowGenerationButtonForComponent(Component _component)
        {
            if (Application.isEditor)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Generate Control Panel for " + _component.GetType().Name))
                {
                    ControlPanelInterface.CreateForComponent(_component);
                }
                EditorGUILayout.Space();
            }
        }


        /// <summary>
        /// Draw the control panel for the selected component
        /// </summary>
        /// <returns></returns>
        private bool drawControlPanel()
        {
            //Return if no component selected
            if (component.SelectedComponent == null)
            {
                EditorGUILayout.LabelField("No properties can be shown while no Component has been selected.", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic });
                return false;
            }

            //If no control panel was set yet
            if (controlPanel == null)
            {
                //get control panel for component
                controlPanel = (IControlPanel)Editor.CreateEditor(component.SelectedComponent);
            }

            controlPanel.ShowControlPanel();

            //Draw control panel
            return true;
        }

        /// <summary>
        /// Get all suitable components from selected gameobject
        /// </summary>
        private void getSuitableComponents()
        {
            //Clear previous data
            this.suitableComponents.Clear();
            this.controlPanels.Clear();

            //If no gameobject selected, return
            if (component.SelectedGameObject == null) { return; }

            //Define lists
            List<Component> components = new List<Component>();
            List<IControlPanel> controlPanels = new List<IControlPanel>();

            //Get all components from selected gameobject
            Component[] allComponents = component.SelectedGameObject.GetComponents<Component>();

            //For each component
            foreach (Component component in allComponents)
            {
                //Get the editor script
                Editor editor = Editor.CreateEditor(component);

                //If editor is IControlPanel
                if (editor is IControlPanel)
                {
                    //Add component and control panel to list
                    components.Add(component);
                    controlPanels.Add((IControlPanel)editor);
                }
            }

            //Set values
            this.suitableComponents = components;
            this.controlPanels = controlPanels;
        }
    }
}
#endif
