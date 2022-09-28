#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.inspector.editor;
using u040.prespective.prepair.inspector;

namespace u040.prespective.standardcomponents.kinetics.motor.linearactuator.editor
{
    public class LinearActuatorEditor : PrespectiveEditor, IControlPanel
    {
#pragma warning disable 0618

        private LinearActuator component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;

        internal void OnEnable()
        {
            component = (LinearActuator)target;
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
                    EditorGUILayout.LabelField("Target (%)", component.Target.ToString());
                    EditorGUILayout.LabelField("Position (%)", component.Position.ToString());
                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);

                    component.PrismaticJoint = (AFPrismaticJoint)EditorGUILayout.ObjectField("PrismaticJoint", component.PrismaticJoint, typeof(AFPrismaticJoint), true);
                    component.InvertPosition = EditorGUILayout.Toggle("Invert Position", component.InvertPosition);
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Extending", EditorStyles.boldLabel);
                    component.ExtendingMoveSpeed = EditorGUILayout.FloatField("Move Speed (m/s)", component.ExtendingMoveSpeed);
                    component.ExtendingCycleTime = EditorGUILayout.FloatField("Cycle Time (s)", component.ExtendingCycleTime);
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Retracting", EditorStyles.boldLabel);
                    component.RetractingMoveSpeed = EditorGUILayout.FloatField("Move Speed (m/s)", component.RetractingMoveSpeed);
                    component.RetractingCycleTime = EditorGUILayout.FloatField("Cycle Time (s)", component.RetractingCycleTime);
                    
                    EditorGUI.EndDisabledGroup();
                    break;

                case 2:
                    ControlPanelInterfaceEditor.ShowGenerationButtonForComponent(component);
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
            component.Target = EditorGUILayout.Slider("Target (%)", component.Target, 0f, 1f);
            EditorGUILayout.LabelField("Position (%)", component.Position.ToString());

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Go to Zero"))
            {
                component.Target = 0f;
            }
            if (GUILayout.Button("Go To End"))
            {
                component.Target = 1f;
            }
            EditorGUILayout.EndHorizontal();
        }
#pragma warning restore 0618
    }
}
#endif




