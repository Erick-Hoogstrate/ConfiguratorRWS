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
    [CustomEditor(typeof(DAngularGripperFinger))]
    public class DAngularGripperFingerEditorUIE : DGripperFingerEditorUIE<DAngularGripperFinger>
    {
        #region << Property Fields >>
        ObjectField wheelJoint;
        DoubleField lowerLimit;
        DoubleField upperLimit;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/DAngularGripperFingerLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();
            #region << Properties >>
            wheelJoint = root.Q<ObjectField>(name: "wheel-joint");
            lowerLimit = root.Q<DoubleField>(name: "lower-limit");
            upperLimit = root.Q<DoubleField>(name: "upper-limit");
            #endregion

            UIUtility.InitializeField
            (
                wheelJoint,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AWheelJoint)e.newValue;
                },
                typeof(AWheelJoint)
            );

            UIUtility.InitializeField
            (
                lowerLimit,
                () => component.LowerLimit,
                e =>
                {
                    double _value = Math.Min(e.newValue, component.UpperLimit);
                    component.LowerLimit = _value;
                    UpdateLimit();
                }
            );

            UIUtility.InitializeField
            (
                upperLimit,
                () => component.UpperLimit,
                e =>
                {
                    double _value = Math.Max(component.LowerLimit, e.newValue);
                    component.UpperLimit = _value;
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
}
