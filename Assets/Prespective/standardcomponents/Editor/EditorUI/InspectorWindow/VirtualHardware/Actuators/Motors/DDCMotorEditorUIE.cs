using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.motors
{
    [CustomEditor(typeof(DDCMotor))]
    public class DDCMotorEditorUIE : StandardComponentEditorUIE<DDCMotor>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField targetVelocity;
        private TextField velocity;

        //Property Fields
        private ObjectField wheelJointField;
        private DoubleField maximumVelocityField;
        private DoubleField angularAcceleration;
        private DoubleField angularDeceleration;

        //Control Panel Fields
        TextField velocityControlPanel;
        Slider prefVelocitySlider;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DDCMotorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            targetVelocity.value = component.TargetVelocity.ToString();
            velocity.value = component.Velocity.ToString();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>

            targetVelocity = root.Q<TextField>(name: "target-velocity");
            targetVelocity.isReadOnly = true;

            velocity = root.Q<TextField>(name: "velocity");
            velocity.isReadOnly = true;

            #endregion

            #region << Properties >>

            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            maximumVelocityField = root.Q<DoubleField>(name: "maximum-velocity");
            angularAcceleration = root.Q<DoubleField>(name: "angular-acceleration");
            angularDeceleration = root.Q<DoubleField>(name: "angular-deceleration");

            UIUtility.InitializeField
            (
                wheelJointField,
                component,
                () => component.KinematicWheelJoint,
                _e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint) _e.newValue;
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
                component,
                () => component.Acceleration,
                _e => { component.Acceleration = _e.newValue; }
            );

            UIUtility.InitializeField
            (
                angularDeceleration,
                component,
                () => component.Deceleration,
                _e => { component.Deceleration = _e.newValue; }
            );
            #endregion
        }

        public void OnValidate()
        {
            
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            //Double values are cast to float because Slider with double values is not yet available

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
                component,
                () => (float) component.TargetVelocity,
                _e => component.TargetVelocity = _e.newValue
            );

            #endregion

            velocityControlPanel = new TextField("Velocity (deg/s)");
            velocityControlPanel.AddToClassList("no-box");
            velocityControlPanel.isReadOnly = true;

            //schedule the update for the velocity
            scheduleControlPanelUpdate(velocityControlPanel);

            VisualElement buttonContainer = new VisualElement();
            buttonContainer.AddToClassList("row");

            Button startRotation = new Button();
            Button stoprotation = new Button();

            startRotation.text = "Start";
            startRotation.AddToClassList("content-fit-button");
            startRotation.AddToClassList("margin-unity-standard");
            startRotation.RegisterCallback<MouseUpEvent>(_e =>
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
            stoprotation.RegisterCallback<MouseUpEvent>(_e =>
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

        protected override void updateControlPanelData()
        {
            velocityControlPanel.value = Application.isPlaying ? component.Velocity.ToString() : "N/A";
        }
    }
}
