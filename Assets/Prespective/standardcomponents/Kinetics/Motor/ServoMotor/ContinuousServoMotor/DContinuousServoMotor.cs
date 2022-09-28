#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;
using UnityEngine;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.physics.kinetics.motor;
using u040.prespective.standardcomponents;
using u040.prespective.math.doubles;
using u040.prespective.math;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor
{
    public class DContinuousServoMotor : DBaseMotor, IActuator
    {
        public enum Range { D_180 = 180, D_270 = 270 };
        [SerializeField] private Range rotationRange = Range.D_180;
        public Range RotationRange
        {
            get
            {
                return this.rotationRange;
            }
            set
            {
                if (this.rotationRange != value)
                {
                    this.rotationRange = value;
                    this.rangeValue = (double)value;
                }
            }
        }

        public double SecondsPer60Degrees
        {
            get
            {
                return 60d / this.MaxVelocity;
            }
            set
            {
                if (this.SecondsPer60Degrees != value && value > 0d)
                {
                    this.MaxVelocity = 60d / value;
                    this.Acceleration = this.MaxVelocity * 10d; //FIXME: This * 10f is random. Depends on load on motor but cannot be integrated without physics.
                    this.Deceleration = MaxVelocity * 10d;
                }
            }
        }

        [SerializeField] protected double rangeValue = 180d;

        [SerializeField] protected double _target = 90d;
        public virtual double Target
        {
            get
            {
                return this._target;
            }
            set
            {
                if (this._target != value)
                {
                    this._target = PreSpectiveMath.Clamp(value, 0d, rangeValue);
                }
            }
        }

        public double Position
        {
            get { return this.KinematicWheelJoint ? this.KinematicWheelJoint.CurrentRevolutionDegrees : -1d; }
        }

        [SerializeField] private double deadAngle = 0.05d;
        public double DeadAngle
        {
            get
            {
                return this.deadAngle;
            }
            set
            {
                if (this.deadAngle != value)
                {
                    this.deadAngle = value;
                }
            }
        }


        protected override void FixedUpdate()
        {
            UpdatePreferredVelocity();
            base.FixedUpdate();
        }

        protected virtual void UpdatePreferredVelocity()
        {
            this.TargetVelocity = ((Target - (0.5d * rangeValue)) / (0.5d * rangeValue)) * this.MaxVelocity;
        }

        #region <<PWM>>
        public bool EnablePWM = false;

        public DVector2 PulseWidthDefinition = new DVector2(1.0d, 2.0d);
        private double pulseWidthRange
        {
            get
            {
                return this.PulseWidthDefinition.Y - this.PulseWidthDefinition.X;
            }
        }

        [SerializeField] private double pulseWidth = 1.5d;
        public double PulseWidth
        {
            get
            {
                //Return pulse width between min and max relative to target relative to min and max.
                return PulseWidthDefinition.X + ((Target / rangeValue) * pulseWidthRange);
            }
            set
            {
                //Include check to prevent unnecessary calculations calculations
                if (pulseWidth != value)
                {
                    this.pulseWidth = value;
                    this.Target = (PreSpectiveMath.Clamp(value, PulseWidthDefinition.X, PulseWidthDefinition.Y) - PulseWidthDefinition.X) / pulseWidthRange * rangeValue;
                }
            }
        }

        [SerializeField] private double deadBandWidth = -1d;
        public double DeadBandWidth
        {
            get
            {
                if (this.deadBandWidth == -1d || this.deadBandWidth != (this.DeadAngle / rangeValue) * pulseWidthRange)
                {
                    return this.deadBandWidth = (this.DeadAngle / rangeValue) * pulseWidthRange;
                }
                return this.deadBandWidth;
            }
            set
            {
                if (this.deadBandWidth != value)
                {
                    this.deadBandWidth = value;
                    this.deadAngle = (value / pulseWidthRange) * rangeValue;
                }
            }
        }
        #endregion
    }
}

