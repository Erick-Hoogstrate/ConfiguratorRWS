using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.gripper.fingers
{
    [CustomEditor(typeof(DParallelGripperFinger))]
    public class DParallelGripperFingerEditorUIE : DGripperFingerEditorUIE<DParallelGripperFinger>
    {
        #region << Property Fields >>
        ObjectField prismaticjoint;
        #endregion

        protected override string visualTreeFile
        {
            get
            {
                return "DParallelGripperFingerEditorLayout";
            }
        }

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void initialize()
        {
            base.initialize();
            #region << Properties >>
            prismaticjoint = root.Q<ObjectField>(name: "prismatic-joint");
            #endregion

            UIUtility.InitializeField
            (
                prismaticjoint,
                component,
                () => component.PrismaticJoint,
                e =>
                {
                    component.PrismaticJoint = (APrismaticJoint)e.newValue;
                },
                typeof(APrismaticJoint)
            );
        }
    }
}
