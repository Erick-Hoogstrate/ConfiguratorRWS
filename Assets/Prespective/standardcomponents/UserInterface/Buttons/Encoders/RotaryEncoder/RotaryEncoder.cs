using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class RotaryEncoder : BaseEncoder, ISensor
    {
#pragma warning disable 0618

        public AFWheelJoint WheelJoint;

        private void Reset()
        {
            WheelJoint = this.RequireComponent<AFWheelJoint>(AFWheelJoint.GetConcreteExplicitType);
        }

        protected override void FixedUpdate()
        {
            if (WheelJoint == null)
            {
                Debug.LogError("Cannot function without a WheelJoint assigned.");
                return;
            }

            updateValue(this.WheelJoint.CurrentRevolutionPercentage);
        }
#pragma warning restore 0618
    }
}
