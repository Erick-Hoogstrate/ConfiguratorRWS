using UnityEngine;
using UnityEditor;
using System.Reflection;
using u040.prespective.core.editor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using u040.prespective.utility.editor;
using System;
using UnityEngine.EventSystems;

namespace u040.prespective.prepair.inspector.editor
{
    [CustomEditor(typeof(ControlPanelInterfaceUIE))]
    public class ControlPanelInterfaceEditorUIE : PrespectiveEditorUIE<ControlPanelInterfaceUIE>
    {
        TextField title;
        VisualElement settingsContainer;
        Label settingsLabel;
        Label noSettingsDisplay;

        Foldout controlPanelProperties;
        IMGUIContainer warningContainer;
        ObjectField gameObjectField;
        VisualElement componentContainer;
        PopupField<string> componentField;
        Label noSuitableComponents;

        private List<Component> suitableComponents = new List<Component>();
        private IControlPanelUIE controlPanel;
        private int componentSelectionIndex = 0;
        private readonly string SETTINGS_TEXT = "settings";

        private readonly string NO_BOX_CLASS_LIST = "no-box";

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("ControlPanelInterfaceLayout");
            theme = "standard-components";
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            #region << Control Panel >>

            title = root.Q<TextField>(name: "title");
            title.isDelayed = true;
            controlPanelProperties = root.Q<Foldout>(name: "target-component-foldout");
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            gameObjectField = root.Q<ObjectField>(name: "target-gameobject");
            componentContainer = root.Q<VisualElement>(name: "target-component");
            noSuitableComponents = root.Q<Label>(name: "no-suitable-components");

            title.tooltip = "Click once to ping target component, click twice to edit title";
            title.RegisterCallback<MouseDownEvent>(_mouseEvent =>
            {
                if (component.SelectedComponent == null)
                {
                    Debug.LogError("No component has been set.");
                }
                else 
                {
                    if (_mouseEvent.clickCount == 1)
                    {
                        EditorGUIUtility.PingObject(component.SelectedComponent);
                    }
                    if (_mouseEvent.clickCount > 1)
                    {

                        title.RemoveFromClassList(NO_BOX_CLASS_LIST);
                        title.isReadOnly = false;
                    }
                }
            });

            title.RegisterCallback<FocusOutEvent>(_focusOutEvent =>
            {
                title.AddToClassList(NO_BOX_CLASS_LIST);
                title.isReadOnly = true;
            });

            UIUtility.InitializeField
            (
                title,
                () => component.Title,
                e =>
                {
                    component.Title = e.newValue;
                    title.AddToClassList(NO_BOX_CLASS_LIST);
                    title.isReadOnly = true;
                }
            );

            if (component.SelectedComponent != null)
            {
                controlPanelProperties.value = false;
            }

            ;

            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                gameObjectField,
                () => component.SelectedGameObject,
                e =>
                {
                    component.SelectedGameObject = (GameObject) e.newValue;
                    initializeComponentPopup();
                    drawSettings();
                },

                typeof(GameObject)
            );

            #endregion

            #region << Properties >>
            settingsContainer = root.Q<VisualElement>(name: "settings-container");
            settingsLabel = root.Q<Label>(name: "settings-label");
            noSettingsDisplay = root.Q<Label>(name: "no-settings-display");
            UIUtility.SetDisplay(noSettingsDisplay, component.SelectedComponent == null);
            #endregion

            initializeComponentPopup();
            drawSettings();
        }

        private void initializeComponentPopup()
        {
            componentContainer.Clear();

            if (component.SelectedGameObject == null)
            {
                component.SelectedComponent = null;
                settingsLabel.text = SETTINGS_TEXT;
                title.value = "No component has been set.";
            }
            else
            {
                suitableComponents = listSuitableComponents();
                if (suitableComponents.Count > 0)
                {
                    if (component.SelectedComponent != null)
                    {
                        if (suitableComponents.Contains(component.SelectedComponent))
                        {
                            componentSelectionIndex = (suitableComponents.FindIndex(_e => _e == component.SelectedComponent));
                        }
                    }

                    componentField = new PopupField<string>("Target Component", suitableComponents.ConvertAll<string>(_e => _e.GetType().Name), componentSelectionIndex);
                    componentContainer.Add(componentField);

                    componentField.RegisterValueChangedCallback(_changeEvent =>
                    {
                        setSelectedComponent(_changeEvent.newValue);
                        drawSettings();
                    });

                    setSelectedComponent(componentField.value);
                }

                UIUtility.SetDisplay(noSuitableComponents, suitableComponents.Count == 0);
            }
        }

        private void setSelectedComponent(string _typeName)
        {
            if (_typeName == "")
            {
                component.SelectedComponent = null;
                settingsLabel.text = SETTINGS_TEXT;
                title.value = "";
            }
            else
            {
                component.SelectedComponent = suitableComponents[suitableComponents.FindIndex(_e => _e.GetType().Name == _typeName)];
                settingsLabel.text = component.SelectedComponent.GetType().Name + " " + SETTINGS_TEXT;
                title.value = component.Title;
            }
            //controlPanel = null;
        }

        private void drawSettings()
        {
            settingsContainer.Clear();
            if (component.SelectedComponent != null)
            {
                UIUtility.SetDisplay(noSettingsDisplay, component.SelectedComponent == null);
                controlPanel = (IControlPanelUIE)Editor.CreateEditor(component.SelectedComponent);
                controlPanel.ShowControlPanelProperties(settingsContainer);
            }
        }

        private List<Component> listSuitableComponents()
        {
            //Clear Previous Data
            suitableComponents.Clear();

            //if no gameobject selected, return.
            if (component.SelectedGameObject == null)
            {
                return null;
            }

            //get all components from the Selected GameObject
            Component[] allComponents = component.SelectedGameObject.GetComponents<Component>();

            List<Component> componentList = new List<Component>();

            //for each component found check if its editor inherits from icontrolpanel. If it does add it to the list.
            foreach (Component component in allComponents)
            {
                //create the editor for the component
                Editor editor = Editor.CreateEditor(component);

                //check if editor is IControlpanel
                if (editor is IControlPanelUIE)
                {
                    componentList.Add(component);
                }
            }

            return componentList;
        }

        public override void OnInspectorGUI()
        {
            if (component.SelectedComponent == null)
            {
                if (component.SelectedGameObject == null)
                {
                    //show warning when no Game object is assigned
                    EditorGUILayout.HelpBox("No game object assigned", MessageType.Error);
                }
                else
                {
                    //show warning when no component is assigned
                    EditorGUILayout.HelpBox("No component assigned", MessageType.Error);
                }
            }
        }
    }
}
