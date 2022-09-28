using System;
using u040.prespective.math;
using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    public class RotarySwitch : BaseSwitch, ISensor
    {
#pragma warning disable 0618

        public AFWheelJoint WheelJoint;

        public override bool LoopingSwitch
        {
            get 
            {
                if (WheelJoint)
                {
                    return !this.WheelJoint.ApplyKinematicLimit;
                }
                return false;
            }
        }

        [SerializeField] private Quaternion storedRotation;
        protected override bool hasMoved
        {
            get
            {
                if (WheelJoint && storedRotation != this.WheelJoint.transform.rotation)
                {
                    this.storedRotation = this.WheelJoint.transform.rotation;
                    return true;
                }
                return false;
            }
        }

        public override float CurrentPositionPercentage
        {
            get
            {
                if (WheelJoint)
                {
                    return this.WheelJoint.CurrentRevolutionPercentage;
                }
                return -1f;
            }
        }

        private void Reset()
        {
            this.WheelJoint = this.gameObject.RequireComponent<AFWheelJoint>(AFWheelJoint.GetConcreteExplicitType, true);
        }

        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = WheelJoint.RotationLimitMinMaxDeg.x;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = WheelJoint.RotationLimitMinMaxDeg.y;
        }

        public override SwitchState SaveCurrentPositionAsState()
        {
            if (this.WheelJoint)
            {
                return this.SaveState(PreSpectiveMath.LimitMinMax(this.WheelJoint.CurrentRevolutionPercentage, 0f, 1f));
            }
            return null;
        }

        public override void SelectState(int _id)
        {
            try
            {
                float _statePercentage = SwitchStates[_id].Position - this.WheelJoint.CurrentRevolutionPercentage;
                float _targetAngle = 360f * _statePercentage;

                this.WheelJoint.KinematicRotation(_targetAngle, VectorSpace.LocalParent, new Action<float, Quaternion>((float _percentage, Quaternion _rotation) =>
                {
                    /*Callback here*/
                    if (_percentage != 1f)
                    {
                        Debug.LogError("Unable to complete rotation. Traveled " + _percentage + " times the requested distance.");
                    }
                }));
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }
#pragma warning restore 0618
    }
}
