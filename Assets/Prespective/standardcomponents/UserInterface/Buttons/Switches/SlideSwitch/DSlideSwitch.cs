using u040.prespective.utility;
using u040.prespective.prepair.kinematics;
using UnityEngine;
using System;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.math.doubles;
using u040.prespective.math;
using u040.prespective.prepair;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    public class DSlideSwitch : DBaseSwitch, ISensor
    {
        public APrismaticJoint KinematicPrismaticJoint;

        public override bool LoopingSwitch
        {
            get
            {
                if (KinematicPrismaticJoint && KinematicPrismaticJoint.ConstrainingSpline)
                {
                    return this.KinematicPrismaticJoint.ConstrainingSpline.IsClosed;
                }
                return false;
            }
        }

        [SerializeField] private DVector3 storedTransform;
        protected override bool hasMoved
        {
            get
            {
                if (KinematicPrismaticJoint && storedTransform != this.KinematicPrismaticJoint.transform.position.ToDouble())
                {
                    this.storedTransform = this.KinematicPrismaticJoint.transform.position.ToDouble();
                    return true;
                }
                return false;
            }
        }

        private void Reset()
        {
            this.KinematicPrismaticJoint = this.gameObject.RequireComponent<DPrismaticJoint>(true);
        }

        public override void SelectState(int _id)
        {
            try
            {
                this.KinematicPrismaticJoint.Translate(SwitchStates[_id].Position - this.KinematicPrismaticJoint.CurrentPercentage, (IntentData _intent) =>
                {
                    /*Callback here*/
                    if (_intent.EnforcableFraction != 1d)
                    {
                        Debug.LogError("Unable to complete translation. Traveled " + _intent.EnforcableFraction + " times the requested distance.");
                    }
                });
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }

        public override double CurrentPositionPercentage
        {
            get
            {
                if (KinematicPrismaticJoint)
                {
                    return this.KinematicPrismaticJoint.CurrentPercentage;
                }
                return -1d;
            }
        }



        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = 0d;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = 1d;
        }

        public override DSwitchState SaveCurrentPositionAsState()
        {
            return this.SaveState(this.KinematicPrismaticJoint.CurrentPercentage);
        }
    }
}
