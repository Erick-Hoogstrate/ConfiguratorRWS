using u040.prespective.standardcomponents.virtualhardware.systems.gripper;
using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.standardcomponents.virtualhardware.systems.gripper.DGripperBase;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.gripper
{
    [CustomEditor(typeof(DGripperBase))]
    public class DGripperBaseEditorUIE : StandardComponentEditorUIE<DGripperBase>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField stateField;
        private VisualElement stateTextInput;
        private TextField currentClosePercentageField;
        private Label grippedObjectsLabel;
        private Label noGrippedObjectsLabel;
        private VisualElement gripperObjectsContainer;

        //Property Fields
        private DoubleField closeTimeField;
        private ObjectField addFingerField;
        private Label fingersLabel;
        private Label noFingersLabel;
        private VisualElement fingersContainer;

        //Values
        private string grippedObjectsText = "Gripped Objects";
        private string fingersLabelText = "Fingers";
        private VisualTreeAsset fingerVisualTree;
        #endregion
        #region << Control Panel Properties >>
        private TextField stateControlPanel;
        private TextField currentClosePercentageControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DGripperBaseEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            fingerVisualTree = Resources.Load<VisualTreeAsset>("DGripperFingerLayout");
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            stateField.value = component.State.ToString();
            switch (component.State)
            {
                case DGripperBase.GripperState.Open:
                    stateTextInput.style.color = new Color(0f, 0.5f, 0f);
                    break;
                case DGripperBase.GripperState.Closed:
                    stateTextInput.style.color = Color.red;
                    break;
                default:
                    stateTextInput.style.color = new Color(0.9f, 0.5f, 0f);
                    break;
            }
            currentClosePercentageField.value = component.ClosePercentage.ToString();
            grippedObjectsLabel.text = grippedObjectsText + " (" + component.GrippedGameObjects.Count + ")";
            UpdateGripperContainer();
        }

        private void UpdateGripperContainer()
        {
            if (component.GrippedGameObjects.Count == 0)
            {
                UIUtility.SetDisplay(noGrippedObjectsLabel, true);
                UIUtility.SetDisplay(gripperObjectsContainer, false);
            }
            else
            {
                UIUtility.SetDisplay(noGrippedObjectsLabel, false);
                UIUtility.SetDisplay(gripperObjectsContainer, true);
                gripperObjectsContainer.Clear();
                gripperObjectsContainer.Add(UIUtility.CreateObjectLocatorFields(component.GrippedGameObjects));
            }
        }

        protected override void initialize()
        {
            base.initialize();
            root.styleSheets.Add(Resources.Load<StyleSheet>("GripperBaseEditorStyle"));

            #region << Live Data >>
            stateField = root.Q<TextField>(name: "state");
            stateField.isReadOnly = true;
            stateTextInput = stateField.Q<VisualElement>(name: "unity-text-input");

            currentClosePercentageField = root.Q<TextField>(name: "current-close-percentage");
            currentClosePercentageField.isReadOnly = true;

            grippedObjectsLabel = root.Q<Label>(name: "gripped-objects-label");
            noGrippedObjectsLabel = root.Q<Label>(name: "no-gripped-objects");

            gripperObjectsContainer = root.Q<VisualElement>(name: "gripped-objects-container");
            #endregion
            #region << Properties >>
            closeTimeField = root.Q<DoubleField>(name: "close-time");
            addFingerField = root.Q<ObjectField>(name: "add-finger");
            fingersLabel = root.Q<Label>(name: "fingers-label");
            noFingersLabel = root.Q<Label>(name: "no-fingers");
            fingersContainer = root.Q<VisualElement>(name: "fingers-container");

            UIUtility.InitializeField
            (
                closeTimeField,
                component,
                () => component.CloseTime,
                e =>
                {
                    component.CloseTime = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                addFingerField,
                component,
                () => null,
                e =>
                {
                    if (addFingerField.value != null)
                    {
                        bool createNewFinger;
                        createNewFinger = component.AddFinger((DGripperFinger)e.newValue);
                        if (createNewFinger)
                        {
                            AddFingerSettings((DGripperFinger)addFingerField.value);
                        }
                        addFingerField.SetValueWithoutNotify(null);
                    }
                    UpdateFingerLabelDisplay();
                },
                typeof(DGripperFinger)
            );

            if (component.GripperFingers.Count > 0)
            {
                for (int _i = 0; _i < component.GripperFingers.Count; _i++)
                {
                    AddFingerSettings(component.GripperFingers[_i].Finger);
                }
            }

            UpdateFingerLabelDisplay();
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                properties.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };
        }

        private void UpdateFingerLabelDisplay()
        {
            fingersLabel.text = fingersLabelText + " (" + component.GripperFingers.Count + ")";
            UIUtility.SetDisplay(noFingersLabel, component.GripperFingers.Count == 0);
            UIUtility.SetDisplay(fingersContainer, component.GripperFingers.Count > 0);
        }

        private void AddFingerSettings(DGripperFinger _finger)
        {
            DGripperBase.DFingerSetting fingerSetting = component.GripperFingers.Find((_entry) => _entry.Finger);

            VisualElement fingerSettings = fingerVisualTree.CloneTree();
            fingersContainer.Add(fingerSettings);

            ObjectField fingerField = fingerSettings.Q<ObjectField>(name: "finger");
            Toggle invertToggle = fingerSettings.Q<Toggle>(name: "invert");
            Button removeButton = fingerSettings.Q<Button>(name: "remove");

            fingerField.objectType = typeof(DGripperFinger);
            fingerField.value = _finger;
            fingerField.Q<VisualElement>(className: "unity-object-field__selector").RemoveFromHierarchy();
            fingerField.allowSceneObjects = false;

            UIUtility.InitializeField
            (
                invertToggle,
                component,
                () => fingerSetting.Inverted,
                e =>
                {
                    fingerSetting.Inverted = e.newValue;
                }
            );

            removeButton.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                RemoveFinger(_finger, fingerSettings);
                UpdateFingerLabelDisplay();
            });
        }

        private void RemoveFinger(DGripperFinger _finger, VisualElement _fingerSettings)
        {
            component.RemoveFinger(_finger);
            _fingerSettings.parent.Remove(_fingerSettings);
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.SetReadOnlyState(stateControlPanel, true);

            currentClosePercentageControlPanel = new TextField("Current Close Percentage");
            UIUtility.SetReadOnlyState(currentClosePercentageControlPanel, true);

            Button openCloseGripper = new Button();
            openCloseGripper.text = component.TargetClosePercentage == 1f ? "Open" : "Close";
            openCloseGripper.AddToClassList("content-fit-button");
            openCloseGripper.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.TargetClosePercentage = (component.TargetClosePercentage + 1f) % 2f;
                openCloseGripper.text = component.TargetClosePercentage == 1f ? "Open" : "Close";
            });

            scheduleControlPanelUpdate(stateControlPanel);

            _container.Add(stateControlPanel);
            _container.Add(currentClosePercentageControlPanel);
            _container.Add(openCloseGripper);
        }

        protected override void updateControlPanelData()
        {
            Color stateColor;
            switch (component.State)
            {
                case GripperState.Open:
                    //Green
                    stateColor = new Color(0f, 0.5f, 0f);
                    break;

                case GripperState.Closed:
                    //Red
                    stateColor = Color.red;
                    break;

                default:
                    //Orange
                    stateColor = new Color(0.9f, 0.5f, 0f);
                    break;
            }
            currentClosePercentageControlPanel.value = component.ClosePercentage.ToString();
            stateControlPanel.value = component.State.ToString();
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = stateColor;
        }
    }
}
