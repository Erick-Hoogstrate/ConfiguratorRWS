using UnityEngine;
using u040.prespective.prepair.physics.kinetics.motor;
using System;
    
namespace u040.prespective.standardcomponents.kinetics.motor.steppermotor
{
    public class DDrivenStepperMotor : DDrivenMotor, IActuator
    {
        public int StepsPerCycle = 200;
        private double stepAngle { get { return 360d / StepsPerCycle; } }

        [SerializeField] private int targetStep = 0;
        public int TargetStep
        {
            get { return (int)Math.Round(this.TargetAngle / stepAngle); }
            set
            {
                this.targetStep = value;
                this.TargetAngle = this.targetStep * stepAngle;
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            TargetStep = TargetStep; //Round target to steps
        }

        public double PositionDegrees { get { return this.Position; } }
        public int PositionSteps { get { return (int)Math.Round(this.Position / stepAngle); } }

        protected override void goToError()
        {
            base.goToError();
            this.TargetStep = this.PositionSteps;
        }
    }
}
