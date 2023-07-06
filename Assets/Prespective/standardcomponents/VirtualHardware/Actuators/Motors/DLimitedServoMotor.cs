using System;
using System.Reflection;
using u040.prespective.math;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.motors
{
    /// <summary>
    /// Simulates a motor that moves to a target rotation, this motor type can only move in a limited rotation range
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DLimitedServoMotor : DContinuousServoMotor
    {
        #region<properties>
        [SerializeField]  private double damping = 0.1d;
        /// <summary>
        /// The rate at which the acceleration of the motor is being controlled
        /// </summary>
        public double Damping
        {
            get
            {
                return this.damping;
            }
            set
            {
                if (this.damping != value)
                {
                    this.damping = Math.Max(value, 0f);
                }
            }
        }

        private double target
        {
            get
            {
                //Return angle to rotate to, minus the neutral angle which is half the max angle.
                return this.Target - rangeValue * 0.5d;
            }
        }

        /// <summary>
        /// Target position in degrees
        /// </summary>
        public override double Target
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
        #endregion

        #region<reset>
        protected override void onReset()
        {
            base.onReset();
            this.SecondsPer60Degrees = 0.1d;
        }
        #endregion

        #region<update>
        protected override void updatePreferredVelocity()
        {
            this.TargetVelocity = (target - Position) * (1d / Damping); 
            
            //Prevent updates within the dead band
            if (Math.Abs(target - Position) <= DeadAngle)
            {
                this.TargetVelocity = 0d;
            }
        }
        #endregion
    }
}
