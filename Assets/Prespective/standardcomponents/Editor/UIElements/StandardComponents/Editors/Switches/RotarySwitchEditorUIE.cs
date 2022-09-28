using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using u040.prespective.core;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.ui.buttons.BaseSwitch;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(RotarySwitch))]
    public class RotarySwitchEditorUIE : SwitchesEditorUIE<RotarySwitch>
    {
        #region << Live Data Fields >>
        TextField selectedState;
        #endregion
        #region << Property Fields>>
        ObjectField wheelJoint;
        Toggle useSceneGizmo;
        ColorField gizmoColor;
        Button addNewState;
        #endregion

        string noSwitchStatesText = "N/A";

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Switches/RotarySwitchLayout");
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

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            selectedState = root.Q<TextField>(name: "selected-state");
            selectedState.isReadOnly = true;
            #endregion
            #region << Properties >>
            wheelJoint = root.Q<ObjectField>(name: "wheel-joint");
            useSceneGizmo = root.Q<Toggle>(name: "use-scene-gizmo");
            gizmoColor = root.Q<ColorField>(name: "gizmo-color");
            addNewState = root.Q<Button>(name: "add-new-state");

            UIUtility.InitializeField
            (
                wheelJoint,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AFWheelJoint)e.newValue;
                },
                typeof(AFWheelJoint)
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

            addNewState.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                SwitchState switchState = component.SaveCurrentPositionAsState();
                PersistentEditorCoroutine.StartCoroutine(redrawWindow());
            });
            #endregion
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
#pragma warning restore 0618
    }
}
