using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic helical joint
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207370075/WIP+Double+Helical+Joint
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Setting pitch and checking if spline control points values are correct (CustomHelicalJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     https://unit040.atlassian.net/wiki/spaces/TEST/pages/11436687/Helical+Joint
    ///     </Test2>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DHelicalJoint : AHelicalJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableHelicalJoint;
            }
        }
    }
}
