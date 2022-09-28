using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
    [CustomEditor(typeof(RotaryEncoder))]
    public class RotaryEncoderEditorUIE : EncoderEditorUIE<RotaryEncoder>
    {
#pragma warning disable 0618

        #region << Property Fields>>
        ObjectField wheelJoint;
        #endregion
        #region << Control Panel Fields >>
        TextField valueControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Encoders/RotaryEncoderLayout");
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
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AFWheelJoint)e.newValue;
                },
                typeof(AFWheelJoint)
            );
            #endregion
        }
    }
#pragma warning restore 0618
}
