using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using u040.prespective.core.editor.editorui.inspectorwindow;

namespace u040.prespective.standardcomponents.logic.editor
{
    [CustomEditor(typeof(UnityUILogic))]
    public class UnityUILogicEditor : PrespectiveEditorIMGUI
    {
        private UnityUILogic component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;
        private SerializedProperty signalNamingRuleOverrides;
        private SerializedProperty implicitNamingRule;
        
        internal void OnEnable()
        {
            component = (UnityUILogic)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
            signalNamingRuleOverrides = soTarget.FindProperty("signalNamingRuleOverrides");
            implicitNamingRule = soTarget.FindProperty("implicitNamingRule");
        }

        public override void DrawInspectorGUI()
        {
            if (licenseState == false)
            {
                EditorGUILayout.LabelField("No Prespective License Found", EditorStyles.boldLabel);
                return;
            }

            soTarget.Update();

            EditorGUI.BeginChangeCheck();
            toolbarTab.intValue = GUILayout.Toolbar(toolbarTab.intValue, new string[] { "Properties", "I/O", "Settings"});

            switch (toolbarTab.intValue)
            {
                case 0:
                    EditorGUILayout.LabelField("Inputs", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });

                    EditorGUI.indentLevel++;
                    
                    if (component.SignalNames.Count > 0)
                    {
                        for (int i = 0; i < component.SignalNames.Count;  i++)
                        {
                            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("Trigger", new GUIStyle(GUI.skin.button) { margin = new RectOffset(30, 0, 0, 0) }, GUILayout.ExpandWidth(false)))
                            {
                                component.SendSignal(i);
                            }
                            EditorGUI.EndDisabledGroup();

                            EditorGUI.BeginDisabledGroup(Application.isPlaying);
                            EditorGUILayout.LabelField(i.ToString() + ":", new GUIStyle(GUI.skin.label) { margin = new RectOffset(0, 0, 0, 0), wordWrap = true, fontStyle = FontStyle.Bold });
                            component.SignalNames[i] = EditorGUILayout.TextField(component.SignalNames[i], GUILayout.ExpandWidth(true)).Replace(" ", "_");

                            if (GUILayout.Button("Delete", new GUIStyle(GUI.skin.button) { margin = new RectOffset(30, 0, 0, 0) }, GUILayout.ExpandWidth(false)))
                            {
                                component.SignalNames.RemoveAt(i);
                            }
                            GUILayout.EndHorizontal();
                            EditorGUI.EndDisabledGroup();
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No inputs defined", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic });
                    }
                    EditorGUI.indentLevel--;

                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    if (GUILayout.Button("Add New Input"))
                    {
                        List<string> tmpList = new List<string>(component.SignalNames);
                        tmpList.Add("New Input");
                        component.SignalNames = tmpList;
                    }
                    EditorGUI.EndDisabledGroup();
                    break;

                case 1:
                    EditorGUILayout.LabelField("Last triggered", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
                    EditorGUI.indentLevel++;
                    
                    if (component.SignalNames.Count > 0)
                    {
                        for (int i = 0; i < component.SignalNames.Count; i++)
                        {
                            //TODO: Check default timestamp
                            EditorGUILayout.LabelField(component.SignalNames[i], component.LastTriggeredTime[i].ToString() == component.DefaultTimestamp ? "Not yet triggered" : component.LastTriggeredTime[i].ToString());
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No inputs defined", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic });
                    }
                    EditorGUI.indentLevel--;
                    break;

                case 2:
                    EditorGUILayout.PropertyField(signalNamingRuleOverrides, true);
                    EditorGUILayout.PropertyField(implicitNamingRule, true);

                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties();
            }
        }
    }
}
