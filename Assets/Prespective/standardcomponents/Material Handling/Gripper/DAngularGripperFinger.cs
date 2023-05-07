using System;
using u040.prespective.math.doubles;
using u040.prespective.prepair;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.utility.modelmanagement;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers
{
    public class DAngularGripperFinger : DGripperFinger
    {
        public AWheelJoint WheelJoint;

        public override KinematicBody KinematicBody
        {
            get
            {
                return this.WheelJoint;
            }
            set
            {
                if (value != WheelJoint)
                {
                    if (value.GetType() == typeof(DWheelJoint))
                    {
                        this.WheelJoint = (DWheelJoint)value;
                    }
                    else
                    {
                        Debug.LogError("Cannot add " + value.name + " as a KinematicTransform since it is not of type " + typeof(DWheelJoint).Name);
                    }
                }
            }
        }

        public double LowerLimit
        {
            get
            {
                if (WheelJoint)
                {
                    return this.WheelJoint.RotationLimitMinMaxDegrees.X;
                }
                return 0d;
            }
            set
            {
                if (WheelJoint && LowerLimit != value)
                {
                    DVector2 newLimits = new DVector2(value, this.WheelJoint.RotationLimitMinMaxDegrees.Y);
                    this.WheelJoint.RotationLimitMinMaxDegrees = newLimits;
                }
            }
        }
        public double UpperLimit
        {
            get
            {
                if (WheelJoint)
                {
                    return this.WheelJoint.RotationLimitMinMaxDegrees.Y;
                }
                return 0d;
            }
            set
            {
                if (WheelJoint && UpperLimit != value)
                {
                    DVector2 newLimits = new DVector2(this.WheelJoint.RotationLimitMinMaxDegrees.X, value);
                    this.WheelJoint.RotationLimitMinMaxDegrees = newLimits;
                }
            }
        }

        private double angleOfFreedom
        {
            get
            {
                if (WheelJoint)
                {
                    DVector2 limits = WheelJoint.RotationLimitMinMaxDegrees;
                    return limits.Y - limits.X;
                }
                return 0d;
            }
        }

        internal void Reset()
        {
            this.WheelJoint = this.gameObject.RequireComponent<DWheelJoint>(true);
        }

        public override void SetPosition(double _percent)
        {
            if (WheelJoint)
            {
                double relativeAngle = this.angleOfFreedom * _percent;
                double absoluteAngle = WheelJoint.RotationLimitMinMaxDegrees.X + relativeAngle;
                double moveIntent = absoluteAngle - this.WheelJoint.CurrentRevolutionDegrees;

                WheelJoint.Rotate(moveIntent, new Action<IntentData>(_intentData => 
                { 
                    fingerJammedCallback(_intentData.EnforceableFraction); 
                }));
            }
            else
            {
                Debug.LogError("Cannot set position of " + this.name + " while no WheelJoint has been assigned");
            }
        }
    }
}
