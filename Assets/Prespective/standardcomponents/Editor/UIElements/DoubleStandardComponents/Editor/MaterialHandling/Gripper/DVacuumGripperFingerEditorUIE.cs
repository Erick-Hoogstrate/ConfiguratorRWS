using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;
using UnityEditor;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.gripper.fingers
{
    [CustomEditor(typeof(DVacuumGripperFinger))]
    public class DVacuumGripperFingerEditorUIE : DGripperFingerEditorUIE<DVacuumGripperFinger>
    {
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DVacuumGripperFingerEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }
    }
}
