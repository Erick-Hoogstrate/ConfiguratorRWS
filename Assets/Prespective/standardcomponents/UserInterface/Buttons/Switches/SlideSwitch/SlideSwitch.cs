using u040.prespective.utility;
using u040.prespective.prepair.kinematics;
using UnityEngine;
using System;
using System.Reflection;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.standardcomponents;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    public class SlideSwitch : BaseSwitch, ISensor
    {
#pragma warning disable 0618

        public AFPrismaticJoint PrismaticJoint;

        public override bool LoopingSwitch
        {
            get
            {
                if (PrismaticJoint && PrismaticJoint.ConstrainingSpline)
                {
                    return this.PrismaticJoint.ConstrainingSpline.IsClosed;
                }
                return false;
            }
        }

        [SerializeField] private Vector3 storedTransform;
        protected override bool hasMoved
        {
            get
            {
                if (PrismaticJoint && storedTransform != this.PrismaticJoint.transform.position)
                {
                    this.storedTransform = this.PrismaticJoint.transform.position;
                    return true;
                }
                return false;
            }
        }

        private void Reset()
        {
            this.PrismaticJoint = this.gameObject.RequireComponent<AFPrismaticJoint>(AFPrismaticJoint.GetConcreteExplicitType, true);
        }

        public override void SelectState(int _id)
        {
            try
            {
                this.PrismaticJoint.KinematicTranslation(SwitchStates[_id].Position - this.PrismaticJoint.CurrentPerc, VectorSpace.LocalParent, new Action<float, Vector3>((float percentage, Vector3 translation) =>
                {
                /*Callback here*/
                    if (percentage != 1f)
                    {
                        Debug.LogError("Unable to complete translation. Traveled " + percentage + " times the requested distance.");
                    }
                }));
            }
            catch
            {
                Debug.LogWarning("Cannot select state with id '" + _id + "' since it does not exist.");
            }
        }

        public override float CurrentPositionPercentage
        {
            get
            {
                if (PrismaticJoint)
                {
                    return this.PrismaticJoint.CurrentPerc;
                }
                return -1f;
            }
        }



        protected override void matchOuterTransitionsToLimits()
        {
            SwitchStates[0].LowerTransition = 0f;
            SwitchStates[SwitchStates.Count - 1].UpperTransition = 1f;
        }

        public override SwitchState SaveCurrentPositionAsState()
        {
            if (this.PrismaticJoint)
            {
                return this.SaveState(this.PrismaticJoint.CurrentPerc);
            }
            return null;
        }
#pragma warning restore 0618
    }
}
