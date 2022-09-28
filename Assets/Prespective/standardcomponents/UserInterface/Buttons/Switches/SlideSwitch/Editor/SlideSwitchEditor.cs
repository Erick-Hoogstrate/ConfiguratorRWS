#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.inspector;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.math;
using System.Collections.Generic;
using static u040.prespective.prepair.ui.buttons.BaseSwitch;
using u040.prespective.prepair.inspector.editor;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
    public class SlideSwitchEditor : PrespectiveEditor, IControlPanel
    {
#pragma warning disable 0618

        private SlideSwitch component;
        private SerializedObject soTarget;
        private SerializedProperty toolbarTab;
        private List<bool> foldoutList = new List<bool>();

        internal void OnEnable()
        {
            component = (SlideSwitch)target;
            soTarget = new SerializedObject(target);
            toolbarTab = soTarget.FindProperty("toolbarTab");

            for(int i = 0; i < component.SwitchStates.Count; i++)
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
                    if (Application.isPlaying && component.SelectedState != null )
                    {
                        string stateName = (component.SelectedState.Name != null && component.SelectedState.Name != "") ? component.SelectedState.Name : "State";
                        int id = component.SelectedState.Id;
                        label = "[" + id + "] - " + stateName;
                    }
                    EditorGUILayout.LabelField("Selected State", label);

                    break;

                case 1:
                    EditorGUI.BeginDisabledGroup(Application.isPlaying);
                    component.PrismaticJoint = (AFPrismaticJoint)EditorGUILayout.ObjectField("Prismatic Joint", component.PrismaticJoint, typeof(AFPrismaticJoint), true);
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
                        SlideSwitch.SwitchState switchState = component.SwitchStates[i];

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
                            float newPosition = PreSpectiveMath.LimitMinMax(EditorGUILayout.DelayedFloatField("Position", switchState.Position), 0f, 1f);

                            if (component.LoopingSwitch) { newPosition = newPosition % 1f; }

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


                    //State save button
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    if (!Application.isPlaying && GUILayout.Button("Add new state"))
                    {
                        SwitchState newState = component.SaveCurrentPositionAsState();

                        if (newState != null)
                        {
                            foldoutList.Insert(newState.Id, true);
                        }
                        else
                        {
                            Debug.LogWarning("Cannot save current position as state." + (component.PrismaticJoint == null ? " Assign a PrismaticJoint first." : ""));
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
            if (component.UseSceneGizmo && component.PrismaticJoint && component.PrismaticJoint.ConstrainingSpline)
            {
                Handles.color = component.GizmoColor;
                float handleSize = HandleUtility.GetHandleSize(component.PrismaticJoint.ConstrainingSpline.transform.position);


                if (component.SwitchStates.Count > 0)
                {                
                    //Draw the first previous transition point manually
                    float firstTransition = component.SwitchStates[0].LowerTransition;
                    Vector3 firstTransitionPoint = component.PrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(firstTransition);
                    Handles.SphereHandleCap(0, firstTransitionPoint, Quaternion.identity, handleSize * 0.10f, EventType.Repaint);

                    for (int i = 0; i < component.SwitchStates.Count; i++)
                    {
                        //Draw the position of each switch state
                        SlideSwitch.SwitchState state = component.SwitchStates[i];
                        float position = state.Position;
                        Vector3 pointPosition = component.PrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(position);
                        Handles.SphereHandleCap(0, pointPosition, Quaternion.identity, handleSize * 0.25f, EventType.Repaint);

                        //Draw all next transition dots for each state
                        float transitionPoint = state.UpperTransition;
                        Vector3 transitionPointPosition = component.PrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(transitionPoint);
                        Handles.SphereHandleCap(0, transitionPointPosition, Quaternion.identity, handleSize * 0.10f, EventType.Repaint);
                    }
                }


                //Draw a dot on current prismatic position
                Handles.color = new Color(1f - component.GizmoColor.r, 1f - component.GizmoColor.g, 1f - component.GizmoColor.b); //Inverted gizmo color
                float prismaticPosition = component.PrismaticJoint.CurrentPerc;
                Vector3 prismaticPoint = component.PrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(prismaticPosition);
                Handles.SphereHandleCap(0, prismaticPoint, Quaternion.identity, handleSize * 0.20f, EventType.Repaint);
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
