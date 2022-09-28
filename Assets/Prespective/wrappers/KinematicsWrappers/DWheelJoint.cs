using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic Wheel joint with angle limits
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207370209/WIP+Double+Wheel+Joint
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Setting forward and normal direction (CustomWheelJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving over wheel joint axis now limits (CustomWheelJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     Moving over wheel joint axis with limits (CustomWheelJointUnitTest)
    ///     </Test3>
    ///     <Test4>
    ///     Moving over wrong wheel joint axis(CustomWheelJointUnitTest)
    ///     </Test4>
    ///     <Test5>
    ///     Same test with before rotated wheel(CustomWheelJointUnitTest)
    ///     </Test5>
    ///     <Test6>
    ///     https://unit040.atlassian.net/wiki/spaces/TEST/pages/2228248/Wheel+Joint
    ///     </Test6>
    /// </testing>
#endif
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
