using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.sensors.position;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    [CustomEditor(typeof(DLinearEncoder))]
    public class DLinearEncoderEditorUIE : DEncoderEditorUIE<DLinearEncoder>
    {
        #region << FIELDS >>
        //Property Fields
        private ObjectField prismaticJointField;
        
        //Control Panel Fields
        private TextField valueControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DLinearEncoderEditorLayout";
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

            prismaticJointField = root.Q<ObjectField>(name: "prismatic-joint");

            UIUtility.InitializeField
            (
                prismaticJointField,
                component,
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
