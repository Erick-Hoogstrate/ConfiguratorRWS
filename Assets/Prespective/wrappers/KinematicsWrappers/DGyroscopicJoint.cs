using UnityEngine;
using u040.prespective.core.transformation;

namespace u040.prespective.prepair.kinematics.joints.extended
{
    /// <summary>
    /// Represents a generic gyroscopic joint which keeps a game object orientated to the same up direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [RequireComponent(typeof(DTransform))]
    public class DGyroscopicJoint : AGyroscopicJoint
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get { return DKinematicLookUpTables.JointLookupTableGyroscopicJoint; }
        }
    }
}
