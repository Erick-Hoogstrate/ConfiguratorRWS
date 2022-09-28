using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(ContinuousServoMotor))]
    public class ContinuousServoMotorEditorUIE : StandardComponentEditorUIE<ContinuousServoMotor>
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
        FloatField timePerDegrees;
        FloatField deadAngle;
        Toggle pulseWidthModulation;
        #endregion
        #region << Control Panel Fields >>
        TextField positionControlPanel;
        FloatField pulseWidthConrolPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/ContinuousServoMotorLayout");
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
            timePerDegrees = root.Q<FloatField>(name: "time-per-degrees");
            deadAngle = root.Q<FloatField>(name: "dead-angle");
            pulseWidthModulation = root.Q<Toggle>(name: "pulse-width-modulation");

            UIUtility.InitializeField
            (
                wheelJointField,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AFWheelJoint)e.newValue;
                    UIUtility.SetDisplay(settings, e.newValue == null ? false : true);
                    UIUtility.SetDisplay(noWheelJoint, e.newValue == null ? true : false);
                },
                typeof(AFWheelJoint)
            );

            UIUtility.SetDisplay(settings, component.WheelJoint == null ? false : true);
            UIUtility.SetDisplay(noWheelJoint, component.WheelJoint == null ? true : false);

            UIUtility.InitializeField
            (
                rotationRange,
                () => component.RotationRange,
                e =>
                {
                    component.RotationRange = (ContinuousServoMotor.Range)e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                timePerDegrees,
                () => component.SecondsPer60Degrees,
                e =>
                {
                    component.SecondsPer60Degrees = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                deadAngle,
                () => component.DeadAngle,
                e =>
                {
                    component.DeadAngle = e.newValue;
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

            FloatField zeroDegreePulseWidth = root.Q<FloatField>(name: "zero-degree-pulse-width");
            FloatField oneEightyDegpulseWidth = root.Q<FloatField>(name: "180-degree-pulse-width");
            FloatField deadBandWidth = root.Q<FloatField>(name: "dead-band-width");

            UIUtility.InitializeField
            (
                zeroDegreePulseWidth,
                () => component.PulseWidthDefinition.x,
                e =>
                {
                    component.PulseWidthDefinition = new Vector2(e.newValue, component.PulseWidthDefinition.y);
                }
            );

            UIUtility.InitializeField
            (
                oneEightyDegpulseWidth,
                () => component.PulseWidthDefinition.y,
                e =>
                {
                    component.PulseWidthDefinition = new Vector2(component.PulseWidthDefinition.x, e.newValue);
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
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            FloatField target = new FloatField("Target (deg)");
            target.isDelayed = true;
            UIUtility.InitializeField
            (
                target,
                () => component.Target,
                e =>
                {
                    component.Target = e.newValue;
                }
            );

            pulseWidthConrolPanel = new FloatField("Pulse Width (ms)");
            pulseWidthConrolPanel.isDelayed = true;
            UIUtility.InitializeField
            (
                pulseWidthConrolPanel,
                () => component.PulseWidth,
                e =>
                {
                    component.PulseWidth = e.newValue;
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
#pragma warning restore 0618
    }
}
