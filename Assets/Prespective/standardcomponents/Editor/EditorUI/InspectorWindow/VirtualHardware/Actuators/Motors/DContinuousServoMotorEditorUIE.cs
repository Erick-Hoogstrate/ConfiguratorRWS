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
    [CustomEditor(typeof(DContinuousServoMotor))]
    public class DContinuousServoMotorEditorUIE : StandardComponentEditorUIE<DContinuousServoMotor>
    {
        #region << Live Data Fields >>
        private TextField targetDegreesField;
        private TextField positionField;
        #endregion
        #region << Property Fields >>
        private VisualElement settings;
        private VisualElement pulseWithModulationEnabled;
        private ObjectField wheelJointField;
        private EnumField rotationRangefield;
        private DoubleField timePerDegreesField;
        private DoubleField deadAngleField;
        private Toggle pulseWidthModulationToggle;
        private DoubleField zeroDegreePulseWidth;
        private DoubleField oneEightyDegpulseWidth;
        private DoubleField deadBandWidth;
        #endregion
        #region << Control Panel Fields >>
        private TextField positionControlPanel;
        private DoubleField pulseWidthControlPanelField;
        #endregion

        #region << Properties >>
        protected override string visualTreeFile => "DContinuousServoMotorEditorLayout";
        #endregion

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
            rotationRangefield = root.Q<EnumField>(name: "rotation-range");
            timePerDegreesField = root.Q<DoubleField>(name: "time-per-degrees");
            deadAngleField = root.Q<DoubleField>(name: "dead-angle");
            pulseWidthModulationToggle = root.Q<Toggle>(name: "pulse-width-modulation");

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
                rotationRangefield,
                component,
                () => component.RotationRange,
                _e =>
                {
                    component.RotationRange = (DContinuousServoMotor.Range)_e.newValue;
                    deadBandWidth.SetValueWithoutNotify(component.DeadBandWidth);
                    oneEightyDegpulseWidth.label = component.RotationRange == DContinuousServoMotor.Range.D_180 ? "180 Deg Pulse Width (ms)" : "270 Deg Pulse Width (ms)";
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
                deadAngleField,
                component,
                () => component.DeadAngle,
                _e =>
                {
                    component.DeadAngle = _e.newValue;
                    deadAngleField.SetValueWithoutNotify(component.DeadAngle);
                    deadBandWidth.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                pulseWidthModulationToggle,
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

            zeroDegreePulseWidth = root.Q<DoubleField>(name: "zero-degree-pulse-width");
            oneEightyDegpulseWidth = root.Q<DoubleField>(name: "180-degree-pulse-width");
            deadBandWidth = root.Q<DoubleField>(name: "dead-band-width");

            UIUtility.InitializeField
            (
                zeroDegreePulseWidth,
                component,
                () => component.PulseWidthDefinition.X,
                _e =>
                {
                    component.PulseWidthDefinition = new DVector2(_e.newValue, component.PulseWidthDefinition.Y);
                    zeroDegreePulseWidth.SetValueWithoutNotify(component.PulseWidthDefinition.X);
                    deadBandWidth.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                oneEightyDegpulseWidth,
                component,
                () => component.PulseWidthDefinition.Y,
                _e =>
                {
                    component.PulseWidthDefinition = new DVector2(component.PulseWidthDefinition.X, _e.newValue);
                    oneEightyDegpulseWidth.SetValueWithoutNotify(component.PulseWidthDefinition.Y);
                    deadBandWidth.SetValueWithoutNotify(component.DeadBandWidth);
                }
            );

            UIUtility.InitializeField
            (
                deadBandWidth,
                component,
                () => component.DeadBandWidth,
                _e =>
                {
                    component.DeadBandWidth = _e.newValue;
                    deadBandWidth.SetValueWithoutNotify(component.DeadBandWidth);
                    deadAngleField.SetValueWithoutNotify(component.DeadAngle);
                }
            );
            #endregion
            #endregion

            EditorApplication.playModeStateChanged += _state =>
            {
                properties.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
            };
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
