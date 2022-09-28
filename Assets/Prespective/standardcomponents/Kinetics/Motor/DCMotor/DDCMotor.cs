using u040.prespective.prepair.physics.kinetics.motor;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor
{
    public class DDCMotor : DBaseMotor, IActuator
    {
        public void StartRotation()
        {
            if (this.TargetVelocity == 0d)
            {
                this.TargetVelocity = this.MaxVelocity;
            }
        }

        public void StopRotation()
        {
            if (this.TargetVelocity != 0d)
            {
                this.TargetVelocity = 0d;
            }
        }
    }
}
