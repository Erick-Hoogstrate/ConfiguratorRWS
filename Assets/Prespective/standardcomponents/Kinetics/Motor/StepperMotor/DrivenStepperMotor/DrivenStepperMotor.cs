#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using u040.prespective.prepair.inspector;
using UnityEngine;
using System.Reflection;
using u040.prespective.core;
using u040.prespective.prepair.physics.kinetics.motor;
using u040.prespective.standardcomponents;

namespace u040.prespective.standardcomponents.kinetics.motor.steppermotor
{
#pragma warning disable 0618
    [Obsolete(DeprecationMessages.DEPRECATION_DRIVENSTEPPERMOTOR)]
    public class DrivenStepperMotor : DrivenMotor, IActuator
    {
        public int StepsPerCycle = 200;
        private float stepAngle { get { return 360f / StepsPerCycle; } }

        [SerializeField] private int targetStep = 0;
        public int TargetStep
        {
            get { return Mathf.RoundToInt(this.TargetAngle / stepAngle); }
            set
            {
                this.targetStep = value;
                this.TargetAngle = this.targetStep * stepAngle;
            }
        }

        protected override void onFixedUpdate()
        {
            base.onFixedUpdate();
            TargetStep = TargetStep; //Round target to steps
        }

        public float PositionDegrees { get { return this.Position; } }
        public int PositionSteps { get { return Mathf.RoundToInt(this.Position / stepAngle); } }

        protected override void goToError()
        {
            base.goToError();
            this.TargetStep = this.PositionSteps;
        }
#pragma warning restore 0618
    }
}
