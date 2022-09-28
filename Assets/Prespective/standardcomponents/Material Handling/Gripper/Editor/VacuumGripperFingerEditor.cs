#if UNITY_EDITOR
using UnityEditor;
using u040.prespective.utility.editor;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using UnityEngine;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    public class VacuumGripperFingerEditor : PrespectiveEditor
    {
#pragma warning disable 0618

        private VacuumGripperFinger component;
        private SerializedObject soTarget;

        private void OnEnable()
        {
            component = (VacuumGripperFinger)target;
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
