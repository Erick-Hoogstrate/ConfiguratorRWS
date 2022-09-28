using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// Represents a generic cylindrical joint limited by its spline and can rotated around its spline with fixed radius
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// [NOT YET MADE]
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Moving away from spline (CustomCylindricalJointUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving over spline (CustomCylindricalJointUnitTest)
    ///     </Test2>
    ///     <Test3>
    ///     Moving over limits of spline (CustomCylindricalJointUnitTest)
    ///     </Test3>
    ///     <Test4>
    ///     Moving over its cylinder(CustomCylindricalJointUnitTest)
    ///     </Test4>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DCylindricalJoint : ACylindericalJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableCylindericalJoint;
            }
        }
    }
}
