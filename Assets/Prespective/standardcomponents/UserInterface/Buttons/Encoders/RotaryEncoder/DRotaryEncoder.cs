using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class DRotaryEncoder : DBaseEncoder, ISensor
    {
        public AWheelJoint KinematicWheelJoint;

        private void Reset()
        {
            KinematicWheelJoint = this.RequireComponent<DWheelJoint>();
        }

        protected override void FixedUpdate()
        {
            if (KinematicWheelJoint == null)
            {
                Debug.LogError("Cannot function without a WheelJoint assigned.");
                return;
            }

            updateValue(this.KinematicWheelJoint.CurrentRevolutionPercentage);
        }
    }
}
