using u040.prespective.math;
using u040.prespective.math.doubles;
using u040.prespective.prepair;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    public class DRotarySwitch : DBaseSwitch, ISensor
    {
        public AWheelJoint KinematicWheelJoint;

        public override bool LoopingSwitch
        {
            get 
            {
                if (KinematicWheelJoint)
                {
                    return !this.KinematicWheelJoint.ApplyKinematicLimit;
                }
                return false;
            }
        }

        [SerializeField] private DQuaternion storedRotation;
        protected override bool hasMoved
        {
            get
            {
                if (KinematicWheelJoint && storedRotation != this.KinematicWheelJoint.DTransform.Rotation)
                {
                    this.storedRotation = this.KinematicWheelJoint.DTransform.Rotation;
                    return true;
                }
                return false;
            }
        }

        public override double CurrentPositionPercentage
        {
            get
            {
                if (KinematicWheelJoint)
                {
                    return this.KinematicWheelJoint.CurrentRevolutionPercentage;
                }
                return -1f;
            }
        }

        private void Reset()
        {
            this.KinematicWheelJoint = this.gameObject.RequireComponent<DWheelJoint>(true);
        }

        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = KinematicWheelJoint.RotationLimitMinMaxDeg.X;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = KinematicWheelJoint.RotationLimitMinMaxDeg.Y;
        }

        public override DSwitchState SaveCurrentPositionAsState()
        {
            if (this.KinematicWheelJoint)
            {
                return this.SaveState(PreSpectiveMath.LimitMinMax(this.KinematicWheelJoint.CurrentRevolutionPercentage, 0d, 1d));
            }
            return null;
        }

        public override void SelectState(int _id)
        {
            try
            {
                double _statePercentage = SwitchStates[_id].Position - this.KinematicWheelJoint.CurrentRevolutionPercentage;
                double _targetAngle = 360d * _statePercentage;

                this.KinematicWheelJoint.Rotate(_targetAngle, (IntentData _intent) =>
                {
                    /*Callback here*/
                    if (_intent.EnforcableFraction != 1d)
                    {
                        Debug.LogError("Unable to complete rotation. Traveled " + _intent.EnforcableFraction + " times the requested distance.");
                    }
                });
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }
    }
}
