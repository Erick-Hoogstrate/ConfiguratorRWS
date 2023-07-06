using System;
using u040.prespective.math.doubles;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.prepair.kinematics.joints.compound;

namespace u040.prespective.prepair.kinematics
{
    public static class DKinematicLookUpTables
    {
        #region<wheel>
        public static CustomKinematicJointIntentConversion[] JointLookupTableWheelJoint = new CustomKinematicJointIntentConversion[]
        {
            //WHEEL JOINT -> WHEELJOINT
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {
                    if (_relation is WheelWheelRelation)
                    {
                        double rotationScale = (((WheelWheelRelation)_relation).Direct ? 1d : (_relation.Inverse ? -1d : 1d) *  (((AWheelJoint)_from).Radius / ((AWheelJoint)_to).Radius) * _relation.Ratio);

                        return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent = rotationScale * _intent.OriginalPercentageIntent};
                    }
                    else
                    {
                        double rotationScale =  ((AWheelJoint)_from).Radius / ((AWheelJoint)_to).Radius;

                        return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent = (_relation.Inverse ? -1d : 1d) * rotationScale * _intent.OriginalPercentageIntent * _relation.Ratio};
                    }
                })),

            //WHEEL JOINT -> PRISMATIC JOINT
            new CustomKinematicJointIntentConversion(typeof(APrismaticJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {
                    double wheelCircumference = ((AWheelJoint)_from).Radius * 2d * Math.PI;

                    double newPerc = (wheelCircumference * _intent.OriginalPercentageIntent) / ((APrismaticJoint)_to).ConstrainingSpline.SplineLength;

                    return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent =  (_relation.Inverse ? -1d : 1d) * newPerc * _relation.Ratio};

                })),
            
        };
        #endregion

        #region<prismatic>
        public static CustomKinematicJointIntentConversion[] JointLookupTablePrismaticJoint = new CustomKinematicJointIntentConversion[]
        {
            //PRISMATIC JOINT -> WHEELJOINT
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {

                double wheelCircumference = ((AWheelJoint)_to).Radius * 2d * Math.PI;

                double newPerc = (((APrismaticJoint)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / wheelCircumference;

                return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent =  (_relation.Inverse ? -1d : 1d) * newPerc * _relation.Ratio};

                })),

            //PRISMATIC JOINT -> PRISMATIC JOINT
            new CustomKinematicJointIntentConversion(typeof(APrismaticJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {
                double newPerc = (((APrismaticJoint)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / ((APrismaticJoint)_to).ConstrainingSpline.SplineLength;

                return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent =  (_relation.Inverse ? -1d : 1d) * newPerc * _relation.Ratio};

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
            //BAR LINKAGE SOLVER -> WHEELJOINT
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {

                double wheelCircumference = ((AWheelJoint)_to).Radius * 2d * Math.PI;

                double newPerc = (((ABarLinkageSolver)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / wheelCircumference;

                return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent =  (_relation.Inverse ? -1d : 1d) * newPerc * _relation.Ratio};

                })),

            //BAR LINKAGE SOLVER -> PRISMATIC JOINT
            new CustomKinematicJointIntentConversion(typeof(APrismaticJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {
                double newPerc = (((ABarLinkageSolver)_from).ConstrainingSpline.SplineLength * _intent.OriginalPercentageIntent) / ((APrismaticJoint)_to).ConstrainingSpline.SplineLength;

                return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent =  (_relation.Inverse ? -1d : 1d) * newPerc * _relation.Ratio};

                }))
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
            new CustomKinematicJointIntentConversion(typeof(AWheelJoint), new OnCustomTransferIntentGenerator((IntentData _intent, KinematicBody _from, KinematicBody _to, CustomKinematicTransferRelation _relation) =>
                {
                    AWheelJoint target = (AWheelJoint)_to;
                    DVector3 currentForward = target.GlobalForwardDirection.Normalized;
                    DVector3 currentUp = target.GlobalAxisDirection.Normalized;
                    //Define the new axis and forward after _rotation is applied
                    DVector3 rotatedAxis = _intent.OriginalWorldRotation * currentUp;
                    DVector3 rotatedForward = _intent.OriginalWorldRotation * currentForward;
                    //Define the axis correction quaternion and the new forward vector that comes from it
                    DQuaternion rotationToPlane = DQuaternion.FromToRotation(rotatedAxis, target.GlobalAxisDirection);
                    DVector3 newForwardOnPlane = rotationToPlane * rotatedForward;
                    //determine the signed angle by getting the difference of the current forward compared to the plane new forward
                    double signedAngle = DVector3.SignedAngle(currentForward, newForwardOnPlane, currentUp);
                    return new IntentData(){ EnforceableFraction = _intent.EnforceableFraction, OriginalPercentageIntent = (signedAngle / 360d)};
                })),
        };
        #endregion

        #region<gyroscopic>
        public static CustomKinematicJointIntentConversion[] JointLookupTableGyroscopicJoint = new CustomKinematicJointIntentConversion[]
        {

        };
        #endregion

    }
}
