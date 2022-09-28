using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.standardcomponents.materialhandling.gripper.GripperBase;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    
    [CustomEditor(typeof(GripperBase))]
    public class GripperBaseEditorUIE : StandardComponentEditorUIE<GripperBase>
    {
        #region << Live Data Fields >>
        TextField state;
        VisualElement stateTextInput;
        TextField currentClosePercentage;
        Label grippedObjectsLabel;
        Label noGrippedObjects;
        VisualElement gripperObjectsContainer;
        #endregion
        #region << Property Fields>>
        FloatField closeTime;
        ObjectField addFinger;
        Label fingersLabel;
        Label noFingersLabel;
        VisualElement fingersContainer;
        #endregion
        #region << Values >>
        string grippedObjectsText = "Gripped Objects";
        string fingersLabelText = "Fingers";
        VisualTreeAsset fingerVisualTree;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField currentClosePercentageControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/GripperBaseLayout");
            fingerVisualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/GripperFingerLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.State.ToString();
            switch (component.State)
            {
                case GripperBase.GripperState.Open:
                    stateTextInput.style.color = new Color(0f, 0.5f, 0f);
                    break;
                case GripperBase.GripperState.Closed:
                    stateTextInput.style.color = Color.red;
                    break;
                default:
                    stateTextInput.style.color = new Color(0.9f, 0.5f, 0f);
                    break;
            }
            currentClosePercentage.value = component.ClosePercentage.ToString();
            grippedObjectsLabel.text = grippedObjectsText + " (" + component.GrippedGameObjects.Count + ")";
            UpdateGripperContainer();
        }

        private void UpdateGripperContainer()
        {
            if (component.GrippedGameObjects.Count == 0)
            {
                UIUtility.SetDisplay(noGrippedObjects, true);
                UIUtility.SetDisplay(gripperObjectsContainer, false);
            }
            else
            {
                UIUtility.SetDisplay(noGrippedObjects, false);
                UIUtility.SetDisplay(gripperObjectsContainer, true);
                gripperObjectsContainer.Clear();
                gripperObjectsContainer.Add(UIUtility.CreateObjectLocatorFields(component.GrippedGameObjects));
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            root.styleSheets.Add(Resources.Load<StyleSheet>("MaterialHandling/Gripper/GripperBaseStyleSheet"));

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;
            stateTextInput = state.Q<VisualElement>(name: "unity-text-input");

            currentClosePercentage = root.Q<TextField>(name: "current-close-percentage");
            currentClosePercentage.isReadOnly = true;

            grippedObjectsLabel = root.Q<Label>(name: "gripped-objects-label");
            noGrippedObjects = root.Q<Label>(name: "no-gripped-objects");

            gripperObjectsContainer = root.Q<VisualElement>(name: "gripped-objects-container");
            #endregion
            #region << Properties >>
            closeTime = root.Q<FloatField>(name: "close-time");
            addFinger = root.Q<ObjectField>(name: "add-finger");
            fingersLabel = root.Q<Label>(name: "fingers-label");
            noFingersLabel = root.Q<Label>(name: "no-fingers");
            fingersContainer = root.Q<VisualElement>(name: "fingers-container");

            UIUtility.InitializeField
            (
                closeTime,
                () => component.CloseTime,
                e =>
                {
                    component.CloseTime = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                addFinger,
                () => null,
                e =>
                {
                    if (addFinger.value != null)
                    {
                        bool createNewFinger;
                        createNewFinger = component.AddFinger((GripperFinger)e.newValue);
                        if (createNewFinger)
                        {
                            AddFingerSettings((GripperFinger)addFinger.value);
                        }
                        addFinger.SetValueWithoutNotify(null);
                    }
                    UpdateFingerLabelDisplay();
                },
                typeof(GripperFinger)
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
        }

        private void UpdateFingerLabelDisplay()
        {
            fingersLabel.text = fingersLabelText + " (" + component.GripperFingers.Count + ")";
            UIUtility.SetDisplay(noFingersLabel, component.GripperFingers.Count == 0);
            UIUtility.SetDisplay(fingersContainer, component.GripperFingers.Count > 0);
        }

        private void AddFingerSettings(GripperFinger _finger)
        {
            GripperBase.FingerSetting fingerSetting = component.GripperFingers.Find((_entry) => _entry.Finger == _finger);

            VisualElement fingerSettings = fingerVisualTree.CloneTree();
            fingersContainer.Add(fingerSettings);

            ObjectField finger = fingerSettings.Q<ObjectField>(name: "finger");
            Toggle invert = fingerSettings.Q<Toggle>(name: "invert");
            Button remove = fingerSettings.Q<Button>(name: "remove");

            finger.objectType = typeof(GripperFinger);
            finger.value = _finger;
            finger.Q<VisualElement>(className: "unity-object-field__selector").RemoveFromHierarchy();
            finger.allowSceneObjects = false;

            UIUtility.InitializeField
            (
                invert,
                () => fingerSetting.Inverted,
                e =>
                {
                    fingerSetting.Inverted = e.newValue;
                }
            );

            remove.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                RemoveFinger(_finger, fingerSettings);
                UpdateFingerLabelDisplay();
            });
        }

        private void RemoveFinger(GripperFinger _finger, VisualElement _fingerSettings)
        {
            component.RemoveFinger(_finger);
            _fingerSettings.parent.Remove(_fingerSettings);
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.ToggleNoBoxAndReadOnly(stateControlPanel, true);
            UIUtility.InitializeField
            (
                stateControlPanel,
                () => 
                {
                    updateStateFieldColor();
                    return component.State.ToString();
                },
                100
            );

            currentClosePercentageControlPanel = new TextField("Current Close Percentage");
            UIUtility.ToggleNoBoxAndReadOnly(currentClosePercentageControlPanel, true);
            UIUtility.InitializeField
            (
                currentClosePercentageControlPanel,
                () => component.ClosePercentage.ToString(),
                100
            );

            Button openCloseGripper = new Button();
            openCloseGripper.text = component.TargetClosePercentage == 1f ? "Open" : "Close";
            openCloseGripper.AddToClassList("content-fit-button");
            openCloseGripper.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.TargetClosePercentage = (component.TargetClosePercentage + 1f) % 2f;
                openCloseGripper.text = component.TargetClosePercentage == 1f ? "Open" : "Close";
            });

            _container.Add(stateControlPanel);
            _container.Add(currentClosePercentageControlPanel);
            _container.Add(openCloseGripper);
        }

        private void updateStateFieldColor()
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
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = stateColor;
        }
    }
}
