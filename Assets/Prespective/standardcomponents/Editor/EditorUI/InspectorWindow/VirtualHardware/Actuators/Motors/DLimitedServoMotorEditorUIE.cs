using u040.prespective.math.doubles;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.motors
{
    [CustomEditor(typeof(DLimitedServoMotor))]
    public class DLimitedServoMotorEditorUIE : StandardComponentEditorUIE<DLimitedServoMotor>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField targetDegreesField;
        private TextField positionField;
        
        //Property Fields
        private VisualElement settings;
        private VisualElement pulseWithModulationEnabled;
        private Label noWheelJointLabel;
        private ObjectField wheelJointField;
        private EnumField rotationRangeField;
        private DoubleField timePerDegreesField;
        private DoubleField dampingField;
        private DoubleField deadAngleField;
        private Toggle pulseWidthModulationField;
        private DoubleField zeroDegreePulseWidthField;
        private DoubleField oneEightyDegpulseWidthField;
        private DoubleField deadBandWidthField;
        
        //Control Panel Fields
        private TextField positionControlPanel;
        private DoubleField pulseWidthControlPanelField;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DLimitedServoMotorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            targetDegreesField.value = component.Target.ToString();
            positionField.value = component.Position.ToString();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            targetDegreesField = root.Q<TextField>(name: "target-degrees");
            targetDegreesField.isReadOnly = true;

            positionField = root.Q<TextField>(name: "position");
            positionField.isReadOnly = true;
            #endregion

            #region << Properties >>
            settings = root.Q<VisualElement>(name: "settings");
            pulseWithModulationEnabled = root.Q<VisualElement>(name: "pulse-width-modulation-enabled");
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            rotationRangeField = root.Q<EnumField>(name: "rotation-range");
            timePerDegreesField = root.Q<DoubleField>(name: "time-per-degrees");
            dampingField = root.Q<DoubleField>(name: "damping");
            deadAngleField = root.Q<DoubleField>(name: "dead-angle");
            pulseWidthModulationField = root.Q<Toggle>(name: "pulse-width-modulation");

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
                rotationRangeField,
                component,
                () => component.RotationRange,
                _e =>
                {
                    component.RotationRange = (DContinuousServoMotor.Range)_e.newValue;
                    deadBandWidthField.SetValueWithoutNotify(component.DeadBandWidth);
                    oneEightyDegpulseWidthField.label = component.RotationRange == DContinuousServoMotor.Range.D_180 ? "180 Deg Pulse Width (ms)" : "270 Deg Pulse Width (ms)";
                }
            );

            UIUtility.InitializeField
            (
                timePerDegreesField,
                component,
                () => component.SecondsPer60Degrees,
                _e =>
                {
                    component.SecondsPer60Degrees = _e.newValue;
                    timePerDegreesField.SetValueWithoutNotify(component.SecondsPer60Degrees);
                }
            );

            UIUtility.InitializeField
            (
                dampingField,
                component,
                () => component.Damping,
                _e =>
                {
                    component.Damping = _e.newValue;
                    dampingField.SetValueWithoutNotify(component.Damping);
                }
            );

            UIUtility.InitializeField
            (
                deadAngleField,
                component,
                () => component.DeadAngle,
                _e =>
                {
                    component.DeadAngle = _e.newValue;
                    deadAngleField.SetValueWithoutNotify(component.DeadAngle);
                    deadBandWidthField.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                pulseWidthModulationField,
                component,
                () => component.EnablePWM,
                _e =>
                {
                    component.EnablePWM = _e.newValue;
                    UIUtility.SetDisplay(pulseWithModulationEnabled, component.EnablePWM);
                }
            );

            #region << Pulse Width Modulation Settings >>
            UIUtility.SetDisplay(pulseWithModulationEnabled, component.EnablePWM);

            zeroDegreePulseWidthField = root.Q<DoubleField>(name: "zero-degree-pulse-width");
            oneEightyDegpulseWidthField = root.Q<DoubleField>(name: "180-degree-pulse-width");
            deadBandWidthField = root.Q<DoubleField>(name: "dead-band-width");

            UIUtility.InitializeField
            (
                zeroDegreePulseWidthField,
                component,
                () => component.PulseWidthDefinition.X,
                _e =>
                {
                    component.PulseWidthDefinition = new DVector2(_e.newValue, component.PulseWidthDefinition.Y);
                    zeroDegreePulseWidthField.SetValueWithoutNotify(component.PulseWidthDefinition.X);
                    deadBandWidthField.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                oneEightyDegpulseWidthField,
                component,
                () => component.PulseWidthDefinition.Y,
                _e =>
                {
                    component.PulseWidthDefinition = new DVector2(component.PulseWidthDefinition.X, _e.newValue);
                    oneEightyDegpulseWidthField.SetValueWithoutNotify(component.PulseWidthDefinition.Y);
                    deadBandWidthField.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                deadBandWidthField,
                component,
                () => component.DeadBandWidth,
                _e =>
                {
                    component.DeadBandWidth = _e.newValue;
                    deadBandWidthField.SetValueWithoutNotify(component.DeadBandWidth);
                    deadAngleField.SetValueWithoutNotify(component.DeadAngle);
                }
            );
            #endregion
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            DoubleField targetField = new DoubleField("Target (deg)");
            targetField.isDelayed = true;
            UIUtility.InitializeField
            (
                targetField,
                component,
                () => component.Target,
                _e =>
                {
                    component.Target = _e.newValue;
                    targetField.SetValueWithoutNotify(component.Target);
                    pulseWidthControlPanelField.SetValueWithoutNotify(component.PulseWidth);
                }
            );

            pulseWidthControlPanelField = new DoubleField("Pulse Width (ms)");
            pulseWidthControlPanelField.isDelayed = true;

            UIUtility.InitializeField
            (
                pulseWidthControlPanelField,
                component,
                () => component.PulseWidth,
                _e =>
                {
                    component.PulseWidth = _e.newValue;
                    pulseWidthControlPanelField.SetValueWithoutNotify(component.PulseWidth);
                    targetField.SetValueWithoutNotify(component.Target);
                }
            );

            UIUtility.SetDisplay(pulseWidthControlPanelField, component.EnablePWM);

            positionControlPanel = new TextField("Position (deg)");
            positionControlPanel.AddToClassList("no-box");
            positionControlPanel.isReadOnly = true;

            scheduleControlPanelUpdate(targetField);

            _container.Add(targetField);
            _container.Add(pulseWidthControlPanelField);
            _container.Add(positionControlPanel);
        }

        protected override void updateControlPanelData()
        {
            positionControlPanel.value = Application.isPlaying ? component.Position.ToString() : "N/A";
            UIUtility.SetDisplay(pulseWidthControlPanelField, component.EnablePWM);
        }
    }
}
