using UnityEngine;
using System;
using System.Reflection;
using u040.prespective.prepair.virtualhardware.actuators;
using u040.prespective.prepair.virtualhardware.actuators.motors;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.motors
{
    /// <summary>
    /// Simulations a motor that supports discrete motion rounded to its step size
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DDrivenStepperMotor : DDrivenMotor, IActuator
    {
        #region<properties>
        public int StepsPerCycle = 200;

        private double stepAngle 
        { 
            get 
            { 
                return 360d / StepsPerCycle; 
            }
        }

        [SerializeField]  private int targetStep = 0;
        public int TargetStep
        {
            get 
            { 
                return (int)Math.Round(this.TargetAngle / stepAngle); 
            }
            set
            {
                this.targetStep = value;
                this.TargetAngle = this.targetStep * stepAngle;
            }
        }
        #endregion

        public double PositionDegrees 
        { 
            get 
            { 
                return this.Position; 
            } 
        }

        public int PositionSteps 
        { 
            get 
            { 
                return (int)Math.Round(this.Position / stepAngle); 
            }
        }

        #region<update>
        protected override void onFixedUpdate()
        {
            base.onFixedUpdate();
            TargetStep = TargetStep; //Round target to steps
        }
        #endregion

        #region<error>
        /// <summary>
        /// Sets the motor into an error state
        /// </summary>
        protected override void triggerError()
        {
            base.triggerError();
            this.TargetStep = this.PositionSteps;
        }
        #endregion
    }
}
