#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using u040.prespective.prepair.kinematics;
using u040.prespective.math;
using u040.prespective.prepair.inspector;
using System.Reflection;
using u040.prespective.core.editor;
using System.Collections.Generic;
using static u040.prespective.prepair.ui.buttons.BaseSwitch;
using u040.prespective.prepair.inspector.editor;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
#pragma warning disable 0618

    public class RotarySwitchEditor : PrespectiveEditor, IControlPanel
    {
        private RotarySwitch component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;
        private List<bool> foldoutList = new List<bool>();

        internal void OnEnable()
        {
            component = (RotarySwitch)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");

            for (int i = 0; i < component.SwitchStates.Count; i++)
            {
                foldoutList.Add(false);
            }
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
                    // current state
                    string label = "N/A";
                    if (Application.isPlaying && component.SelectedState != null)
                    {
                        string stateName = (component.SelectedState.Name != null && component.SelectedState.Name != "") ? component.SelectedState.Name : "State";
                        int id = component.SelectedState.Id;
                        label = "[" + id + "] - " + stateName;
                    }
                    EditorGUILayout.LabelField("Selected State", label);
                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    component.WheelJoint = (AFWheelJoint)EditorGUILayout.ObjectField("Wheel Joint", component.WheelJoint, typeof(AFWheelJoint), true);

                    //Get the serialized property of the switch states list
                    SerializedProperty serializedSwitchStatesList = soTarget.FindProperty("SwitchStates");

                    EditorGUI.indentLevel++;

                    //Display message of no switch states have been assigned
                    if (component.SwitchStates.Count == 0)
                    {
                        EditorGUILayout.LabelField("No Switch States available", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic });
                    }

                    //For each switch state
                    for (int i = 0; i < component.SwitchStates.Count; i++)
                    {
                        //Buffer switch state
                        RotarySwitch.SwitchState switchState = component.SwitchStates[i];

                        //Show foldout with summary of information like ID, name and positon
                        EditorGUILayout.BeginHorizontal();
                        foldoutList[i] = EditorGUILayout.Foldout(foldoutList[i], "[" + switchState.Id + "] " + switchState.Name + ": " + switchState.Position, true);
                        if (GUILayout.Button("Select", new GUIStyle(GUI.skin.button) { stretchWidth = false }))
                        {
                            component.SelectState(i);
                        }
                        EditorGUILayout.EndHorizontal();

                        //If foldout
                        if (foldoutList[i])
                        {
                            //Show Id and Delete buttons next to each other
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("ID", switchState.Id.ToString());
                            if (GUILayout.Button("Delete", new GUIStyle(GUI.skin.button) { stretchWidth = false }))
                            {
                                Undo.RecordObject(component, "Switch states");
                                component.DeleteState(i);
                                foldoutList.RemoveAt(i);
                            }
                            EditorGUILayout.EndHorizontal();

                            //Name field
                            switchState.Name = EditorGUILayout.TextField("Name", switchState.Name);

                            //Previous Weight
                            float newPreviousWeight = Mathf.Max(EditorGUILayout.FloatField("Lower Weight", switchState.LowerWeight), 0f);
                            if (switchState.LowerWeight != newPreviousWeight)
                            {
                                switchState.LowerWeight = newPreviousWeight;
                                component.RecalculateTransitions();
                            }

                            //Position field
                            float newPosition = PreSpectiveMath.LimitMinMax(EditorGUILayout.DelayedFloatField("Position", switchState.Position), 0f, 1f) % 1f;
                            
                            //If position has changed, sort list by position
                            if (switchState.Position != newPosition)
                            {
                                switchState.Position = newPosition;
                                component.RecalculateTransitions();
                            }

                            //Next Weight
                            float newNextWeight = Mathf.Max(EditorGUILayout.FloatField("Upper Weight", switchState.UpperWeight), 0f);
                            if (switchState.UpperWeight != newNextWeight)
                            {
                                switchState.UpperWeight = newNextWeight;
                                component.RecalculateTransitions();
                            }

                            //Get serialized property of switch state from serialized list
                            SerializedProperty serializedSwitchState = serializedSwitchStatesList.GetArrayElementAtIndex(i);
                            
                            //Get serialized property of UnityEvents from serialized switch state
                            SerializedProperty serializedOnSelectedEvent = serializedSwitchState.FindPropertyRelative("OnSelected");
                            SerializedProperty serializedOnUnselectedEvent = serializedSwitchState.FindPropertyRelative("OnUnselected");

                            //Display serialized UnityEvent properties
                            EditorGUILayout.PropertyField(serializedOnSelectedEvent, true);
                            EditorGUILayout.PropertyField(serializedOnUnselectedEvent, true);
                            EditorGUILayout.Space();
                        }
                    }

                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    if (!Application.isPlaying && GUILayout.Button("Save current position as state"))
                    {
                        SwitchState newState = component.SaveCurrentPositionAsState();

                        if (newState != null)
                        {
                            foldoutList.Insert(newState.Id, true);
                        }
                        else
                        {
                            Debug.LogWarning("Cannot save current position as state." + (component.WheelJoint == null ? " Assign a WheelJoint first." : ""));
                        }
                    }

                    //Gizmo settings
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    component.UseSceneGizmo = EditorGUILayout.Toggle("Use Scene Gizmo", component.UseSceneGizmo);
                    component.GizmoColor = EditorGUILayout.ColorField(component.GizmoColor);
                    EditorGUILayout.EndHorizontal();

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

        internal void OnSceneGUI()
        {
            if (component.UseSceneGizmo && component.WheelJoint != null)
            {
                //Draw arrows for state positions
                Handles.color = component.GizmoColor;

                //Position or origin
                Vector3 handleOrigin = component.WheelJoint.transform.position;

                //Size
                float size = component.WheelJoint.Radius;

                //Direction of origin
                Vector3 parentForward = component.WheelJoint.transform.parent == null ? Vector3.forward : component.WheelJoint.transform.parent.transform.forward;
                Quaternion parentRotation = component.WheelJoint.transform.parent == null ? Quaternion.identity : component.WheelJoint.transform.parent.rotation;

                for (int i = 0; i < component.SwitchStates.Count; i++)
                {
                    float correctionalAngle = Vector3.SignedAngle(parentForward, component.WheelJoint.ForwardDir.GlobalVector, component.WheelJoint.AxisDir.GlobalVector);
                    Quaternion handleDirection = Quaternion.AngleAxis((component.SwitchStates[i].Position * 360f) + correctionalAngle, component.WheelJoint.AxisDir.GlobalVector) * parentRotation;

                    //Draw handle
                    Handles.ArrowHandleCap(0, handleOrigin, handleDirection, size * 0.875f, EventType.Repaint);
                    Handles.Label(handleOrigin + (handleDirection * (parentForward * size)), component.SwitchStates[i].Name);

                    //Draw transition lines
                    Vector3 fromPosition = component.WheelJoint.transform.position;                    
                    float angle = component.SwitchStates[i].UpperTransition * 360f;
                    Vector3 toVector = component.WheelJoint.ForwardDir.GlobalVector.normalized * component.WheelJoint.Radius;
                    Quaternion rotateVector = Quaternion.AngleAxis(angle, component.WheelJoint.AxisDir.GlobalVector);
                    toVector = rotateVector * toVector;
                    Vector3 toPosition = fromPosition + toVector;
                    Handles.DrawLine(fromPosition, toPosition);
                }
            }
        }

        public void ShowControlPanel()
        {
            EditorGUILayout.LabelField("Selected State", component.SelectedState != null && component.SelectedState.Name != "" && Application.isPlaying ? component.SelectedState.Name.ToString() : "N/A");
            EditorGUILayout.LabelField("ID", component.SelectedState != null && Application.isPlaying ? component.SelectedState.Id.ToString() : "N/A");
        }
#pragma warning restore 0618
    }
}
#endif
