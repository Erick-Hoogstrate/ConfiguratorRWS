using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic complex joint has a direction which is constrained by a closed spline
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// [NOT YET MADE]
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Moving within limit (CustomComplexJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving outside limit (CustomComplexJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     Rotating within limit (CustomComplexJointUnitTest)
    ///     </Test3>
    ///     <Test4>
    ///     Rotating outside limit (CustomComplexJointUnitTest)
    ///     </Test4>
    ///     <Test5>
    ///     https://unit040.atlassian.net/wiki/spaces/TEST/pages/9633835/Complex+Joint
    ///     </Test5>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DComplexJoint : AComplexJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableComplexJoint;
            }
        }
    }
}
