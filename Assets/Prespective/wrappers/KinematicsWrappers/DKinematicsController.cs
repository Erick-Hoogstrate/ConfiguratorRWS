using UnityEngine;
using u040.prespective.core;

namespace u040.prespective.prepair
{
    /// <summary>
    /// Controller needed to process the kinematics and their connections between each other
    /// 
    /// <para>Copyright (c) 2015-2021 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
#if !RELEASE
    /// <feature>
    /// https://unit040.atlassian.net/wiki/spaces/PUD/pages/1207369773/WIP+Double+Body+Controller
    /// </feature>
#endif
    [ExecuteInEditMode]
    [RequireComponent(typeof(DTransform))]
    public class DKinematicsController : AKinematicsController
    {


    }
}
