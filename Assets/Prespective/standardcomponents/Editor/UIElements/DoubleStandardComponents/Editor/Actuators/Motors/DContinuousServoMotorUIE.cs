using System.Reflection;
using u040.prespective.math.doubles;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor.editor
{
    [CustomEditor(typeof(DContinuousServoMotor))]
    public class DContinuousServoMotorUIE : StandardComponentEditorUIE<DContinuousServoMotor>
    {
        #region << Live Data Fields >>
        TextField targetDegrees;
        TextField position;
        #endregion
        #region << Property Fields >>
        VisualElement settings;
        VisualElement pulseWithModulationEnabled;
        Label noWheelJoint;
        ObjectField wheelJointField;
        EnumField rotationRange;
        DoubleField timePerDegrees;
        DoubleField deadAngle;
        Toggle pulseWidthModulation;
        #endregion
        #region << Control Panel Fields >>
        TextField positionControlPanel;
        DoubleField pulseWidthConrolPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/DContinuousServoMotorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            targetDegrees.value = component.Target.ToString();
            position.value = component.Position.ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            targetDegrees = root.Q<TextField>(name: "target-degrees");
            targetDegrees.isReadOnly = true;

            position = root.Q<TextField>(name: "position");
            position.isReadOnly = true;
            #endregion
            #region << Properties >>
            settings = root.Q<VisualElement>(name: "settings");
            pulseWithModulationEnabled = root.Q<VisualElement>(name: "pulse-width-modulation-enabled");
            noWheelJoint = root.Q<Label>(name: "no-wheel-joint");
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint-field");
            rotationRange = root.Q<EnumField>(name: "rotation-range");
            timePerDegrees = root.Q<DoubleField>(name: "time-per-degrees");
            deadAngle = root.Q<DoubleField>(name: "dead-angle");
            pulseWidthModulation = root.Q<Toggle>(name: "pulse-width-modulation");

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
                rotationRange,
                () => component.RotationRange,
                e =>
                {
                    component.RotationRange = (DContinuousServoMotor.Range)e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                timePerDegrees,
                () => component.SecondsPer60Degrees,
                e =>
                {
                    component.SecondsPer60Degrees = e.newValue;
                    timePerDegrees.SetValueWithoutNotify(component.SecondsPer60Degrees);
                }
            );

            UIUtility.InitializeField
            (
                deadAngle,
                () => component.DeadAngle,
                e =>
                {
                    component.DeadAngle = e.newValue;
                    deadAngle.SetValueWithoutNotify(component.DeadAngle);
                }
            );

            UIUtility.InitializeField
            (
                pulseWidthModulation,
                () => component.EnablePWM,
                e =>
                {
                    component.EnablePWM = e.newValue;
                    UIUtility.SetDisplay(pulseWithModulationEnabled, component.EnablePWM ? true : false);
                }
            );

            #region << Pulse Width Modulation Settings >>
            UIUtility.SetDisplay(pulseWithModulationEnabled, component.EnablePWM ? true : false);

            DoubleField zeroDegreePulseWidth = root.Q<DoubleField>(name: "zero-degree-pulse-width");
            DoubleField oneEightyDegpulseWidth = root.Q<DoubleField>(name: "180-degree-pulse-width");
            DoubleField deadBandWidth = root.Q<DoubleField>(name: "dead-band-width");

            UIUtility.InitializeField
            (
                zeroDegreePulseWidth,
                () => component.PulseWidthDefinition.X,
                e =>
                {
                    component.PulseWidthDefinition = new DVector2(e.newValue, component.PulseWidthDefinition.Y);
                    zeroDegreePulseWidth.SetValueWithoutNotify(component.PulseWidthDefinition.X);
                }
            );

            UIUtility.InitializeField
            (
                oneEightyDegpulseWidth,
                () => component.PulseWidthDefinition.Y,
                e =>
                {
                    component.PulseWidthDefinition = new DVector2(component.PulseWidthDefinition.X, e.newValue);
                    oneEightyDegpulseWidth.SetValueWithoutNotify(component.PulseWidthDefinition.Y);
                }
            );

            UIUtility.InitializeField
            (
                deadBandWidth,
                () => component.DeadBandWidth,
                e =>
                {
                    component.DeadBandWidth = e.newValue;
                }
            );
            #endregion
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                properties.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            DoubleField target = new DoubleField("Target (deg)");
            target.isDelayed = true;
            UIUtility.InitializeField
            (
                target,
                () => component.Target,
                e =>
                {
                    component.Target = e.newValue;
                    target.SetValueWithoutNotify(component.Target);
                    pulseWidthConrolPanel.SetValueWithoutNotify(component.PulseWidth);
                }
            );

            pulseWidthConrolPanel = new DoubleField("Pulse Width (ms)");
            pulseWidthConrolPanel.isDelayed = true;
            UIUtility.InitializeField
            (
                pulseWidthConrolPanel,
                () => component.PulseWidth,
                e =>
                {
                    component.PulseWidth = e.newValue;
                    pulseWidthConrolPanel.SetValueWithoutNotify(component.PulseWidth);
                    target.SetValueWithoutNotify(component.Target);
                }
            );
            UIUtility.SetDisplay(pulseWidthConrolPanel, component.EnablePWM);

            positionControlPanel = new TextField("Position (deg)");
            positionControlPanel.AddToClassList("no-box");
            positionControlPanel.isReadOnly = true;

            ScheduleControlPanelUpdate(target);

            _container.Add(target);
            _container.Add(pulseWidthConrolPanel);
            _container.Add(positionControlPanel);
        }

        protected override void UpdateControlPanelData()
        {
            positionControlPanel.value = Application.isPlaying ? component.Position.ToString() : "N/A";
            UIUtility.SetDisplay(pulseWidthConrolPanel, component.EnablePWM);
        }
    }
}
