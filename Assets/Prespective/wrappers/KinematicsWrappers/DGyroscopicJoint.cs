using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic gyroscopic joint which keeps a game object orientated to the same up direction
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207370075/WIP+Double+Helical+Joint
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Setting up direction (CustomGyroscopicJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Rotating base object and check if rotated up back to orginal up direction (CustomGyroscopicJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     https://unit040.atlassian.net/wiki/spaces/TEST/pages/9437461/Gyroscopic+Joint
    ///     </Test3>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DGyroscopicJoint : AGyroscopicJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableGyroscopicJoint;
            }
        }
    }
}
