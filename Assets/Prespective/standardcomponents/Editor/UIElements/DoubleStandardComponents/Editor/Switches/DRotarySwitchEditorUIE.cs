using u040.prespective.core;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.ui.buttons.DBaseSwitch;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
    [CustomEditor(typeof(DRotarySwitch))]
    public class DRotarySwitchEditorUIE : DSwitchesEditorUIE<DRotarySwitch>
    {

        #region << Property Fields>>
        ObjectField wheelJoint;
        Toggle useSceneGizmo;
        ColorField gizmoColor;
        Button addNewState;
        #endregion
        #region << Control Panel Fields>>
        TextField selectedStateControlPanel;
        TextField idControlPanel;
        #endregion


        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Switches/DRotarySwitchLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Properties >>
            wheelJoint = root.Q<ObjectField>(name: "wheel-joint");
            useSceneGizmo = root.Q<Toggle>(name: "use-scene-gizmo");
            gizmoColor = root.Q<ColorField>(name: "gizmo-color");
            addNewState = root.Q<Button>(name: "add-new-state");

            UIUtility.InitializeField
            (
                wheelJoint,
                () => component.KinematicWheelJoint,
                e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint)e.newValue;
                },
                typeof(AWheelJoint)
            );

            UIUtility.InitializeField
            (
                useSceneGizmo,
                () => component.UseSceneGizmo,
                e =>
                {
                    component.UseSceneGizmo = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                gizmoColor,
                () => component.GizmoColor,
                e =>
                {
                    component.GizmoColor = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            addNewState.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                DSwitchState switchState = component.SaveCurrentPositionAsState();
                PersistentEditorCoroutine.StartCoroutine(redrawWindow());
                UpdateStateContainerEnabled();
            });
            #endregion
        }
        internal void OnSceneGUI()
        {
            if (component.UseSceneGizmo && component.KinematicWheelJoint != null)
            {
                //Draw arrows for state positions
                Handles.color = component.GizmoColor;

                //Position or origin
                Vector3 handleOrigin = component.KinematicWheelJoint.transform.position;

                //Size
                float size = (float)component.KinematicWheelJoint.Radius;

                //Direction of origin
                Vector3 parentForward = component.KinematicWheelJoint.transform.parent == null ? Vector3.forward : component.KinematicWheelJoint.transform.parent.transform.forward;
                Quaternion parentRotation = component.KinematicWheelJoint.transform.parent == null ? Quaternion.identity : component.KinematicWheelJoint.transform.parent.rotation;

                for (int i = 0; i < component.SwitchStates.Count; i++)
                {
                    float correctionalAngle = Vector3.SignedAngle(parentForward, component.KinematicWheelJoint.ForwardDir.GlobalVector.ToFloat(), component.KinematicWheelJoint.AxisDir.GlobalVector.ToFloat());
                    Quaternion handleDirection = Quaternion.AngleAxis(((float)component.SwitchStates[i].Position * 360f) + correctionalAngle, component.KinematicWheelJoint.AxisDir.GlobalVector.ToFloat()) * parentRotation;

                    //Draw handle
                    Handles.ArrowHandleCap(0, handleOrigin, handleDirection, size * 0.875f, EventType.Repaint);
                    Handles.Label(handleOrigin + (handleDirection * (parentForward * size)), component.SwitchStates[i].Name);

                    //Draw transition lines
                    Vector3 fromPosition = component.KinematicWheelJoint.transform.position;
                    float angle = (float)component.SwitchStates[i].UpperTransition * 360f;
                    Vector3 toVector = (component.KinematicWheelJoint.ForwardDir.GlobalVector.Normalized * component.KinematicWheelJoint.Radius).ToFloat();
                    Quaternion rotateVector = Quaternion.AngleAxis(angle, component.KinematicWheelJoint.AxisDir.GlobalVector.ToFloat());
                    toVector = rotateVector * toVector;
                    Vector3 toPosition = fromPosition + toVector;
                    Handles.DrawLine(fromPosition, toPosition);
                }
            }
        }
    }
}
