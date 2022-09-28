using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic Prismatic joint relation between a spline and a geometric object
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207369965/WIP+Double+Prismatic+Joint
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Moving over the spline within limits (CustomPrismaticJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving over the spline outside it limits (CustomPrismaticJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     Moving away from the spline (CustomPrismaticJointUnitTest)
    ///     </Test3>
    ///     <Test4>
    ///     Moving over spline with rotation on it(CustomPrismaticJointUnitTest)
    ///     </Test4>
    ///     <Test5>
    ///     https://unit040.atlassian.net/wiki/spaces/TEST/pages/2818070/Prismatic+Joint
    ///     </Test5>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DPrismaticJoint : APrismaticJoint
    {
        #region<<Fields>>
        /// <summary>
        /// look up table how to process  
        /// </summary>
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get 
            { 
                return DKinematicLookUpTables.JointLookupTablePrismaticJoint; 
            }
        }
        #endregion
    }
}
