using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.sensors.position;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    [CustomEditor(typeof(DRotaryEncoder))]
    public class DRotaryEncoderEditorUIE : DEncoderEditorUIE<DRotaryEncoder>
    {
        #region << FIELDS >>
        ObjectField wheelJointField;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DRotaryEncoderEditorLayout";
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
        }
    }
}
