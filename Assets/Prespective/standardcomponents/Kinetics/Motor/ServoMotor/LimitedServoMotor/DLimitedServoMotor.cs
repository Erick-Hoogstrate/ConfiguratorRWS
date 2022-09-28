using System;
using u040.prespective.math;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor
{
    public class DLimitedServoMotor : DContinuousServoMotor
    {
        public double Damping = 0.1d;
        private double startingPosition;

        private double target
        {
            get
            {
                //Return angle to rotate to, minus the neutral angle which is half the max angle.
                return this.Target - (rangeValue * 0.5d);
            }
        }

        protected override void Reset()
        {
            base.Reset();
            this.SecondsPer60Degrees = 0.1d;
        }

        public override double Target
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
                    startingPosition = Position;
                }
            }
        }



        protected override void UpdatePreferredVelocity()
        {
            this.TargetVelocity = (target - Position) * (1d / Damping); //FIXME: This *50f is random... Needs to be changed to something with more ground to it.
            //if(startingPosition == Position)
            //{
            //    double prefVelocity = ((target - Position) / (0.5d * rangeValue)) * MaxVelocity;
            //    TargetVelocity = PreSpectiveMath.Clamp(prefVelocity, -MaxVelocity, MaxVelocity);
            //}
            
            //Prevent updates within the deadband
            if (Math.Abs(target - Position) <= DeadAngle)
            {
                this.TargetVelocity = 0d;
            }
        }
    }
}
