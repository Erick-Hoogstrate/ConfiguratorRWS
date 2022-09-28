using System;

namespace u040.prespective.prepair.kinematics
{
    public static class DKinematicLookUpTables
    {
        #region<wheel>
        public static CustomKinematicJointIntentConversion[] JointLookupTableWheelJoint = new CustomKinematicJointIntentConversion[]
        {
            //WHEEL JOINT -> WHEELJOINT
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, bool _inverse, double _ratio) =>
                {
                    double rotationScale =  ((AWheelJoint)_from).Radius / ((AWheelJoint)_to).Radius;

                    return new IntentData(){ EnforcableFraction = _intent.EnforcableFraction, OriginalPercentageIntent = (_inverse ? -1d : 1d) * rotationScale * _intent.OriginalPercentageIntent * _ratio};
                })),

            //WHEEL JOINT -> PRISMATIC JOINT
            new CustomKinematicJointIntentConversion(typeof(APrismaticJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, bool _inverse, double _ratio) =>
                {
                    double wheelCircumference = ((AWheelJoint)_from).Radius * 2d * Math.PI;

                    double newPerc = (wheelCircumference * _intent.OriginalPercentageIntent) / ((APrismaticJoint)_to).ConstrainingSpline.SplineLength;

                    return new IntentData(){ EnforcableFraction = _intent.EnforcableFraction, OriginalPercentageIntent =  (_inverse ? -1d : 1d) * newPerc * _ratio};

                })),
            
        };
        #endregion

        #region<prismatic>
        public static CustomKinematicJointIntentConversion[] JointLookupTablePrismaticJoint = new CustomKinematicJointIntentConversion[]
        {
            //PRISMATIC JOINT -> WHEELJOINT
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, bool _inverse, double _ratio) =>
                {

                double wheelCircumference = ((AWheelJoint)_to).Radius * 2d * Math.PI;

                double newPerc = (((APrismaticJoint)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / wheelCircumference;

                return new IntentData(){ EnforcableFraction = _intent.EnforcableFraction, OriginalPercentageIntent =  (_inverse ? -1d : 1d) * newPerc * _ratio};

                })),

            //PRISMATIC JOINT -> PRISMATIC JOINT
            new CustomKinematicJointIntentConversion(typeof(APrismaticJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, bool _inverse, double _ratio) =>
                {
                double newPerc = (((APrismaticJoint)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / ((APrismaticJoint)_to).ConstrainingSpline.SplineLength;

                return new IntentData(){ EnforcableFraction = _intent.EnforcableFraction, OriginalPercentageIntent =  (_inverse ? -1d : 1d) * newPerc * _ratio};

                })),
            
        };
        #endregion

        #region<helical>
        public static CustomKinematicJointIntentConversion[] JointLookupTableHelicalJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

        #region<cylinderical>
        public static CustomKinematicJointIntentConversion[] JointLookupTableCylindericalJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

        #region<bar linkage solver>
        public static CustomKinematicJointIntentConversion[] JointLookupTableBarLinkageSolver = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

        #region<complex>
        public static CustomKinematicJointIntentConversion[] JointLookupTableComplexJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

        #region<transfer>
        public static CustomKinematicJointIntentConversion[] JointLookupTableTransferJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

        #region<gyroscopic>
        public static CustomKinematicJointIntentConversion[] JointLookupTableGyroscopicJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

    }
}
