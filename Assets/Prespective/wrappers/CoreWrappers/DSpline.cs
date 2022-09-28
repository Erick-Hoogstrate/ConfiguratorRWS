using System;

namespace u040.prespective.core
{

    /// <summary>
    /// <description>
    /// Explicit Wrapper class for the ADSpline
    /// </description>
    /// <version 
    ///     ver="1.0.0" 
    ///     author="PWS" 
    ///     date="200727">
    ///     - Resorted to wrapper classes to prevent having to relink all explict implementations of monobehavior when updating the version of prespective
    /// </version>
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
