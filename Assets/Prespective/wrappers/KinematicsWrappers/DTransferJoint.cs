using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic transfer joint which applies the orientation change to a other connected game object with ability to fix orientation
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// [NOT MADE YET]
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Moving direction non fixed (CustomTransferJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving direction fixed (CustomTransferJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     Rotating non fixed (CustomTransferJointUnitTest)
    ///     </Test3>
    ///     <Test4>
    ///     Rotating fixed(CustomTransferJointUnitTest)
    ///     </Test4>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DTransferJoint : ATransferJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableTransferJoint;
            }
        }
    }
}
