using System.Reflection;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor.editor
{
    [CustomEditor(typeof(DDCMotor))]
    public class DDCMotorUIE : StandardComponentEditorUIE<DDCMotor>
    {
        #region << Live Data Fields >>

        TextField targetVelocity;
        TextField velocity;

        #endregion

        #region << Property Fields>>

        ObjectField wheelJointField;
        DoubleField maximumVelocity;
        DoubleField angularAcceleration;
        DoubleField angularDeceleration;

        Label noWheelJoint;

        #endregion

        #region << Control Panel Fields >>

        TextField velocityControlPanel;
        Slider prefVelocitySlider;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/DDCMotorLayout");
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

            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            maximumVelocity = root.Q<DoubleField>(name: "maximum-velocity");
            angularAcceleration = root.Q<DoubleField>(name: "angular-acceleration");
            angularDeceleration = root.Q<DoubleField>(name: "angular-deceleration");
            Label noWheelJoint = root.Q<Label>(name: "no-wheel-joint");

            UIUtility.InitializeField
            (
                wheelJointField,
                () => component.KinematicWheelJoint,
                e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint) e.newValue;
                    UIUtility.SetDisplay(maximumVelocity, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(angularAcceleration, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(angularDeceleration, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(noWheelJoint, e.newValue == null ? true : false);
                },
                typeof(AWheelJoint)
            );

            UIUtility.SetDisplay(maximumVelocity, component.KinematicWheelJoint == null ? false : true);
            UIUtility.SetDisplay(angularAcceleration, component.KinematicWheelJoint == null ? false : true);
            UIUtility.SetDisplay(angularDeceleration, component.KinematicWheelJoint == null ? false : true);
            UIUtility.SetDisplay(noWheelJoint, component.KinematicWheelJoint == null ? true : false);

            UIUtility.InitializeField
            (
                maximumVelocity,
                () => component.MaxVelocity,
                e =>
                {
                    component.MaxVelocity = e.newValue;
                    if (prefVelocitySlider != null) 
                    {
                        prefVelocitySlider.lowValue = (float)-component.MaxVelocity;
                        prefVelocitySlider.highValue = (float)component.MaxVelocity;
                    }
                }
            );

            UIUtility.InitializeField
            (
                angularAcceleration,
                () => component.Acceleration,
                e => { component.Acceleration = e.newValue; }
            );

            UIUtility.InitializeField
            (
                angularDeceleration,
                () => component.Deceleration,
                e => { component.Deceleration = e.newValue; }
            );

            #endregion
        }

        public void OnValidate()
        {
            
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            //Double values are casted to float because Slider with double values is not yet available

            #region Slider

            VisualElement sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("row");

            prefVelocitySlider = new Slider("Preferred Velocity (deg/s)", (float)-component.MaxVelocity, (float)component.MaxVelocity);
            prefVelocitySlider.AddToClassList("flex-grow");

            FloatField prefVelocityField = new FloatField();
            prefVelocityField.style.width = 60;

            sliderContainer.Add(prefVelocitySlider);
            sliderContainer.Add(prefVelocityField);

            UIUtility.BindSliderAndField
            (
                prefVelocitySlider,
                prefVelocityField,
                () => (float) component.TargetVelocity,
                e => component.TargetVelocity = e.newValue
            );

            #endregion

            velocityControlPanel = new TextField("Velocity (deg/s)");
            velocityControlPanel.AddToClassList("no-box");
            velocityControlPanel.isReadOnly = true;

            //schedule the update for the velocity
            ScheduleControlPanelUpdate(velocityControlPanel);

            VisualElement buttonContainer = new VisualElement();
            buttonContainer.AddToClassList("row");

            Button startRotation = new Button();
            Button stoprotation = new Button();

            startRotation.text = "Start";
            startRotation.AddToClassList("content-fit-button");
            startRotation.AddToClassList("margin-unity-standard");
            startRotation.RegisterCallback<MouseUpEvent>(e =>
            {
                component.StartRotation();
                prefVelocitySlider.value = (float) component.TargetVelocity;
                prefVelocityField.value = (float) component.TargetVelocity;
                UIUtility.SetDisplay(startRotation, component.TargetVelocity == 0f);
                UIUtility.SetDisplay(stoprotation, component.TargetVelocity != 0f);
            });

            stoprotation.text = "Stop";
            stoprotation.AddToClassList("content-fit-button");
            stoprotation.AddToClassList("margin-unity-standard");
            stoprotation.RegisterCallback<MouseUpEvent>(e =>
            {
                component.StopRotation();
                prefVelocitySlider.value = (float) component.TargetVelocity;
                prefVelocityField.value = (float) component.TargetVelocity;
                UIUtility.SetDisplay(startRotation, component.TargetVelocity == 0f);
                UIUtility.SetDisplay(stoprotation, component.TargetVelocity != 0f);
            });

            UIUtility.SetDisplay(startRotation, component.TargetVelocity == 0f);
            UIUtility.SetDisplay(stoprotation, component.TargetVelocity != 0f);

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
    }
}
