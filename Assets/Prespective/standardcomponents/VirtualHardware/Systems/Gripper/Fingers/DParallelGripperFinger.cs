using System;
using u040.prespective.prepair;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.utility.modelmanagement;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers
{
    public class DParallelGripperFinger : DGripperFinger
    {
        public APrismaticJoint PrismaticJoint;

        public override KinematicBody KinematicBody
        {
            get
            {
                return this.PrismaticJoint;
            }
            set
            {
                if (value != PrismaticJoint)
                {
                    if (value.GetType() == typeof(DPrismaticJoint))
                    {
                        this.PrismaticJoint = (DPrismaticJoint)value;
                    }
                    else
                    {
                        Debug.LogError("Cannot add " + value.name + " as a KinematicTransform since it is not of type " + typeof(DPrismaticJoint).Name);
                    }
                }
            }
        }

        internal void Reset()
        {
            this.PrismaticJoint = this.RequireComponent<APrismaticJoint>(APrismaticJoint.GetConcreteExplicitType, true);
        }

        public override void SetPosition(double _percent)
        {
            if (PrismaticJoint)
            {
                double relativePercentageMove = _percent - PrismaticJoint.CurrentPercentage;


                PrismaticJoint.Translate(relativePercentageMove, new Action<IntentData>(_intentData => 
                { 
                    fingerJammedCallback(_intentData.EnforceableFraction); 
                }));
            }
            else
            {
                Debug.LogError("Cannot set position of " + this.name + " while no PrismaticJoint has been assigned");
            }
        }
    }
}
