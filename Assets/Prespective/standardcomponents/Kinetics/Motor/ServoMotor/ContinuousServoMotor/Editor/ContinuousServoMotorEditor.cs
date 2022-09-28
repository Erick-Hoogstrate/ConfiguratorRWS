#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using u040.prespective.utility.editor;
using u040.prespective.prepair.inspector;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.inspector.editor;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor.editor
{
    public class ContinuousServoMotorEditor : PrespectiveEditor, IControlPanel
    {
#pragma warning disable 0618

        private ContinuousServoMotor component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;

        internal void OnEnable()
        {
            component = (ContinuousServoMotor)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
            //
        }

        public override void OnInspectorGUI()
        {
            if (licenseState == false)
            {
                EditorGUILayout.LabelField("No Prespective License Found", EditorStyles.boldLabel);
                return;
            }

            soTarget.Update();
            //DrawDefaultInspector();

            EditorGUI.BeginChangeCheck();
            toolbarTab.intValue = GUILayout.Toolbar(toolbarTab.intValue, new string[] { "Live Data", "Properties", "Control Panel" });

            switch (toolbarTab.intValue)
            {
                case 0:
                    EditorGUILayout.LabelField("Target (deg)", component.Target.ToString());
                    if (component.EnablePWM)
                    {
                        EditorGUILayout.LabelField("Pulse Width (ms)", component.PulseWidth.ToString());
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Position (deg)", component.Position.ToString());
                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    this.component.WheelJoint = (AFWheelJoint)EditorGUILayout.ObjectField("Wheel Joint", component.WheelJoint, typeof(AFWheelJoint), true);
                    component.RotationRange = (ContinuousServoMotor.Range)EditorGUILayout.EnumPopup("Rotation Range", component.RotationRange);
                    component.SecondsPer60Degrees = EditorGUILayout.DelayedFloatField("Seconds per 60 degrees", component.SecondsPer60Degrees);
                    component.DeadAngle = EditorGUILayout.DelayedFloatField("Dead Angle (deg)", component.DeadAngle);

                    EditorGUILayout.Space();

                    component.EnablePWM = EditorGUILayout.Toggle("Enable PWM", component.EnablePWM);
                    if (component.EnablePWM)
                    {
                        EditorGUI.indentLevel++;
                        component.PulseWidthDefinition.x = EditorGUILayout.DelayedFloatField("0 deg Pulse Width (ms)", component.PulseWidthDefinition.x);
                        component.PulseWidthDefinition.y = EditorGUILayout.DelayedFloatField("180 deg Pulse Width (ms)", component.PulseWidthDefinition.y);
                        component.DeadBandWidth = EditorGUILayout.DelayedFloatField("Dead Band Width (ms)", component.DeadBandWidth);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.EndDisabledGroup();
                    break;

                case 2:
                    EditorGUILayout.Space();
                    ControlPanelInterfaceEditor.ShowGenerationButtonForComponent(component);
                    EditorGUILayout.Space();
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties();
            }
            base.OnInspectorGUI();
        }

        public virtual void ShowControlPanel()
        {
            component.Target = EditorGUILayout.FloatField("Target (deg)", component.Target);
            if (component.EnablePWM)
            {
                component.PulseWidth = EditorGUILayout.DelayedFloatField("Pulse Width (ms)", component.PulseWidth);
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Position (deg)", component.Position.ToString());
        }
#pragma warning restore 0618
    }
}
#endif
