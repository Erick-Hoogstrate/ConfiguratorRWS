using u040.prespective.utility;
using UnityEngine;
using u040.prespective.math.doubles;
using u040.prespective.math;
using u040.prespective.prepair;
using u040.prespective.utility.modelmanagement;
using System.Reflection;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.prepair.virtualhardware.sensors;
using u040.prespective.prepair.virtualhardware.sensors.position;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.position
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DSlideSwitch : DBaseSwitch, ISensor
    {
        #region<properties>
        /// <summary>
        /// The Wheel Joint used for the Slide Switch
        /// </summary>
        public APrismaticJoint KinematicPrismaticJoint;

        /// <summary>
        /// Returns whether the switch has a physical limit 
        /// </summary>
        public override bool LoopingSwitch
        {
            get
            {
                if (KinematicPrismaticJoint && KinematicPrismaticJoint.ConstrainingSpline)
                {
                    return this.KinematicPrismaticJoint.ConstrainingSpline.IsClosed;
                }
                return false;
            }
        }

        [SerializeField] private DVector3 storedTransform;
        protected override bool hasMoved
        {
            get
            {
                if (KinematicPrismaticJoint && storedTransform != this.KinematicPrismaticJoint.transform.position.ToDouble())
                {
                    this.storedTransform = this.KinematicPrismaticJoint.transform.position.ToDouble();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// The current position of the switch in percentages over the full length of the Prismatic Joint
        /// </summary>
        public override double CurrentPositionPercentage
        {
            get
            {
                if (KinematicPrismaticJoint)
                {
                    return this.KinematicPrismaticJoint.CurrentPercentage;
                }
                return -1d;
            }
        }
        #endregion

        #region<unity functions>
        /// <summary>
        /// Unity Reset
        /// </summary>
        public void Reset()
        {
            this.KinematicPrismaticJoint = this.RequireComponent<APrismaticJoint>(APrismaticJoint.GetConcreteExplicitType, true);
        }
        #endregion

        #region<select>
        /// <summary>
        /// Select an existing switch state by ID. This sets the Wheel Joint to that position.
        /// </summary>
        /// <param name="_id">Switch State ID</param>
        public override void SelectState(int _id)
        {
            try
            {
                this.KinematicPrismaticJoint.Translate(SwitchStates[_id].Position - this.KinematicPrismaticJoint.CurrentPercentage, (IntentData _intent) =>
                {
                    if (_intent.EnforceableFraction != 1d)
                    {
                        Debug.LogError("Unable to complete translation. Traveled " + _intent.EnforceableFraction + " times the requested distance.");
                    }
                });
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }
        #endregion

        #region<match>
        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = 0d;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = 1d;
        }
        #endregion

        #region<save>
        /// <summary>
        /// Add the current position of the switch as a Switch State
        /// </summary>
        /// <returns>The newly created Switch State</returns>
        public override DSwitchState SaveCurrentPositionAsState()
        {
            return this.SaveState(this.KinematicPrismaticJoint.CurrentPercentage);
        }
        #endregion
    }
}
