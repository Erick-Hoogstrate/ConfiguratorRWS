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
    public class DLimitedServoMotorEditor : PrespectiveEditor, IControlPanel
    {
        private DLimitedServoMotor component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;

        internal void OnEnable()
        {
            component = (DLimitedServoMotor)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
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
                    this.component.KinematicWheelJoint = (DWheelJoint)EditorGUILayout.ObjectField("Kinematic Wheel Joint", component.KinematicWheelJoint, typeof(DWheelJoint), true);
                    component.RotationRange = (DContinuousServoMotor.Range)EditorGUILayout.EnumPopup("Rotation Range", component.RotationRange);
                    component.SecondsPer60Degrees = EditorGUILayout.DelayedDoubleField("Seconds per 60 degrees", component.SecondsPer60Degrees);
                    component.Damping = EditorGUILayout.DelayedDoubleField("Damping", component.Damping);
                    component.DeadAngle = EditorGUILayout.DelayedDoubleField("Dead Angle (deg)", component.DeadAngle);

                    EditorGUILayout.Space();

                    component.EnablePWM = EditorGUILayout.Toggle("Enable PWM", component.EnablePWM);
                    if (component.EnablePWM)
                    {
                        EditorGUI.indentLevel++;
                        component.PulseWidthDefinition.X = EditorGUILayout.DelayedDoubleField("0 deg Pulse Width (ms)", component.PulseWidthDefinition.X);
                        component.PulseWidthDefinition.Y = EditorGUILayout.DelayedDoubleField((double)component.RotationRange + " deg Pulse Width (ms)", component.PulseWidthDefinition.Y);
                        component.DeadBandWidth = EditorGUILayout.DelayedDoubleField("Dead Band Width (ms)", component.DeadBandWidth);
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


        public void ShowControlPanel()
        {
            component.Target = EditorGUILayout.DelayedDoubleField("Target (deg)", component.Target);
            if (component.EnablePWM)
            {
                component.PulseWidth = EditorGUILayout.DelayedDoubleField("Pulse Width (ms)", component.PulseWidth);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Position (deg)", component.Position.ToString());
        }
    }
}
#endif
