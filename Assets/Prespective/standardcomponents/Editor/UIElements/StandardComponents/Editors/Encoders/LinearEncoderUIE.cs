using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
    [CustomEditor(typeof(LinearEncoder))]
    public class LinearEncoderUIE : EncoderEditorUIE<LinearEncoder>
    {
#pragma warning disable 0618

        #region << Property Fields>>
        ObjectField prismaticJoint;
        #endregion
        #region << Control Panel Fields >>
        TextField valueControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Encoders/LinearEncoderLayout");
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
                () => component.PrismaticJoint,
                e =>
                {
                    component.PrismaticJoint = (AFPrismaticJoint)e.newValue;
                },
                typeof(AFPrismaticJoint)
            );
            #endregion
        }
#pragma warning restore 0618

    }
}
