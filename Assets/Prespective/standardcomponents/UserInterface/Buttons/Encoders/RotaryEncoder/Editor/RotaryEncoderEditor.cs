#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using u040.prespective.utility.editor;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.inspector;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.inspector.editor;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
#pragma warning disable 0618

    public class RotaryEncoderEditor : PrespectiveEditor, IControlPanel
    {
        private RotaryEncoder component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;
        private SerializedProperty onValueChanged;

        private void OnEnable()
        {
            component = (RotaryEncoder)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
            onValueChanged = soTarget.FindProperty("onValueChanged");
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
                    EditorGUILayout.LabelField("Value", Application.isPlaying ? component.OutputSignal.ToString() : "N/A");
                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    component.WheelJoint = (AFWheelJoint)EditorGUILayout.ObjectField("Wheel Joint", component.WheelJoint, typeof(AFWheelJoint), true);
                    component.ValuePerWholeCycle = EditorGUILayout.DelayedFloatField("Value Per Rotation", component.ValuePerWholeCycle);
                    component.BaseValue = EditorGUILayout.FloatField("Base Value", component.BaseValue);
                    component.EnableRounding = EditorGUILayout.Toggle("Enable Rounding", component.EnableRounding);
                    if (component.EnableRounding)
                    {
                        EditorGUI.indentLevel++;
                        component.RoundingInterval = EditorGUILayout.DelayedFloatField("Rounding Interval", component.RoundingInterval);
                        EditorGUI.indentLevel--;
                    }
                    component.CapValue = EditorGUILayout.Toggle("Cap Value", component.CapValue);
                    if (component.CapValue)
                    {
                        EditorGUI.indentLevel++;
                        component.MinCapValue = EditorGUILayout.DelayedFloatField("Minimum Value", component.MinCapValue);
                        component.MaxCapValue = EditorGUILayout.DelayedFloatField("Maximum Value", component.MaxCapValue);
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(onValueChanged, true);
                    EditorGUI.EndDisabledGroup();
                    break;

                case 2:
                    ControlPanelInterfaceEditor.ShowGenerationButtonForComponent(component);
                    break;
            }

            EditorUtility.SetDirty(target); //Make sure inspector updates and repaints properly 

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties(); 
            }
            base.OnInspectorGUI();
        }

        public void ShowControlPanel()
        {
            EditorGUILayout.LabelField("Value", Application.isPlaying ? component.OutputSignal.ToString() : "N/A");
        }
#pragma warning restore 0618
    }
}
#endif
