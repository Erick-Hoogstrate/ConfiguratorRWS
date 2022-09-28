using System;
using System.Reflection;
using u040.prespective.core;
using u040.prespective.prepair.inspector;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace u040.prespective.standardcomponents.kinetics.motor.linearactuator
{
    [Obsolete(DeprecationMessages.DEPRECATION_LINEARACTUATOR)]
    public class LinearActuator : MonoBehaviour
    {
#pragma warning disable 0618

#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        public AFPrismaticJoint PrismaticJoint;

        public float Target = 0f;
        public bool InvertPosition = false;

        [SerializeField] private float extendingCycleTime = 1f;
        public float ExtendingCycleTime
        {
            get
            {
                //get actual time by dividing spline length by move speed
                if (this.PrismaticJoint && this.PrismaticJoint.ConstrainingSpline)
                {
                    this.extendingCycleTime = this.PrismaticJoint.ConstrainingSpline.SplineLength / ExtendingMoveSpeed;
                }

                //return value
                return extendingCycleTime;
            }
            set
            {
                //If value has changed and is positive
                if (this.extendingCycleTime != value && value > 0f)
                {
                    //set value
                    this.extendingCycleTime = value;
                    
                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.PrismaticJoint && this.PrismaticJoint.ConstrainingSpline)
                    {
                        ExtendingMoveSpeed = this.PrismaticJoint.ConstrainingSpline.SplineLength / value;
                    }
                }
            }
        }


        [SerializeField] private float extendingMoveSpeed = 1f;
        public float ExtendingMoveSpeed
        {
            get
            {
                return this.extendingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.extendingMoveSpeed != value && value > 0f)
                {
                    //Set value
                    this.extendingMoveSpeed = value;
                }
            }
        }

        [SerializeField] private float retractingCycleTime = 1f;
        public float RetractingCycleTime
        {
            get
            {
                //get actual time by dividing spline length by move speed
                if (this.PrismaticJoint && this.PrismaticJoint.ConstrainingSpline)
                {
                    this.retractingCycleTime = this.PrismaticJoint.ConstrainingSpline.SplineLength / RetractingMoveSpeed;
                }

                //return value
                return retractingCycleTime;
            }
            set
            {
                //If value has changed and is positive
                if (this.retractingCycleTime != value && value > 0f)
                {
                    //set value
                    this.retractingCycleTime = value;

                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.PrismaticJoint && this.PrismaticJoint.ConstrainingSpline)
                    {
                        RetractingMoveSpeed = this.PrismaticJoint.ConstrainingSpline.SplineLength / value;
                    }
                }
            }
        }


        [SerializeField] private float retractingMoveSpeed = 1f;
        public float RetractingMoveSpeed
        {
            get
            {
                return this.retractingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.retractingMoveSpeed != value && value > 0f)
                {
                    //Set value
                    this.retractingMoveSpeed = value;
                }
            }
        }

        public float Position
        {
            get
            {
                if (PrismaticJoint)
                {
                    if(InvertPosition)
                    {
                        return 1f - PrismaticJoint.CurrentPerc;
                    }
                    return PrismaticJoint.CurrentPerc;
                }
                else
                {
                    return 0f;
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
            this.PrismaticJoint = this.RequireComponent<AFPrismaticJoint>(AFPrismaticJoint.GetConcreteExplicitType, true);
        }

        internal void FixedUpdate()
        {
            if (this.PrismaticJoint != null)
            {
                if (Position != Target)
                {
                    float moveSpeed = (Target - Position > 0) ? ExtendingMoveSpeed : RetractingMoveSpeed;

                    float speedPercentage = (moveSpeed * Time.deltaTime) / this.PrismaticJoint.ConstrainingSpline.SplineLength;
                    float travelDistance = Mathf.Clamp(Target - Position, -speedPercentage, speedPercentage);
                    setPosition(Position + travelDistance);
                }
            }
        }

        private void setPosition(float _percentage)
        {
            float move = _percentage - Position;
            if (InvertPosition)
            {
                move *= -1f;
            }

            this.PrismaticJoint.KinematicTranslation(move, VectorSpace.LocalParent, new Action<float, Vector3>((float _completedPercentage, Vector3 _completedTranslation) => { /*CALLBACK HERE*/ })); //apply step
        }
#pragma warning restore 0618
    }
}
