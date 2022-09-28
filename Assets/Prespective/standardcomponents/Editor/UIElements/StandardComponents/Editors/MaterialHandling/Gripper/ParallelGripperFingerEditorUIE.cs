using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
#pragma warning disable 0618

    [CustomEditor(typeof(ParallelGripperFinger))]
    public class ParallelGripperFingerEditorUIE : GripperFingerEditorUIE<ParallelGripperFinger>
    {
        #region << Property Fields >>
        ObjectField prismaticjoint;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/Gripper/ParallelGripperFingerLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();
            #region << Properties >>
            prismaticjoint = root.Q<ObjectField>(name: "prismatic-joint");
            #endregion

            UIUtility.InitializeField
            (
                prismaticjoint,
                () => component.PrismaticJoint,
                e =>
                {
                    component.PrismaticJoint = (AFPrismaticJoint)e.newValue;
                },
                typeof(AFPrismaticJoint)
            );
        }
#pragma warning restore 0618
    }
}
