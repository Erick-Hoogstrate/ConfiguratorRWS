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
    [CustomEditor(typeof(DRotarySwitch))]
    public class DRotarySwitchEditorUIE : DSwitchesEditorUIE<DRotarySwitch>
    {
        #region << FIELDS >>
        //Property Fields
        private ObjectField wheelJointField;
        private Toggle useSceneGizmoToggle;
        private ColorField gizmoColorField;
        private Button addNewStateButton;
        
        //Control Panel Fields>>
        private TextField selectedStateControlPanel;
        private TextField idControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DRotarySwitchEditorLayout";
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
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint");
            useSceneGizmoToggle = root.Q<Toggle>(name: "use-scene-gizmo");
            gizmoColorField = root.Q<ColorField>(name: "gizmo-color");
            addNewStateButton = root.Q<Button>(name: "add-new-state");

            UIUtility.InitializeField
            (
                wheelJointField,
                component,
                () => component.KinematicWheelJoint,
                e =>
                {
                    component.KinematicWheelJoint = (AWheelJoint)e.newValue;
                },
                typeof(AWheelJoint)
            );

            UIUtility.InitializeField
            (
                useSceneGizmoToggle,
                component,
                () => component.UseSceneGizmo,
                e =>
                {
                    component.UseSceneGizmo = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                gizmoColorField,
                component,
                () => component.GizmoColor,
                e =>
                {
                    component.GizmoColor = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            addNewStateButton.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                DSwitchState switchState = component.SaveCurrentPositionAsState();
                EditorCoroutines.StartEditorCoroutine(redrawWindow());
                updateStateContainerEnabled();
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
                    float correctionalAngle = Vector3.SignedAngle(parentForward, component.KinematicWheelJoint.ForwardDirection.GlobalVector.ToFloat(), component.KinematicWheelJoint.AxisDirection.GlobalVector.ToFloat());
                    Quaternion handleDirection = Quaternion.AngleAxis(((float)component.SwitchStates[i].Position * 360f) + correctionalAngle, component.KinematicWheelJoint.AxisDirection.GlobalVector.ToFloat()) * parentRotation;

                    //Draw handle
                    Handles.ArrowHandleCap(0, handleOrigin, handleDirection, size * 0.875f, EventType.Repaint);
                    Handles.Label(handleOrigin + (handleDirection * (parentForward * size)), component.SwitchStates[i].Name);

                    //Draw transition lines
                    Vector3 fromPosition = component.KinematicWheelJoint.transform.position;
                    float angle = (float)component.SwitchStates[i].UpperTransition * 360f;
                    Vector3 toVector = (component.KinematicWheelJoint.ForwardDirection.GlobalVector.Normalized * component.KinematicWheelJoint.Radius).ToFloat();
                    Quaternion rotateVector = Quaternion.AngleAxis(angle, component.KinematicWheelJoint.AxisDirection.GlobalVector.ToFloat());
                    toVector = rotateVector * toVector;
                    Vector3 toPosition = fromPosition + toVector;
                    Handles.DrawLine(fromPosition, toPosition);
                }
            }
        }
    }
}
