using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
    [CustomEditor(typeof(DLinearEncoder))]
    public class DLinearEncoderUIE : DEncoderEditorUIE<DLinearEncoder>
    {
        #region << Property Fields>>
        ObjectField prismaticJoint;
        #endregion
        #region << Control Panel Fields >>
        TextField valueControlPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Encoders/DLinearEncoderLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();
                        
            #region << Properties >>

            prismaticJoint = root.Q<ObjectField>(name: "prismatic-joint");

            UIUtility.InitializeField
            (
                prismaticJoint,
                () => component.KinematicPrismaticJoint,
                e =>
                {
                    component.KinematicPrismaticJoint = (APrismaticJoint)e.newValue;
                },
                typeof(APrismaticJoint)
            );
            #endregion
        }
    }
}
