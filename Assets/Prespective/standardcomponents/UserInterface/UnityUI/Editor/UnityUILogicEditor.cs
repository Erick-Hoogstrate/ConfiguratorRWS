#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using u040.prespective.utility.editor;
using System.Reflection;
using u040.prespective.core.editor;

namespace u040.prespective.standardcomponents.userinterface.unityui.editor
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = true, StripAfterObfuscation = false)]
    [CustomEditor(typeof(UnityUILogic))]
    public class UnityUILogicEditor : PrespectiveEditor
    {
        private UnityUILogic component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;
        private SerializedProperty signalNamingRuleOverrides;
        private SerializedProperty implicitNamingRule;
        
        private void OnEnable()
        {
            component = (UnityUILogic)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");
            signalNamingRuleOverrides = soTarget.FindProperty("signalNamingRuleOverrides");
            implicitNamingRule = soTarget.FindProperty("implicitNamingRule");
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
            toolbarTab.intValue = GUILayout.Toolbar(toolbarTab.intValue, new string[] { "Properties", "I/O", "Settings", "Debugging" });

            switch (toolbarTab.intValue)
            {
                case 0:
                    EditorGUILayout.LabelField("Inputs", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });

                    EditorGUI.indentLevel++;
                    
                    if (component.SignalNames.Count > 0)
                    {
                        for (int _i = 0; _i < component.SignalNames.Count;  _i++)
                        {
                            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("Trigger", new GUIStyle(GUI.skin.button) { margin = new RectOffset(30, 0, 0, 0) }, GUILayout.ExpandWidth(false)))
                            {
                                component.SendSignal(_i);
                            }
                            EditorGUI.EndDisabledGroup();

                            EditorGUI.BeginDisabledGroup(Application.isPlaying);
                            EditorGUILayout.LabelField(_i.ToString() + ":", new GUIStyle(GUI.skin.label) { margin = new RectOffset(0, 0, 0, 0), wordWrap = true, fontStyle = FontStyle.Bold });
                            component.SignalNames[_i] = EditorGUILayout.TextField(component.SignalNames[_i], GUILayout.ExpandWidth(true)).Replace(" ", "_");

                            if (GUILayout.Button("Delete", new GUIStyle(GUI.skin.button) { margin = new RectOffset(30, 0, 0, 0) }, GUILayout.ExpandWidth(false)))
                            {
                                component.SignalNames.RemoveAt(_i);
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
                        List<string> _tmpList = new List<string>(component.SignalNames);
                        _tmpList.Add("New Input");
                        component.SignalNames = _tmpList;
                    }
                    EditorGUI.EndDisabledGroup();
                    break;

                case 1:
                    EditorGUILayout.LabelField("Last triggered", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
                    EditorGUI.indentLevel++;
                    
                    if (component.SignalNames.Count > 0)
                    {
                        for (int _i = 0; _i < component.SignalNames.Count; _i++)
                        {
                            //TODO: Check default timestamp
                            EditorGUILayout.LabelField(component.SignalNames[_i], component.LastTriggeredTime[_i].ToString() == component.DefaultTimestamp ? "Not yet triggered" : component.LastTriggeredTime[_i].ToString());
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

                case 3:
                    component.Verbose = EditorGUILayout.Toggle("VERBOSE", component.Verbose);
                    component.GUIShowSignalsForDebugging = EditorGUILayout.Toggle("UX Show Signals For Debugging", component.GUIShowSignalsForDebugging);
                    component.GUITextOffset = EditorGUILayout.Vector2Field("UX Text Offset", component.GUITextOffset);
                    component.GUITextColor = EditorGUILayout.ColorField("UX Text Color", component.GUITextColor);
                    component.GUITextSize = EditorGUILayout.IntField("UX Text Size", component.GUITextSize);
                    component.GUITextLineSpacing = EditorGUILayout.IntField("UX Text Line Spacing", component.GUITextLineSpacing);
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                soTarget.ApplyModifiedProperties();
            }

            base.OnInspectorGUI();
        }

    }
}
#endif
