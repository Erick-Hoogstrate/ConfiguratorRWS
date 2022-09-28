using System.Reflection;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
#pragma warning disable 0618
    [CustomEditor(typeof(VacuumGripperFinger))]
    public class VacuumGripperFingerEditorUIE : GripperFingerEditorUIE<VacuumGripperFinger>
    {
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/VacuumGripperFingerLayout");
            base.ExecuteOnEnable();
        }
    }
#pragma warning restore 0618

}
