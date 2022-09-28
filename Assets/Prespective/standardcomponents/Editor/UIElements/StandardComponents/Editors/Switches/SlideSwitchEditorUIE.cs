using System.Reflection;
using u040.prespective.core;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.ui.buttons.BaseSwitch;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(SlideSwitch))]
    public class SlideSwitchEditorUIE : SwitchesEditorUIE<SlideSwitch>
    {
        #region << Live Data Fields >>
        TextField selectedState;
        #endregion
        #region << Property Fields>>
        ObjectField prismaticJoint;
        Toggle useSceneGizmo;
        ColorField gizmoColor;
        Button addNewState;
        #endregion
        #region << Control Panel Fields>>
        TextField selectedStateControlPanel;
        TextField idControlPanel;
        #endregion

        string noSwitchStatesText = "N/A";

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Switches/SlideSwitchLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            if (component.SelectedState == null || component.SelectedState.Name == "")
            {
                selectedState.value = noSwitchStatesText;
            }
            else
            {
                selectedState.value = component.SelectedState.Name;
            }
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

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            selectedState = root.Q<TextField>(name: "selected-state");
            selectedState.isReadOnly = true;
            #endregion
            #region << Properties >>
            prismaticJoint = root.Q<ObjectField>(name: "prismatic-joint");
            useSceneGizmo = root.Q<Toggle>(name: "use-scene-gizmo");
            gizmoColor = root.Q<ColorField>(name: "gizmo-color");
            addNewState = root.Q<Button>(name: "add-new-state");

            UIUtility.InitializeField
            (
                prismaticJoint,
                () => component.PrismaticJoint,
                e =>
                {
                    component.PrismaticJoint = (AFPrismaticJoint)e.newValue;
                },
                typeof(AFPrismaticJoint)
            );

            UIUtility.InitializeField
            (
                useSceneGizmo,
                () => component.UseSceneGizmo,
                e =>
                {
                    component.UseSceneGizmo = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                gizmoColor,
                () => component.GizmoColor,
                e =>
                {
                    component.GizmoColor = e.newValue;
                }
            );

            addNewState.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                SwitchState switchState = component.SaveState(component.PrismaticJoint.CurrentPerc);
                PersistentEditorCoroutine.StartCoroutine(redrawWindow());
            });
            #endregion
        }
#pragma warning restore 0618
    }
}
