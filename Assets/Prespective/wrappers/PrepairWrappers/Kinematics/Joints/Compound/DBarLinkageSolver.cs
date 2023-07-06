using UnityEngine;
using u040.prespective.core.transformation;

namespace u040.prespective.prepair.kinematics.joints.compound
{
    /// <summary>
    /// A solver that uses triangles to solve a connection between prismatic and 2 wheel joints
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [RequireComponent(typeof(DTransform))]
    public class DBarLinkageSolver : ABarLinkageSolver
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get { return DKinematicLookUpTables.JointLookupTableBarLinkageSolver; }
        }
    }
}
