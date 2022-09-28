using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.standardcomponents;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class DLinearEncoder : DBaseEncoder, ISensor
    {
        public APrismaticJoint KinematicPrismaticJoint;

        private void Reset()
        {
            KinematicPrismaticJoint = this.RequireComponent<DPrismaticJoint>();
        }

        protected override void FixedUpdate()
        {
            if (KinematicPrismaticJoint == null)
            {
                Debug.LogError("Cannot function without a KinematicPrismaticJoint assigned.");
                return;
            }

            updateValue(this.KinematicPrismaticJoint.CurrentPercentage);
        }
    }
}
