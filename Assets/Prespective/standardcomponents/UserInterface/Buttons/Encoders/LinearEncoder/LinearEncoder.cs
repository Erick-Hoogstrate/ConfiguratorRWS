using u040.prespective.prepair.kinematics;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class LinearEncoder : BaseEncoder, ISensor
    {
#pragma warning disable 0618

        public AFPrismaticJoint PrismaticJoint;

        private void Reset()
        {
            PrismaticJoint = this.RequireComponent<AFPrismaticJoint>(AFPrismaticJoint.GetConcreteExplicitType);
        }

        protected override void FixedUpdate()
        {
            if (PrismaticJoint == null)
            {
                Debug.LogError("Cannot function without a PrismaticJoint assigned.");
                return;
            }

            updateValue(this.PrismaticJoint.CurrentPerc);
        }
#pragma warning restore 0618
    }
}
