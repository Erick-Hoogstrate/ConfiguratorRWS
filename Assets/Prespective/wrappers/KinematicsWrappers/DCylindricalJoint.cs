using UnityEngine;
using u040.prespective.core.transformation;

namespace u040.prespective.prepair.kinematics.joints.basic
{
    /// <summary>
    /// Represents a generic cylindrical joint limited by its spline and can rotated around its spline with fixed radius
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [RequireComponent(typeof(DTransform))]
    public class DCylindricalJoint : ACylindricalJoint
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
