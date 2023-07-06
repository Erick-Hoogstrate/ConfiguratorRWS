using System.Collections.Generic;
using u040.prespective.core.editor.editorui;
using u040.prespective.core.editor.editorui.inspectorwindow;
using u040.prespective.standardcomponents.userinterface;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.uistatepersistence;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.userinterface
{
    [CustomEditor(typeof(ControlPanelInterfaceUIE))]
    public class ControlPanelInterfaceEditorUIE : PrespectiveEditorUIE<ControlPanelInterfaceUIE>
    {
        #region << FIELDS >>
        private TextField titleField;
        private VisualElement settingsContainer;
        private Label settingsLabel;
        private Label noSettingsDisplay;

        private Foldout targetComponentFoldout;
        private IMGUIContainer warningContainer;
        private ObjectField gameObjectField;
        private VisualElement componentContainer;
        private PopupField<string> componentField;
        private Label noSuitableComponentsMessage;

        private List<Component> suitableComponents = new List<Component>();
        private IControlPanelUIE controlPanel;
        private int componentSelectionIndex = 0;
        private readonly string SETTINGS_TEXT = "Settings";

        private readonly string NO_BOX_CLASS_LIST = "no-box";

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ControlPanelInterfaceEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            theme = VisualTheme.StandardComponents;
        }

        protected override void initialize()
        {
            #region << Control Panel >>

            titleField = root.Q<TextField>(name: "title");
            titleField.isDelayed = true;
            titleField.tooltip = "Click once to ping target component, click twice to edit title";
            titleField.RegisterCallback<MouseDownEvent>(_mouseEvent =>
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

                        titleField.RemoveFromClassList(NO_BOX_CLASS_LIST);
                        titleField.isReadOnly = false;
                    }
                }
            });

            titleField.RegisterCallback<FocusOutEvent>(_focusOutEvent =>
            {
                titleField.AddToClassList(NO_BOX_CLASS_LIST);
                titleField.isReadOnly = true;
            });

            UIUtility.InitializeField
            (
                titleField,
                component,
                () => component.Title,
                _e =>
                {
                    component.Title = _e.newValue;
                    titleField.AddToClassList(NO_BOX_CLASS_LIST);
                    titleField.isReadOnly = true;
                }
            );

            #region << Properties >>
            settingsContainer = root.Q<VisualElement>(name: "settings-container");
            settingsLabel = root.Q<Label>(name: "settings-label");
            noSettingsDisplay = root.Q<Label>(name: "no-settings-display");
            UIUtility.SetDisplay(noSettingsDisplay, component.SelectedComponent == null);
            #endregion

            targetComponentFoldout = root.Q<Foldout>(name: "target-component-foldout");
            UIStateUtility.InitTrackedFoldout(targetComponentFoldout, component);

            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            warningContainer.onGUIHandler = OnInspectorGUI;

            gameObjectField = root.Q<ObjectField>(name: "target-gameobject");
            componentContainer = root.Q<VisualElement>(name: "target-component");
            noSuitableComponentsMessage = root.Q<Label>(name: "no-suitable-components");

            UIUtility.InitializeField
            (
                gameObjectField,
                component,
                () => component.SelectedGameObject,
                _e =>
                {
                    component.SelectedGameObject = (GameObject) _e.newValue;
                    component.SelectedComponent = null;
                    initializeComponentPopup();
                    drawSettings();
                },

                typeof(GameObject)
            );

            #endregion

            initializeComponentPopup();
            drawSettings();
        }

        private void initializeComponentPopup()
        {
            UIUtility.SetDisplay(noSuitableComponentsMessage, component.SelectedGameObject != null);

            componentContainer.Clear();

            if (component.SelectedGameObject == null)
            {
                component.SelectedComponent = null;
                settingsLabel.text = SETTINGS_TEXT;
                titleField.value = "No component has been set.";
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

                UIUtility.SetDisplay(noSuitableComponentsMessage, suitableComponents.Count == 0);
            }
        }

        private void setSelectedComponent(string _typeName)
        {
            if (_typeName == "")
            {
                component.SelectedComponent = null;
                settingsLabel.text = SETTINGS_TEXT;
                titleField.value = "";
            }
            else
            {
                component.SelectedComponent = suitableComponents[suitableComponents.FindIndex(_e => _e.GetType().Name == _typeName)];
                settingsLabel.text = component.SelectedComponent.GetType().Name + " " + SETTINGS_TEXT;
                titleField.value = component.Title;
            }
        }

        private void drawSettings()
        {
            settingsContainer.Clear();

            if (component.SelectedComponent == null)
            {
                //Display No Settings when no component is selected
                UIUtility.SetDisplay(noSettingsDisplay, true);
                return;
            }

            UIUtility.SetDisplay(noSettingsDisplay, false);
            Editor editor = CreateEditor(component.SelectedComponent);
            controlPanel = (IControlPanelUIE)editor;
            controlPanel.ShowControlPanelProperties(settingsContainer);
            DestroyImmediate(editor);
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
                if (component.GetType() == typeof(ControlPanelInterfaceUIE))
                {
                    continue;
                }

                //create the editor for the component
                Editor editor = CreateEditor(component);

                //check if editor is IControlpanel
                if (editor is IControlPanelUIE)
                {
                    componentList.Add(component);
                }

                DestroyImmediate(editor);
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
                    EditorGUILayout.HelpBox("No Game Object assigned.", MessageType.Error);
                }
                else
                {
                    //show warning when no component is assigned
                    EditorGUILayout.HelpBox("No Component assigned.", MessageType.Error);
                }
            }
        }
    }
}
