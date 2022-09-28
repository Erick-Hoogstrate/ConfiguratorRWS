using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(DCMotor))]
    public class DCMotorEditorUIE : StandardComponentEditorUIE<DCMotor>
    {
        #region << Live Data Fields >>
        TextField targetVelocity;
        TextField velocity;
        #endregion
        #region << Property Fields>>
        ObjectField wheelJointField;
        FloatField maximumVelocity;
        FloatField angularAcceleration;
        Label noWheelJoint;
        #endregion
        #region << Control Panel Fields >>
        TextField velocityControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/DCMotorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            targetVelocity.value = component.TargetVelocity.ToString();
            velocity.value = component.Velocity.ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            targetVelocity = root.Q<TextField>(name: "target-velocity");
            targetVelocity.isReadOnly = true;

            velocity = root.Q<TextField>(name: "velocity");
            velocity.isReadOnly = true;

            #endregion
            #region << Properties >>
            ObjectField wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            FloatField maximumVelocity = root.Q<FloatField>(name: "maximum-velocity");
            FloatField angularAcceleration = root.Q<FloatField>(name: "angular-acceleration");
            Label noWheelJoint = root.Q<Label>(name: "no-wheel-joint");

            UIUtility.InitializeField
            (
                wheelJointField,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AFWheelJoint)e.newValue;
                    UIUtility.SetDisplay(maximumVelocity, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(angularAcceleration, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(noWheelJoint, e.newValue == null ? true : false);
                },
                typeof(AFWheelJoint)
            );

            UIUtility.SetDisplay(maximumVelocity, component.WheelJoint == null ? false : true);
            UIUtility.SetDisplay(angularAcceleration, component.WheelJoint == null ? false : true);
            UIUtility.SetDisplay(noWheelJoint, component.WheelJoint == null ? true : false);

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
                angularAcceleration,
                () => component.Acceleration,
                e =>
                {
                    component.Acceleration = e.newValue;
                }
            );

            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            Button startRotation = new Button();
            Button stoprotation = new Button();

            #region Slider
            VisualElement sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("row");

            Slider prefVelocitySlider = new Slider("Preferred Velocity (deg/s)", -component.MaxVelocity, component.MaxVelocity);
            prefVelocitySlider.AddToClassList("flex-grow");

            FloatField prefVelocityField = new FloatField();
            prefVelocityField.style.width = 60;

            sliderContainer.Add(prefVelocitySlider);
            sliderContainer.Add(prefVelocityField);

            UIUtility.BindSliderAndField
            (
                prefVelocitySlider,
                prefVelocityField,
                () => component.TargetVelocity,
                e =>
                {
                    component.TargetVelocity = e.newValue;
                    UIUtility.SetDisplay(startRotation, component.TargetVelocity == 0);
                    UIUtility.SetDisplay(stoprotation, component.TargetVelocity != 0);
                }
            );
            #endregion

            velocityControlPanel = new TextField("Velocity (deg/s)");
            velocityControlPanel.AddToClassList("no-box");
            velocityControlPanel.isReadOnly = true;

            //schedule the update for the velocity
            ScheduleControlPanelUpdate(velocityControlPanel);

            VisualElement buttonContainer = new VisualElement();
            buttonContainer.AddToClassList("row");

            UIUtility.SetDisplay(startRotation, component.TargetVelocity == 0);
            UIUtility.SetDisplay(stoprotation, component.TargetVelocity != 0);

            startRotation.text = "Start";
            startRotation.AddToClassList("content-fit-button");
            startRotation.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.StartRotation();
                prefVelocitySlider.value = component.TargetVelocity;
                prefVelocityField.value = component.TargetVelocity;
                UIUtility.SetDisplay(startRotation, false);
                UIUtility.SetDisplay(stoprotation, true);
            });

            stoprotation.text = "Stop";
            stoprotation.AddToClassList("content-fit-button");
            stoprotation.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.StopRotation();
                prefVelocitySlider.value = component.TargetVelocity;
                prefVelocityField.value = component.TargetVelocity;
                UIUtility.SetDisplay(startRotation, true);
                UIUtility.SetDisplay(stoprotation, false);
            });

            buttonContainer.Add(startRotation);
            buttonContainer.Add(stoprotation);

            _container.Add(sliderContainer);
            _container.Add(velocityControlPanel);
            _container.Add(buttonContainer);
        }

        protected override void UpdateControlPanelData()
        {
            velocityControlPanel.value = Application.isPlaying ? component.Velocity.ToString() : "N/A";
        }
#pragma warning restore 0618
    }
}
