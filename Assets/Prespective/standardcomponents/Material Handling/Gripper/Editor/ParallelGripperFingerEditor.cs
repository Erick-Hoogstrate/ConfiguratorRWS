#if UNITY_EDITOR
using UnityEditor;
using u040.prespective.utility.editor;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using UnityEngine;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    public class ParallelGripperFingerEditor : PrespectiveEditor
    {
#pragma warning disable 0618

        private ParallelGripperFinger component;
        private SerializedObject soTarget;

        private void OnEnable()
        {
            component = (ParallelGripperFinger)target;
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
            component.PrismaticJoint = (AFPrismaticJoint)EditorGUILayout.ObjectField("Prismatic Joint", component.PrismaticJoint, typeof(AFPrismaticJoint), true);
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