using System;

namespace u040.prespective.core.spline
{
    /// <summary>
    /// Creates a Spline with double precision in the Unity Editor
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DSpline : ADSpline
    {
        /// <summary>
        /// The explicit controlpoint type used by this explicit implementation of the ADSpline
        /// </summary>
        public override Type ControlPointType => typeof(DSplineControlPoint);

        /// <summary>
        /// The Explicit NPrecision Affected Child Component type that should change with the changing of this precision
        /// </summary>
        public override Type[] NPrecisionAffectedChildComponentTypes => new Type[]{ typeof(DSplineControlPoint) };
    }
}
