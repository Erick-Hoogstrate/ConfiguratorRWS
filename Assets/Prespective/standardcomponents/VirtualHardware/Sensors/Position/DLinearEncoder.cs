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
    public class DLinearEncoder : DBaseEncoder, ISensor
    {
        /// <summary>
        /// The Prismatic Joint used for the Linear Encoder
        /// </summary>
        public APrismaticJoint KinematicPrismaticJoint;

        /// <summary>
        /// Unity Reset
        /// </summary>
        public void Reset()
        {
            KinematicPrismaticJoint = this.RequireComponent<APrismaticJoint>(APrismaticJoint.GetConcreteExplicitType, true);
        }

        /// <summary>
        /// Unity Update
        /// </summary>
        public override void FixedUpdate()
        {
            if (KinematicPrismaticJoint == null)
            {
                Debug.LogError("Cannot function without a KinematicPrismaticJoint assigned.");
                return;
            }

            //Call function in DBaseEncoder
            updateValue(this.KinematicPrismaticJoint.CurrentPercentage);
        }
    }
}
