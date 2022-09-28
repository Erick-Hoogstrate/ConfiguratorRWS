using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.steppermotor.editor
{
    [CustomEditor(typeof(DDrivenStepperMotor))]
    public class DDrivenStepperMotorEditorUIE : StandardComponentEditorUIE<DDrivenStepperMotor>
    {
        #region << Live Data Fields >>
        TextField preferredVelocity;
        TextField targetStep;
        TextField continuous;
        TextField continuousDirection;
        TextField velocity;
        TextField position;
        TextField state;
        #endregion
        #region << Property Fields >>
        VisualElement settings;
        VisualElement pulseWithModulationEnabled;
        Label noWheelJoint;
        ObjectField wheelJointField;
        IntegerField stepCount;
        DoubleField maximumVelocity;
        DoubleField acceleration;
        DoubleField zeroOffset;
        Button setCurrentOffset;
        #endregion
        #region << Control Panel Fields >>
        TextField velocityControlPanel;
        TextField positionControlPanel;
        TextField positionStepControlPanel;
        TextField stateControlPanel;
        IntegerField targetStepControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/DDrivenStepperMotorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
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

        protected override void Initialize()
        {
            base.Initialize();

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
            pulseWithModulationEnabled = root.Q<VisualElement>(name: "pulse-width-modulation-enabled");
            noWheelJoint = root.Q<Label>(name: "no-wheel-joint");
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            stepCount = root.Q<IntegerField>(name: "step-count");
            maximumVelocity = root.Q<DoubleField>(name: "maximum-velocity");
            acceleration = root.Q<DoubleField>(name: "acceleration");
            zeroOffset = root.Q<DoubleField>(name: "zero-offset");
            setCurrentOffset = root.Q<Button>(name: "set-current-offset");

            UIUtility.InitializeField
            (
                wheelJointField,
                () => component.KinematicWheelJoint,
                e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint)e.newValue;
                    UIUtility.SetDisplay(settings, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(noWheelJoint, e.newValue == null ? true : false);
                },
                typeof(AWheelJoint)
            );

            UIUtility.SetDisplay(settings, component.KinematicWheelJoint == null ? false : true);
            UIUtility.SetDisplay(noWheelJoint, component.KinematicWheelJoint == null ? true : false);

            UIUtility.InitializeField
            (
                stepCount,
                () => component.StepsPerCycle,
                e =>
                {
                    component.StepsPerCycle = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                maximumVelocity,
                () => component.MaxVelocity,
                e =>
                {
                    component.MaxVelocity = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                acceleration,
                () => component.Acceleration,
                e =>
                {
                    component.Acceleration = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                zeroOffset,
                () => component.ZeroOffset,
                e =>
                {
                    component.ZeroOffset = e.newValue;
                }
            );

            setCurrentOffset.RegisterCallback<MouseUpEvent>(mouseEvent => 
            {
                component.ZeroOffset = component.KinematicWheelJoint.CurrentRevolutionDegrees;
                zeroOffset.SetValueWithoutNotify(component.ZeroOffset);
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
                () => (float)component.PreferredVelocity,
                e => component.PreferredVelocity = e.newValue
            );
            #endregion

            targetStepControlPanel = new IntegerField("Target step");
            targetStepControlPanel.isDelayed = true;
            UIUtility.InitializeField
            (
                targetStepControlPanel,
                () => component.TargetStep,
                e =>
                {
                    component.TargetStep = e.newValue;
                }
            );
            UIUtility.ToggleNoBoxAndReadOnly(targetStepControlPanel, component.Continuous);

            Toggle continuous = new Toggle("Continuous");
            UIUtility.InitializeField
            (
                continuous,
                () => component.Continuous,
                e =>
                {
                    component.Continuous = e.newValue;
                    UIUtility.ToggleNoBoxAndReadOnly(targetStepControlPanel, component.Continuous);
                }
            );

            EnumField continuousDirection = new EnumField("Continuous Direction");
            UIUtility.InitializeField
            (
                continuousDirection,
                () => component.ContinuousDirection,
                e =>
                {
                    component.ContinuousDirection = (DDrivenStepperMotor.Direction)e.newValue;
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
            ScheduleControlPanelUpdate(velocityControlPanel);

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
            _container.Add(targetStepControlPanel);
            _container.Add(continuous);
            _container.Add(continuousDirection);
            _container.Add(velocityControlPanel);
            _container.Add(positionControlPanel);
            _container.Add(positionStepControlPanel);
            _container.Add(stateControlPanel);
            _container.Add(startRotation);
        }

        //Define the fields in the control panel that need to be udpated constantly
        protected override void UpdateControlPanelData()
        {
            velocityControlPanel.value = Application.isPlaying ? component.Velocity.ToString() : "N/A";
            positionControlPanel.value = Application.isPlaying ? component.PositionDegrees.ToString() : "N/A";
            positionStepControlPanel.value = Application.isPlaying ? component.PositionSteps.ToString() : "N/A";
            stateControlPanel.value = component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive");
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            if (component.Continuous)
            {
                targetStepControlPanel.value = component.TargetStep;
            }
        }
    }
}
