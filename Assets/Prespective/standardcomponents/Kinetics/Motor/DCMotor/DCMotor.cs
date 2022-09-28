#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using u040.prespective.core;
using UnityEngine;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.physics.kinetics.motor;
using u040.prespective.standardcomponents;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor
{
    [Obsolete(DeprecationMessages.DEPRECATION_DCMOTOR)]
    public class DCMotor : BaseMotor, IActuator
    {
        public void StartRotation()
        {
            if (this.TargetVelocity == 0f)
            {
                this.TargetVelocity = this.MaxVelocity;
            }
        }

        public void StopRotation()
        {
            if (this.TargetVelocity != 0f)
            {
                this.TargetVelocity = 0f;
            }
        }
    }
}
