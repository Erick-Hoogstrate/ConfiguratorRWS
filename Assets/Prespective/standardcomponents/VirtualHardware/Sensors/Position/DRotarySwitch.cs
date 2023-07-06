using System.Reflection;
using u040.prespective.math;
using u040.prespective.math.doubles;
using u040.prespective.prepair;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.prepair.virtualhardware.sensors;
using u040.prespective.prepair.virtualhardware.sensors.position;
using u040.prespective.utility.modelmanagement;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.position
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DRotarySwitch : DBaseSwitch, ISensor
    {
        #region<properties>
        /// <summary>
        /// The Wheel Joint used for the Rotary Switch
        /// </summary>
        public AWheelJoint KinematicWheelJoint;

        /// <summary>
        /// Returns whether the switch has a physical limit 
        /// </summary>
        public override bool LoopingSwitch
        {
            get 
            {
                if (KinematicWheelJoint)
                {
                    return !this.KinematicWheelJoint.ApplyKinematicLimit;
                }
                return false;
            }
        }

        [SerializeField]  private DQuaternion storedRotation;
        protected override bool hasMoved
        {
            get
            {
                if (KinematicWheelJoint && storedRotation != this.KinematicWheelJoint.DTransform.Rotation)
                {
                    this.storedRotation = this.KinematicWheelJoint.DTransform.Rotation;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// The current position of the switch in percentages over a full rotation
        /// </summary>
        public override double CurrentPositionPercentage
        {
            get
            {
                if (KinematicWheelJoint)
                {
                    return this.KinematicWheelJoint.CurrentRevolutionPercentage;
                }
                return -1f;
            }
        }
        #endregion

        #region<unity functions>
        /// <summary>
        /// Unity reset
        /// </summary>
        public void Reset()
        {
            this.KinematicWheelJoint = this.gameObject.RequireComponent<DWheelJoint>(true);
        }
        #endregion

        #region<match>
        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = KinematicWheelJoint.RotationLimitMinMaxDegrees.X;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = KinematicWheelJoint.RotationLimitMinMaxDegrees.Y;
        }
        #endregion

        #region<save>
        /// <summary>
        /// Add the current position of the switch as a Switch State
        /// </summary>
        /// <returns>The newly created Switch State</returns>
        public override DSwitchState SaveCurrentPositionAsState()
        {
            if (this.KinematicWheelJoint)
            {
                return this.SaveState(PreSpectiveMath.LimitMinMax(this.KinematicWheelJoint.CurrentRevolutionPercentage, 0d, 1d));
            }
            return null;
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
                double statePercentage = SwitchStates[_id].Position - this.KinematicWheelJoint.CurrentRevolutionPercentage;
                double targetAngle = 360d * statePercentage;

                this.KinematicWheelJoint.Rotate(targetAngle, (IntentData _intent) =>
                {
                    if (_intent.EnforceableFraction != 1d)
                    {
                        Debug.LogError("Unable to complete rotation. Travelled " + _intent.EnforceableFraction + " times the requested distance.");
                    }
                });
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }
        #endregion
    }
}
