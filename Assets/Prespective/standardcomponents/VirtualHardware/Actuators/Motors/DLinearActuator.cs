using u040.prespective.prepair.kinematics;
using UnityEngine;
using u040.prespective.math;
using System;
using u040.prespective.utility.modelmanagement;
using System.Reflection;
using u040.prespective.prepair.kinematics.joints.basic;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.motors
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DLinearActuator : MonoBehaviour
    {
        /// <summary>
        /// The Prismatic Joint used for the Linear Actuator
        /// </summary>
        public APrismaticJoint KinematicPrismaticJoint;

        /// <summary>
        /// The target percentage on the Prismatic Joint the Linear Actuator tries to move to
        /// </summary>
        public double Target = 0d;

        /// <summary>
        /// Whether the Target and Position on the Prismatic Joint are inverted
        /// </summary>
        public bool InvertPosition = false;

        private const double MIN_EXTENDING_TIME = 0.0001d;
        [SerializeField] private double extendingCycleTime = 1d;

        private bool missingComponentsWarned = false;

        /// <summary>
        /// The time (s) required for the Linear Actuator to do a full extensions
        /// </summary>
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
                if (this.extendingCycleTime != value)
                {
                    //set value
                    this.extendingCycleTime = Math.Max(value, MIN_EXTENDING_TIME);

                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                    {
                        ExtendingMoveSpeed = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / extendingCycleTime;
                    }
                }
            }
        }

        private const double MIN_EXTENDING_SPEED = 0.0001d;
        [SerializeField] private double extendingMoveSpeed = 1d;

        /// <summary>
        /// The speed (m/s) at which the Linear Actuator extends
        /// </summary>
        public double ExtendingMoveSpeed
        {
            get
            {
                return this.extendingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.extendingMoveSpeed != value)
                {
                    //Set value
                    this.extendingMoveSpeed = Math.Max(value, MIN_EXTENDING_SPEED);
                }
            }
        }

        private const double MIN_RETRACTING_TIME = 0.0001d;
        [SerializeField] private double retractingCycleTime = 1d;

        /// <summary>
        /// The time (s) required for the Linear Actuator to do a full retractions
        /// </summary>
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
                if (this.retractingCycleTime != value)
                {
                    //set value
                    this.retractingCycleTime = Math.Max(value, MIN_RETRACTING_TIME);

                    //If we have a spline set, set the MoveSpeed accordingly
                    if (this.KinematicPrismaticJoint && this.KinematicPrismaticJoint.ConstrainingSpline)
                    {
                        RetractingMoveSpeed = this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength / retractingCycleTime;
                    }
                }
            }
        }

        private const double MIN_RETRACTING_SPEED = 0.0001d;
        [SerializeField] private double retractingMoveSpeed = 1d;
        /// <summary>
        /// The speed (m/s) at which the Linear Actuator retracts
        /// </summary>
        public double RetractingMoveSpeed
        {
            get
            {
                return this.retractingMoveSpeed;
            }
            set
            {
                //If value has changed and is positive
                if (this.retractingMoveSpeed != value)
                {
                    //Set value
                    this.retractingMoveSpeed = Math.Max(value, MIN_RETRACTING_SPEED);
                }
            }
        }

        /// <summary>
        /// The Position of the Linear Actuator on the Prismatic Joint. This is affected by Invert Position.
        /// </summary>
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

        public void Reset()
        {
            this.KinematicPrismaticJoint = this.RequireComponent<APrismaticJoint>(APrismaticJoint.GetConcreteExplicitType, true);
        }

        public void FixedUpdate()
        {
            if (this.KinematicPrismaticJoint != null && this.KinematicPrismaticJoint.ConstrainingSpline != null)
            {
                if (Position != Target)
                {
                    double moveSpeed = (Target - Position > 0d) ? ExtendingMoveSpeed : RetractingMoveSpeed;

                    double speedPercentage = moveSpeed * Time.fixedDeltaTime / this.KinematicPrismaticJoint.ConstrainingSpline.SplineLength;
                    double travelDistance = PreSpectiveMath.Clamp(Target - Position, -speedPercentage, speedPercentage);
                    setPosition(Position + travelDistance);
                }
            }
            else if (!missingComponentsWarned)
            {
                Debug.LogError("Linear Actuator " + this.name + " is unable to function because either no PrismaticJoint has been assigned, or the assigned PrismaticJoint has no Spline assigned.");
                missingComponentsWarned = true;
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
