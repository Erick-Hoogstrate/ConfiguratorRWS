using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair.kinematics
{
    /// <summary>
    /// A solver that uses triangles to solve a connection between prismatic and 2 wheel joints
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207370341/WIP+Double+Bar+Linkage+Solver
    /// </feature>
    /// <testing>
    ///     <Test1>
    ///     Moving prismatic and back for leading triangle (CustomBarLinkageSolverUnitTest)
    ///     </Test1>
    ///     <Test2>
    ///     Moving prismatic and back for non-leading triangle (CustomBarLinkageSolverUnitTest)
    ///     </Test2>
    /// </testing>
#endif
    [RequireComponent(typeof(DTransform))]
    public class DBarLinkageSolver : ABarLinkageSolver
    {
        protected override CustomKinematicJointIntentConversion[] jointConversionLookupTable
        {
            get
            {
                return DKinematicLookUpTables.JointLookupTableBarLinkageSolver;
            }
        }
    }
}
