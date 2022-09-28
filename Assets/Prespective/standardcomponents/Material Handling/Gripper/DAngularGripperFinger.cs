using System;
using u040.prespective.math.doubles;
using u040.prespective.prepair;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.materialhandling.gripper
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
                    return this.WheelJoint.RotationLimitMinMaxDeg.X;
                }
                return 0d;
            }
            set
            {
                if (WheelJoint && LowerLimit != value)
                {
                    DVector2 _newLimits = new DVector2(value, this.WheelJoint.RotationLimitMinMaxDeg.Y);
                    this.WheelJoint.RotationLimitMinMaxDeg = _newLimits;
                }
            }
        }
        public double UpperLimit
        {
            get
            {
                if (WheelJoint)
                {
                    return this.WheelJoint.RotationLimitMinMaxDeg.Y;
                }
                return 0d;
            }
            set
            {
                if (WheelJoint && UpperLimit != value)
                {
                    DVector2 _newLimits = new DVector2(this.WheelJoint.RotationLimitMinMaxDeg.X, value);
                    this.WheelJoint.RotationLimitMinMaxDeg = _newLimits;
                }
            }
        }

        private double angleOfFreedom
        {
            get
            {
                if (WheelJoint)
                {
                    DVector2 _limits = WheelJoint.RotationLimitMinMaxDeg;
                    return _limits.Y - _limits.X;
                }
                return 0d;
            }
        }

        private void Reset()
        {
            this.WheelJoint = this.gameObject.RequireComponent<DWheelJoint>(true);
        }

        public override void SetPosition(double _percent)
        {
            if (WheelJoint)
            {
                double _relativeAngle = this.angleOfFreedom * _percent;
                double _absoluteAngle = WheelJoint.RotationLimitMinMaxDeg.X + _relativeAngle;
                double _moveIntent = _absoluteAngle - this.WheelJoint.CurrentRevolutionDegrees;

                WheelJoint.Rotate(_moveIntent, new Action<IntentData>(_intentData => 
                { 
                    fingerJammedCallback(_intentData.EnforcableFraction); 
                }));
            }
            else
            {
                Debug.LogError("Cannot set position of " + this.name + " while no WheelJoint has been assigned");
            }
        }
    }
}
