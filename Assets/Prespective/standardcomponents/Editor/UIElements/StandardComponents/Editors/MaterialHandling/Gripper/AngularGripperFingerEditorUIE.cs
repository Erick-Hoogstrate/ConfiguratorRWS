using System;
using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(AngularGripperFinger))]
    public class AngularGripperFingerEditorUIE : GripperFingerEditorUIE<AngularGripperFinger>
    {
        #region << Property Fields >>
        ObjectField wheelJoint;
        FloatField lowerLimit;
        FloatField upperLimit;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/AngularGripperFingerLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();
            #region << Properties >>
            wheelJoint = root.Q<ObjectField>(name: "wheel-joint");
            lowerLimit = root.Q<FloatField>(name: "lower-limit");
            upperLimit = root.Q<FloatField>(name: "upper-limit");
            #endregion

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
                lowerLimit,
                () => component.LowerLimit,
                e =>
                {
                    float _value = Math.Min(e.newValue, component.UpperLimit);
                    component.LowerLimit = e.newValue;
                    UpdateLimit();
                }
            );

            UIUtility.InitializeField
            (
                upperLimit,
                () => component.UpperLimit,
                e =>
                {
                    float _value = Math.Max(component.LowerLimit, e.newValue);
                    component.UpperLimit = e.newValue;
                    UpdateLimit();
                }
            );
        }

        private void UpdateLimit()
        {
            lowerLimit.value = component.LowerLimit;
            upperLimit.value = component.UpperLimit;
        }
    }
#pragma warning restore 0618
}
