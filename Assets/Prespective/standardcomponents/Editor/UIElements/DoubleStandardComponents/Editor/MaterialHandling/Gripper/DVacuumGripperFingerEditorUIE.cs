using System.Reflection;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    [CustomEditor(typeof(DVacuumGripperFinger))]
    public class DVacuumGripperFingerEditorUIE : DGripperFingerEditorUIE<DVacuumGripperFinger>
    {
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/DVacuumGripperFingerLayout");
            base.ExecuteOnEnable();
        }
    }
}
