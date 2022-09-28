#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Reflection;
using u040.prespective.core;
using UnityEngine;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.physics.kinetics.motor;
using u040.prespective.standardcomponents;

namespace u040.prespective.standardcomponents.kinetics.motor.servomotor
{
    [Obsolete(DeprecationMessages.DEPRECATION_CONTINUOUS_SERVO_MOTOR)]
    public class ContinuousServoMotor : BaseMotor, IActuator
    {
#pragma warning disable 0618

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
                    this.rangeValue = (float)value;
                }
            }
        }

        public float SecondsPer60Degrees
        {
            get
            {
                return 60f / this.MaxVelocity;
            }
            set
            {
                if (this.SecondsPer60Degrees != value && value > 0f)
                {
                    this.MaxVelocity = 60f / value;
                    this.Acceleration = this.MaxVelocity * 10f; //FIXME: This * 10f is random. Depends on load on motor but cannot be integrated without physics.
                }
            }
        }

        [SerializeField] protected float rangeValue = 180f;

        [SerializeField] private float target = 90f;
        public float Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if (this.target != value)
                {
                    this.target = Mathf.Clamp(value, 0f, rangeValue);
                }
            }
        }

        public float Position
        {
            get { return this.WheelJoint ? this.WheelJoint.CurrentRevolutionDegrees : -1f; }

        }

        [SerializeField] private float deadAngle = 0.05f;
        public float DeadAngle
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


        protected override void onFixedUpdate()
        {
            updatePreferredVelocity();
            base.onFixedUpdate();
        }

        protected virtual void updatePreferredVelocity()
        {
            this.TargetVelocity = ((Target - (0.5f * rangeValue)) / (0.5f * rangeValue)) * this.MaxVelocity;
        }

        #region <<PWM>>
        public bool EnablePWM = false;

        public Vector2 PulseWidthDefinition = new Vector2(1.0f, 2.0f);
        private float pulseWidthRange
        {
            get
            {
                return this.PulseWidthDefinition.y - this.PulseWidthDefinition.x;
            }
        }

        [SerializeField] private float pulseWidth = 1.5f;
        public float PulseWidth
        {
            get
            {
                //Return pulse width between min and max relative to target relative to min and max.
                return PulseWidthDefinition.x + ((Target / rangeValue) * pulseWidthRange);
            }
            set
            {
                //Include check to prevent unnecessary calculations calculations
                if (pulseWidth != value)
                {
                    this.pulseWidth = value;
                    this.Target = (Mathf.Clamp(value, PulseWidthDefinition.x, PulseWidthDefinition.y) - PulseWidthDefinition.x) / pulseWidthRange * rangeValue;
                }
            }
        }

        [SerializeField] private float deadBandWidth = -1f;
        public float DeadBandWidth
        {
            get
            {
                if (this.deadBandWidth == -1f || this.deadBandWidth != (this.DeadAngle / rangeValue) * pulseWidthRange)
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
#pragma warning restore 0618
    }
}

