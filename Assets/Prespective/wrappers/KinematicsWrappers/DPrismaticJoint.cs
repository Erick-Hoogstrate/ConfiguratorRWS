using UnityEngine;
using u040.prespective.core.transformation;

namespace u040.prespective.prepair.kinematics.joints.basic
{
    /// <summary>
    /// Represents a generic Prismatic joint relation between a spline and a geometric object
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
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
