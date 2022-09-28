using System;
using System.Reflection;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility;
using UnityEngine;
using u040.prespective.math;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace u040.prespective.standardcomponents.kinetics.motor.linearactuator
{
    public class DLinearActuator : MonoBehaviour
    {
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        public APrismaticJoint KinematicPrismaticJoint;

        public double Target = 0d;
        public bool InvertPosition = false;

        [SerializeField] private double extendingCycleTime = 1d;
        public double ExtendingCycleTime
        {
            get
            {
                //get actual time by dividing spline length by move speed
                if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                {
                    this.extendingCycleTime = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / ExtendingMoveSpeed;
                }

                //return value
                return extendingCycleTime;
            }
            set
            {
                //If value has changed and is positive
                if (this.extendingCycleTime != value && value > 0d)
                {
                    //set value
                    this.extendingCycleTime = value;

                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                    {
                        ExtendingMoveSpeed = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / value;
                    }
                }
            }
        }


        [SerializeField] private double extendingMoveSpeed = 1d;
        public double ExtendingMoveSpeed
        {
            get
            {
                return this.extendingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.extendingMoveSpeed != value && value > 0d)
                {
                    //Set value
                    this.extendingMoveSpeed = value;
                }
            }
        }

        [SerializeField] private double retractingCycleTime = 1d;
        public double RetractingCycleTime
        {
            get
            {
                //get actual time by dividing spline length by move speed
                if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                {
                    this.retractingCycleTime = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / RetractingMoveSpeed;
                }

                //return value
                return retractingCycleTime;
            }
            set
            {
                //If value has changed and is positive
                if (this.retractingCycleTime != value && value > 0d)
                {
                    //set value
                    this.retractingCycleTime = value;

                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                    {
                        RetractingMoveSpeed = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / value;
                    }
                }
            }
        }


        [SerializeField] private double retractingMoveSpeed = 1d;
        public double RetractingMoveSpeed
        {
            get
            {
                return this.retractingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.retractingMoveSpeed != value && value > 0d)
                {
                    //Set value
                    this.retractingMoveSpeed = value;
                }
            }
        }

        public double Position
        {
            get
            {
                if (KinematicPrismaticJoint)
                {
                    if (InvertPosition)
                    {
                        return 1d - KinematicPrismaticJoint.CurrentPercentage;
                    }
                    return KinematicPrismaticJoint.CurrentPercentage;
                }
                else
                {
                    return 0d;
                }
            }
            set
            {
                if (Position != value)
                {
                    setPosition(value);
                }
            }
        }


        internal void Reset()
        {
            this.KinematicPrismaticJoint = this.RequireComponent<DPrismaticJoint>(true);
        }

        internal void FixedUpdate()
        {
            if (this.KinematicPrismaticJoint != null)
            {
                if (Position != Target)
                {
                    double moveSpeed = (Target - Position > 0d) ? ExtendingMoveSpeed : RetractingMoveSpeed;

                    double speedPercentage = (moveSpeed * Time.deltaTime) / this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength;
                    double travelDistance = PreSpectiveMath.Clamp(Target - Position, -speedPercentage, speedPercentage);
                    setPosition(Position + travelDistance);
                }
            }
        }

        private void setPosition(double _percentage)
        {
            double move = _percentage - Position;
            if (InvertPosition)
            {
                move *= -1d;
            }

            this.KinematicPrismaticJoint.Translate(move);
        }
    }
}
