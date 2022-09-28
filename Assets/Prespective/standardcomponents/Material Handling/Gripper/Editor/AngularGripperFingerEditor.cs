#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using UnityEngine;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    public class AngularGripperFingerEditor : PrespectiveEditor
    {
#pragma warning disable 0618

        private AngularGripperFinger component;
        private SerializedObject soTarget;

        private void OnEnable()
        {
            component = (AngularGripperFinger)target;
            soTarget = new SerializedObject(target);
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
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            component.WheelJoint = (AFWheelJoint)EditorGUILayout.ObjectField("Wheel Joint", component.WheelJoint, typeof(AFWheelJoint), true);
            EditorGUI.indentLevel++;
            component.LowerLimit = EditorGUILayout.FloatField("Lower Limit (deg)", component.LowerLimit);
            component.UpperLimit = EditorGUILayout.FloatField("Upper Limit (deg)", component.UpperLimit);
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();

            component.ShowBaseInspector();

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties();
            }
            base.OnInspectorGUI();
        }
#pragma warning restore 0618
    }
}
#endif
