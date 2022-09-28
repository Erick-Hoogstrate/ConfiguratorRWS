using System;
using u040.prespective.core;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.materialhandling.gripper
{
    [Obsolete(DeprecationMessages.DEPRECATION_PARALLELGRIPPERFINGER)]
    public class ParallelGripperFinger : GripperFinger
    {
#pragma warning disable 0618

        public AFPrismaticJoint PrismaticJoint;

        public override KinematicTransform KinematicTransform
        {
            get
            {
                return this.PrismaticJoint;
            }
            set
            {
                if (value != PrismaticJoint)
                {
                    if (value.GetType() == typeof(AFPrismaticJoint))
                    {
                        this.PrismaticJoint = (AFPrismaticJoint)value;
                    }
                    else
                    {
                        Debug.LogError("Cannot add " + value.name + " as a KinematicTransform since it is not of type " + typeof(AFPrismaticJoint).Name);
                    }
                }
            }
        }

        private void Reset()
        {
            this.PrismaticJoint = this.gameObject.RequireComponent<AFPrismaticJoint>(AFPrismaticJoint.GetConcreteExplicitType, true);
        }

        public override void SetPosition(float _percent)
        {
            if (PrismaticJoint)
            {
                float _relativePercentageMove = _percent - PrismaticJoint.CurrentPerc;

                PrismaticJoint.KinematicTranslation(_relativePercentageMove, VectorSpace.LocalParent, new Action<float, Vector3>((float _percentage, Vector3 _translation) =>
                {

                    fingerJammedCallback(_percentage);
                }));
            }
            else
            {
                Debug.LogError("Cannot set position of " + this.name + " while no PrismaticJoint has been assigned");
            }
        }
#pragma warning restore 0618
    }
}
