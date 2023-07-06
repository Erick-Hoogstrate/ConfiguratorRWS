using UnityEngine;
using u040.prespective.core.transformation;

namespace u040.prespective.prepair.kinematics.joints.basic
{
    /// <summary>
    /// Represents a generic Wheel joint with angle limits
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [RequireComponent(typeof(DTransform))]
    public class DWheelJoint : AWheelJoint
    {
        #region <Properties>
        #region PROTECTED / PRIVATE
        /// <summary>
        /// get lookup table wheel joint
        /// </summary>
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableWheelJoint;
            }
        }
        #endregion
        #endregion
    }
}
