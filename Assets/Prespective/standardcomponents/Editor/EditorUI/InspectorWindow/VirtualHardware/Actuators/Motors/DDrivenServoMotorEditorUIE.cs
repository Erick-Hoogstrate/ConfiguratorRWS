using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.motors
{
    [CustomEditor(typeof(DDrivenServoMotor))]
    public class DDrivenServoMotorEditorUIE : StandardComponentEditorUIE<DDrivenServoMotor>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField preferredVelocityField;
        private TextField targetAngleField;
        private TextField continuousField;
        private TextField continuousDirectionField;
        private TextField velocityField;
        private TextField positionField;
        private TextField stateField;

        //Property Fields
        private ObjectField wheelJointField;
        private DoubleField maximumVelocityField;
        private DoubleField accelerationField;
        private DoubleField decelerationField;
        private DoubleField zeroOffsetField;
        private Button setCurrentOffsetButton;

        //Control Panel Fields
        private TextField velocityControlPanel;
        private TextField positionControlPanel;
        private TextField stateControlPanel;
        private FloatField targetAngleControlPanelField;

        #endregion
        #region  << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DDrivenServoMotorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            preferredVelocityField.value = component.PreferredVelocity.ToString();
            targetAngleField.value = component.TargetAngle.ToString();
            continuousField.value = component.Continuous.ToString();
            continuousDirectionField.value = component.ContinuousDirection.ToString();
            velocityField.value = component.Velocity.ToString();
            positionField.value = component.Position.ToString();
            stateField.value = component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive");
            stateField.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            preferredVelocityField = root.Q<TextField>(name: "preferred-velocity");
            preferredVelocityField.isReadOnly = true;

            targetAngleField = root.Q<TextField>(name: "target-angle");
            targetAngleField.isReadOnly = true;

            continuousField = root.Q<TextField>(name: "continuous");
            continuousField.isReadOnly = true;

            continuousDirectionField = root.Q<TextField>(name: "continuous-direction");
            continuousDirectionField.isReadOnly = true;

            velocityField = root.Q<TextField>(name: "velocity");
            velocityField.isReadOnly = true;

            positionField = root.Q<TextField>(name: "position");
            positionField.isReadOnly = true;

            stateField = root.Q<TextField>(name: "state");
            stateField.isReadOnly = true;
            stateField.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            #endregion
            #region << Properties >>
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
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

            targetAngleControlPanelField = new FloatField("Target Angle (deg)");
            targetAngleControlPanelField.isDelayed = true;
            UIUtility.InitializeField
            (
                targetAngleControlPanelField,
                component,
                () => (float)component.TargetAngle,
                _e =>
                {
                    component.TargetAngle = _e.newValue;
                }
            );
            UIUtility.SetReadOnlyState(targetAngleControlPanelField, component.Continuous);

            Toggle continuousToggle = new Toggle("Continuous");
            UIUtility.InitializeField
            (
                continuousToggle,
                component,
                () => component.Continuous,
                _e =>
                {
                    component.Continuous = _e.newValue;
                    UIUtility.SetReadOnlyState(targetAngleControlPanelField, component.Continuous);
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
                    component.ContinuousDirection = (DDrivenServoMotor.Direction)_e.newValue;
                }
            );

            velocityControlPanel = new TextField("Velocity (deg/s)");
            velocityControlPanel.AddToClassList("no-box");
            velocityControlPanel.isReadOnly = true;

            positionControlPanel = new TextField("Position (deg)");
            positionControlPanel.AddToClassList("no-box");
            positionControlPanel.isReadOnly = true;

            stateControlPanel = new TextField("State");
            stateControlPanel.AddToClassList("no-box");
            stateControlPanel.isReadOnly = true;

            //schedule the updater
            scheduleControlPanelUpdate(velocityControlPanel);

            Button startRotation = new Button();
            startRotation.text = component.Error ? "Reset Error" : (component.IsActive ? "Stop" : "Start");
            startRotation.AddToClassList("content-fit-button");
            startRotation.AddToClassList("margin-unity-standard");
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
            _container.Add(targetAngleControlPanelField);
            _container.Add(continuousToggle);
            _container.Add(continuousDirectionField);
            _container.Add(velocityControlPanel);
            _container.Add(positionControlPanel);
            _container.Add(stateControlPanel);
            _container.Add(startRotation);
        }

        //Define the fields in the control panel that need to be updated constantly
        protected override void updateControlPanelData()
        {
            velocityControlPanel.value = Application.isPlaying ? component.Velocity.ToString() : "N/A";
            positionControlPanel.value = Application.isPlaying ? component.Position.ToString() : "N/A";
            stateControlPanel.value = component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive");
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            if (component.Continuous)
            {
                targetAngleControlPanelField.value = (float)component.TargetAngle;
            }
        }
    }
}
