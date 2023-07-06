using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.sensors.position;
using u040.prespective.utility.bridge;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.virtualhardware.sensors.position.DBaseSwitch;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    [CustomEditor(typeof(DSlideSwitch))]
    public class DSlideSwitchEditorUIE : DSwitchesEditorUIE<DSlideSwitch>
    {
        #region << FIELDS >>
        //Property Fields
        private ObjectField prismaticJoint;
        private Toggle useSceneGizmo;
        private ColorField gizmoColor;
        private Button addNewState;
        
        //Control Panel Fields
        private TextField selectedStateControlPanel;
        private TextField idControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DSlideSwitchEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Properties >>
            prismaticJoint = root.Q<ObjectField>(name: "prismatic-joint");
            useSceneGizmo = root.Q<Toggle>(name: "use-scene-gizmo");
            gizmoColor = root.Q<ColorField>(name: "gizmo-color");
            addNewState = root.Q<Button>(name: "add-new-state");

            UIUtility.InitializeField
            (
                prismaticJoint,
                component,
                () => component.KinematicPrismaticJoint,
                e =>
                {
                    component.KinematicPrismaticJoint = (APrismaticJoint)e.newValue;
                },
                typeof(APrismaticJoint)
            );

            UIUtility.InitializeField
            (
                useSceneGizmo,
                component,
                () => component.UseSceneGizmo,
                e =>
                {
                    component.UseSceneGizmo = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                gizmoColor,
                component,
                () => component.GizmoColor,
                e =>
                {
                    component.GizmoColor = e.newValue;
                }
            );

            addNewState.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                DSwitchState switchState = component.SaveState(component.KinematicPrismaticJoint.CurrentPercentage);
                EditorCoroutines.StartEditorCoroutine(redrawWindow());
                updateStateContainerEnabled();
            });
            #endregion
        }

        internal void OnSceneGUI()
        {
            if (component.UseSceneGizmo && component.KinematicPrismaticJoint && component.KinematicPrismaticJoint.ConstrainingSpline)
            {
                Handles.color = component.GizmoColor;
                float handleSize = HandleUtility.GetHandleSize(component.KinematicPrismaticJoint.ConstrainingSpline.transform.position);

                if (component.SwitchStates.Count > 0)
                {
                    //Draw the first previous transition point manually
                    float firstTransition = (float)component.SwitchStates[0].LowerTransition;
                    Vector3 firstTransitionPoint = component.KinematicPrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(firstTransition).ToFloat();
                    Handles.SphereHandleCap(0, firstTransitionPoint, Quaternion.identity, handleSize * 0.10f, EventType.Repaint);

                    for (int i = 0; i < component.SwitchStates.Count; i++)
                    {
                        //Draw the position of each switch state
                        DSwitchState state = component.SwitchStates[i];
                        float position = (float)state.Position;
                        Vector3 pointPosition = component.KinematicPrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(position).ToFloat();
                        Handles.SphereHandleCap(0, pointPosition, Quaternion.identity, handleSize * 0.25f, EventType.Repaint);

                        //Draw all next transition dots for each state
                        float transitionPoint = (float)state.UpperTransition;
                        Vector3 transitionPointPosition = component.KinematicPrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(transitionPoint).ToFloat();
                        Handles.SphereHandleCap(0, transitionPointPosition, Quaternion.identity, handleSize * 0.10f, EventType.Repaint);
                    }
                }


                //Draw a dot on current prismatic position
                Handles.color = new Color(1f - component.GizmoColor.r, 1f - component.GizmoColor.g, 1f - component.GizmoColor.b); //Inverted gizmo color
                float prismaticPosition = (float)component.KinematicPrismaticJoint.CurrentPercentage;
                Vector3 prismaticPoint = component.KinematicPrismaticJoint.ConstrainingSpline.GetPointAtEquidistantPerc(prismaticPosition).ToFloat();
                Handles.SphereHandleCap(0, prismaticPoint, Quaternion.identity, handleSize * 0.20f, EventType.Repaint);
            }
        }
    }
}
