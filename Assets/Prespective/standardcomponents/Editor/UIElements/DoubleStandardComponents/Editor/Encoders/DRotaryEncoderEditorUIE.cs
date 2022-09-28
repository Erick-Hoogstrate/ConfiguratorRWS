using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
    [CustomEditor(typeof(DRotaryEncoder))]
    public class DRotaryEncoderEditorUIE : DEncoderEditorUIE<DRotaryEncoder>
    {
        #region << Property Fields>>
        ObjectField wheelJoint;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Encoders/DRotaryEncoderLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Properties >>
            wheelJoint = root.Q<ObjectField>(name: "wheel-joint");

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
            #endregion
        }
    }
}
