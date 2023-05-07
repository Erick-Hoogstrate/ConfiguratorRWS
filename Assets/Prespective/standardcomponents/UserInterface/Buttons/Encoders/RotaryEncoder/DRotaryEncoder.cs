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
    public class DRotaryEncoder : DBaseEncoder, ISensor
    {
        /// <summary>
        /// The Wheel Joint used for the Rotary Encoder
        /// </summary>
        public AWheelJoint KinematicWheelJoint;

        /// <summary>
        /// Unity Reset
        /// </summary>
        public void Reset()
        {
            KinematicWheelJoint = this.RequireComponent<DWheelJoint>();
        }

        /// <summary>
        /// Unity Update
        /// </summary>
        public override void FixedUpdate()
        {
            if (KinematicWheelJoint == null)
            {
                Debug.LogError("Cannot function without a WheelJoint assigned.");
                return;
            }

            //Call function in DBaseEncoder
            updateValue(this.KinematicWheelJoint.CurrentRevolutionPercentage);
        }
    }
}
