using System.Reflection;
using UnityEngine;
using u040.prespective.math.doubles;
using u040.prespective.math;
using u040.prespective.prepair.virtualhardware.actuators;
using u040.prespective.prepair.virtualhardware.actuators.motors;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.motors
{
    /// <summary>
    /// A servomotor that is not limited but can move continuously. Which is a common component in electronics
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DContinuousServoMotor : DBaseMotor, IActuator
    {
        #region<enums>
        /// <summary>
        /// Maximum range of rotation
        /// </summary>
        public enum Range
        {
            D_180 = 180,
            D_270 = 270
        };
        #endregion

        #region<properties>
        [SerializeField] private Range rotationRange = Range.D_180;
        /// <summary>
        /// Maximum range of rotation for the motor
        /// </summary>
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

                }
            }
        }

        /// <summary>
        /// Seconds required to rotate 60 degrees
        /// </summary>
        public double SecondsPer60Degrees
        {
            get
            {
                return 60d / this.MaxVelocity;
            }
            set
            {
                if (this.SecondsPer60Degrees != value)
                {
                    value = PreSpectiveMath.Max(value, 0.001d);
                    this.MaxVelocity = 60d / value;
                    this.Acceleration = this.MaxVelocity * 10d;
                    this.Deceleration = this.MaxVelocity * 10d;
                }
            }
        }

        /// <summary>
        /// Numeric value for the Rotation Range
        /// </summary>
        protected double rangeValue
        {
            get 
            {
                return (double)RotationRange;
            }
        }

        [SerializeField] protected double storedTarget = 90d;
        /// <summary>
        /// Target position for the motor in degrees
        /// </summary>
        public virtual double Target
        {
            get
            {
                return this.storedTarget;
            }
            set
            {
                if (this.storedTarget != value)
                {
                    this.storedTarget = PreSpectiveMath.Clamp(value, 0d, rangeValue);
                }
            }
        }

        /// <summary>
        /// Current position of the motor in degrees
        /// </summary>
        public double Position
        {
            get 
            { 
                return this.KinematicWheelJoint ? this.KinematicWheelJoint.CurrentRevolutionDegrees : 0d; 
            }
        }

        [SerializeField] private double deadAngle = 0.05d;
        /// <summary>
        /// The angle within which the motor will define itself being 'on target'
        /// </summary>
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
                    this.deadAngle = PreSpectiveMath.Max(value, 0d);
                }
            }
        }

        #region <<PWM>>
        /// <summary>
        /// Enable Pulse Width Modulation
        /// </summary>
        public bool EnablePWM = false;

        [SerializeField]  DVector2 pulseWidthDefinition = new DVector2(1d, 2d);
        /// <summary>
        /// Defining the upper and lower band for the PWM
        /// </summary>
        public DVector2 PulseWidthDefinition
        {
            get
            {
                return this.pulseWidthDefinition;
            }
            set
            {
                if (this.pulseWidthDefinition != value)
                {
                    value = new DVector2(PreSpectiveMath.Max(0d, PreSpectiveMath.Min(value.X, value.Y)), PreSpectiveMath.Max(0d, PreSpectiveMath.Max(value.X, value.Y)));
                    this.pulseWidthDefinition = value;
                }
            }
        }

        /// <summary>
        /// PWM range
        /// </summary>
        private double pulseWidthRange
        {
            get
            {
                return this.PulseWidthDefinition.Y - this.PulseWidthDefinition.X;
            }
        }

        [SerializeField] private double pulseWidth = 1.5d;
        /// <summary>
        /// Pulse Width
        /// </summary>
        public double PulseWidth
        {
            get
            {
                //Return pulse width between min and max relative to target relative to min and max.
                return PulseWidthDefinition.X + ((Target / rangeValue) * pulseWidthRange);
            }
            set
            {
                //Include check to prevent unnecessary calculations
                if (pulseWidth != value)
                {
                    this.pulseWidth = PreSpectiveMath.Max(value, 0.001d);
                    this.Target = (PreSpectiveMath.Clamp(value, PulseWidthDefinition.X, PulseWidthDefinition.Y) - PulseWidthDefinition.X) / pulseWidthRange * rangeValue;
                }
            }
        }

        [SerializeField]  private double deadBandWidth = 0d;
        /// <summary>
        /// The Pulse Width within which the motor will define itself being 'on target'
        /// </summary>
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
                    this.deadBandWidth = PreSpectiveMath.Clamp(value, 0d, pulseWidthRange);
                    this.deadAngle = (this.deadBandWidth / pulseWidthRange) * rangeValue;
                }
            }
        }
        #endregion
        #endregion

        #region<update>
        protected override void onFixedUpdate()
        {
            updatePreferredVelocity();
            base.onFixedUpdate();
        }

        protected virtual void updatePreferredVelocity()
        {
            this.TargetVelocity = (Target - 0.5d * rangeValue) / (0.5d * rangeValue) * this.MaxVelocity;
        }
        #endregion
    }
}
