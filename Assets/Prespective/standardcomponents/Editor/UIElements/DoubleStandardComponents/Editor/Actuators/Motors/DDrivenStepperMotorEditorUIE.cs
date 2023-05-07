using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.motors
{
    [CustomEditor(typeof(DDrivenStepperMotor))]
    public class DDrivenStepperMotorEditorUIE : StandardComponentEditorUIE<DDrivenStepperMotor>
    {
        #region << FIELDS >>
        private TextField preferredVelocity;
        private TextField targetStep;
        private TextField continuous;
        private TextField continuousDirection;
        private TextField velocity;
        private TextField position;
        private TextField state;

        // Property Fields
        private VisualElement settings;
        private ObjectField wheelJointField;
        private IntegerField stepCountField;
        private DoubleField maximumVelocityField;
        private DoubleField accelerationField;
        private DoubleField decelerationField;
        private DoubleField zeroOffsetField;
        private Button setCurrentOffsetButton;

        //Control Panel Fields
        private TextField velocityControlPanel;
        private TextField positionControlPanel;
        private TextField positionStepControlPanel;
        private TextField stateControlPanel;
        private IntegerField targetStepControlField;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DDrivenStepperMotorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            preferredVelocity.value = component.PreferredVelocity.ToString();
            targetStep.value = component.TargetStep.ToString();
            continuous.value = component.Continuous.ToString();
            continuousDirection.value = component.ContinuousDirection.ToString();
            velocity.value = component.Velocity.ToString();
            position.value = component.Position.ToString();
            state.value = component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive");
            state.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            preferredVelocity = root.Q<TextField>(name: "preferred-velocity");
            preferredVelocity.isReadOnly = true;

            targetStep = root.Q<TextField>(name: "target-step");
            targetStep.isReadOnly = true;

            continuous = root.Q<TextField>(name: "continuous");
            continuous.isReadOnly = true;

            continuousDirection = root.Q<TextField>(name: "continuous-direction");
            continuousDirection.isReadOnly = true;

            velocity = root.Q<TextField>(name: "velocity");
            velocity.isReadOnly = true;

            position = root.Q<TextField>(name: "position");
            position.isReadOnly = true;

            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;
            state.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            #endregion
            #region << Properties >>
            settings = root.Q<VisualElement>(name: "settings");
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            stepCountField = root.Q<IntegerField>(name: "step-count");
            maximumVelocityField = root.Q<DoubleField>(name: "maximum-velocity");
            accelerationField = root.Q<DoubleField>(name: "acceleration");
            decelerationField = root.Q<DoubleField>(name: "deceleration");
            zeroOffsetField = root.Q<DoubleField>(name: "zero-offset");
            setCurrentOffsetButton = root.Q<Button>(name: "set-current-offset");

            UIUtility.InitializeField
            (
                wheelJointField,
                component,
                () => component.KinematicWheelJoint,
                _e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint)_e.newValue;
                },
                typeof(AWheelJoint)
            );

            UIUtility.InitializeField
            (
                stepCountField,
                component,
                () => component.StepsPerCycle,
                _e =>
                {
                    component.StepsPerCycle = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                maximumVelocityField,
                component,
                () => component.MaxVelocity,
                _e =>
                {
                    component.MaxVelocity = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                accelerationField,
                component,
                () => component.Acceleration,
                _e =>
                {
                    component.Acceleration = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                decelerationField,
                component,
                () => component.Deceleration,
                _e =>
                {
                    component.Deceleration = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                zeroOffsetField,
                component,
                () => component.ZeroOffset,
                _e =>
                {
                    component.ZeroOffset = _e.newValue;
                }
            );

            setCurrentOffsetButton.RegisterCallback<MouseUpEvent>(_mouseEvent => 
            {
                component.ZeroOffset = component.KinematicWheelJoint.CurrentRevolutionDegrees;
                zeroOffsetField.SetValueWithoutNotify(component.ZeroOffset);
            });
            #endregion
        }


        public override void ShowControlPanelProperties(VisualElement _container)
        {
            #region Slider
            VisualElement sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("row");

            Slider prefVelocitySlider = new Slider("Preferred Velocity (deg/s)", 0f, (float)component.MaxVelocity);
            prefVelocitySlider.AddToClassList("flex-grow");

            FloatField prefVelocityField = new FloatField();
            prefVelocityField.style.width = 60;

            sliderContainer.Add(prefVelocitySlider);
            sliderContainer.Add(prefVelocityField);

            UIUtility.BindSliderAndField
            (
                prefVelocitySlider,
                prefVelocityField,
                component,
                () => (float)component.PreferredVelocity,
                _e => component.PreferredVelocity = _e.newValue
            );
            #endregion

            targetStepControlField = new IntegerField("Target step");
            targetStepControlField.isDelayed = true;
            UIUtility.InitializeField
            (
                targetStepControlField,
                component,
                () => component.TargetStep,
                _e =>
                {
                    component.TargetStep = _e.newValue;
                }
            );
            UIUtility.SetReadOnlyState(targetStepControlField, component.Continuous);

            Toggle continuousToggle = new Toggle("Continuous");
            UIUtility.InitializeField
            (
                continuousToggle,
                component,
                () => component.Continuous,
                _e =>
                {
                    component.Continuous = _e.newValue;
                    UIUtility.SetReadOnlyState(targetStepControlField, component.Continuous);
                }
            );

            EnumField continuousDirectionField = new EnumField("Continuous Direction");
            UIUtility.InitializeField
            (
                continuousDirectionField,
                component,
                () => component.ContinuousDirection,
                _e =>
                {
                    component.ContinuousDirection = (DDrivenStepperMotor.Direction)_e.newValue;
                }
            );

            velocityControlPanel = new TextField("Velocity (deg/s)");
            velocityControlPanel.AddToClassList("no-box");
            velocityControlPanel.isReadOnly = true;

            positionControlPanel = new TextField("Position (deg)");
            positionControlPanel.AddToClassList("no-box");
            positionControlPanel.isReadOnly = true;

            positionStepControlPanel = new TextField("Position (step)");
            positionStepControlPanel.AddToClassList("no-box");
            positionStepControlPanel.isReadOnly = true;

            stateControlPanel = new TextField("State");
            stateControlPanel.AddToClassList("no-box");
            stateControlPanel.isReadOnly = true;

            //schedule the updater
            scheduleControlPanelUpdate(velocityControlPanel);

            Button startRotation = new Button();
            startRotation.text = component.Error ? "Reset Error" : (component.IsActive ? "Stop" : "Start");
            startRotation.AddToClassList("content-fit-button");
            startRotation.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                if (component.Error)
                {
                    component.ResetError();
                }
                else
                {
                    component.IsActive = !component.IsActive;
                }
                startRotation.text = component.Error ? "Reset Error" : (component.IsActive ? "Stop" : "Start");
            });

            _container.Add(sliderContainer);
            _container.Add(targetStepControlField);
            _container.Add(continuousToggle);
            _container.Add(continuousDirectionField);
            _container.Add(velocityControlPanel);
            _container.Add(positionControlPanel);
            _container.Add(positionStepControlPanel);
            _container.Add(stateControlPanel);
            _container.Add(startRotation);
        }

        //Define the fields in the control panel that need to be updated constantly
        protected override void updateControlPanelData()
        {
            velocityControlPanel.value = Application.isPlaying ? component.Velocity.ToString() : "N/A";
            positionControlPanel.value = Application.isPlaying ? component.PositionDegrees.ToString() : "N/A";
            positionStepControlPanel.value = Application.isPlaying ? component.PositionSteps.ToString() : "N/A";
            stateControlPanel.value = component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive");
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            if (component.Continuous)
            {
                targetStepControlField.value = component.TargetStep;
            }
        }
    }
}
