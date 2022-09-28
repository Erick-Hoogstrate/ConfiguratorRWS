#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using u040.prespective.utility.editor;
using u040.prespective.prepair.inspector;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.inspector.editor;

namespace u040.prespective.standardcomponents.kinetics.motor.steppermotor.editor
{
    public class DrivenStepperMotorEditor : PrespectiveEditor, IControlPanel
    {
#pragma warning disable 0618

        private DrivenStepperMotor component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;

        internal void OnEnable()
        {
            component = (DrivenStepperMotor)target;
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
                    EditorGUILayout.LabelField("Preferred Velocity (deg/s)", component.PreferredVelocity.ToString());
                    EditorGUILayout.LabelField("Target Step", component.TargetStep.ToString());
                    EditorGUILayout.LabelField("Continuous", component.Continuous.ToString());
                    EditorGUILayout.LabelField("Continuous Direction", component.ContinuousDirection.ToString());

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Velocity (deg/s)", component.Velocity.ToString());
                    EditorGUILayout.LabelField("Position (deg)", component.Position.ToString());
                    EditorGUILayout.LabelField("Position (step)", component.PositionSteps.ToString());
                    GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.normal.textColor = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
                    labelStyle.fontStyle = FontStyle.Bold;
                    EditorGUILayout.LabelField("State", component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive"), labelStyle);
                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    component.WheelJoint = (AFWheelJoint)EditorGUILayout.ObjectField("Wheel Joint", component.WheelJoint, typeof(AFWheelJoint), true);
                    component.StepsPerCycle = EditorGUILayout.DelayedIntField("Step Count", component.StepsPerCycle);
                    component.MaxVelocity = EditorGUILayout.DelayedFloatField("Max Velocity (deg/s)", component.MaxVelocity);
                    component.Acceleration = EditorGUILayout.DelayedFloatField("Acceleration (deg/s/s)", component.Acceleration);
                    component.ZeroOffset = EditorGUILayout.DelayedFloatField("Zero Offset (deg)", component.ZeroOffset);
                    if (GUILayout.Button("Set current position as zero offset"))
                    {
                        component.ZeroOffset = component.WheelJoint.CurrentRevolutionDegrees;
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
            component.PreferredVelocity = EditorGUILayout.Slider("Preferred Velocity (deg/s)", component.PreferredVelocity, 0f, component.MaxVelocity);
            component.TargetStep = EditorGUILayout.DelayedIntField("Target Step", component.TargetStep);
            component.Continuous = EditorGUILayout.Toggle("Continuous", component.Continuous);
            component.ContinuousDirection = (DrivenStepperMotor.Direction)EditorGUILayout.EnumPopup("Continuous Direction", component.ContinuousDirection);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Velocity (deg/s)", component.Velocity.ToString());
            EditorGUILayout.LabelField("Position (deg)", component.PositionDegrees.ToString());
            EditorGUILayout.LabelField("Position (step)", component.PositionSteps.ToString());
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.normal.textColor = (component.IsActive && !component.Error) ? new Color(0f, 0.5f, 0f) : Color.red;
            labelStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("State", component.Error ? "Error" : (component.IsActive ? "Active" : "Inactive"), labelStyle);
            if (GUILayout.Button(component.Error ? "Reset Error" : (component.IsActive ? "Stop" : "Start")))
            {
                if (component.Error)
                {
                    component.ResetError();
                }
                else
                {
                    component.IsActive = !component.IsActive;
                }
                GUI.FocusControl(null);
            }
            EditorGUILayout.EndHorizontal();
        }
#pragma warning restore 0618
    }
}
#endif
