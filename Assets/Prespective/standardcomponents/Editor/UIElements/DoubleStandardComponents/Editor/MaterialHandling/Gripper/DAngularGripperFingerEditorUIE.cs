using System;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.gripper.fingers
{
    [CustomEditor(typeof(DAngularGripperFinger))]
    public class DAngularGripperFingerEditorUIE : DGripperFingerEditorUIE<DAngularGripperFinger>
    {
        #region << FIELDS >>
        private ObjectField wheelJointField;
        private DoubleField lowerLimitField;
        private DoubleField upperLimitField;
        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DAngularGripperFingerEditorLayout";
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

            wheelJointField = root.Q<ObjectField>(name: "wheel-joint");
            lowerLimitField = root.Q<DoubleField>(name: "lower-limit");
            upperLimitField = root.Q<DoubleField>(name: "upper-limit");

            UIUtility.InitializeField
            (
                wheelJointField,
                component,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AWheelJoint)e.newValue;
                },
                typeof(AWheelJoint)
            );

            UIUtility.InitializeField
            (
                lowerLimitField,
                component,
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
                upperLimitField,
                component,
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
            lowerLimitField.value = component.LowerLimit;
            upperLimitField.value = component.UpperLimit;
        }
    }
}
