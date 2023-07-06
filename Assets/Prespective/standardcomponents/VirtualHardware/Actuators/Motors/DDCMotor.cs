using u040.prespective.prepair.virtualhardware.actuators;
using u040.prespective.prepair.virtualhardware.actuators.motors;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.motors
{
    /// <summary>
    /// A motor that can be started and stopped with a given acceleration and deceleration
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DDCMotor : DBaseMotor, IActuator
    {
        #region<rotation control>
        /// <summary>
        /// Start the rotation
        /// </summary>
        public void StartRotation()
        {
            if (this.TargetVelocity == 0d)
            {
                this.TargetVelocity = this.MaxVelocity;
            }
        }

        /// <summary>
        /// Stop the rotation
        /// </summary>
        public void StopRotation()
        {
            if (this.TargetVelocity != 0d)
            {
                this.TargetVelocity = 0d;
            }
        }
        #endregion
    }
}
