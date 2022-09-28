using System;
using u040.prespective.core;
using UnityEngine;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor
{
    [Obsolete(DeprecationMessages.DEPRECATION_LIMITED_SERVO_MOTOR)]
    public class LimitedServoMotor : ContinuousServoMotor
    {
        public float Damping = 0.1f;

        private float target
        {
            get
            {
                //Return angle to rotate to, minus the neutral angle which is half the max angle.
                return this.Target - (rangeValue * 0.5f);
            }
        }

        protected override void onReset()
        {
            base.onReset();
            this.SecondsPer60Degrees = 0.1f;
        }


        protected override void updatePreferredVelocity()
        {
            this.TargetVelocity = (target - Position) * (1f / Damping); //FIXME: This *50f is random... Needs to be changed to something with more ground to it.

            //Prevent updates within the deadband
            if (Mathf.Abs(target - Position) <= DeadAngle)
            {
                this.TargetVelocity = 0f;
            }
        }
    }
}
