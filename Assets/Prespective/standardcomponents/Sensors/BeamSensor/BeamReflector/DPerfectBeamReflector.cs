using u040.prespective.math;
using u040.prespective.math.doubles;
using u040.prespective.prepair.components.sensors;
using u040.prespective.standardcomponents;
using u040.prespective.utility;
using UnityEngine;

namespace u040.prespective.standardcomponents.sensors.beamsensor
{
    public class DPerfectBeamReflector : DBaseBeamReflector, ISensor
    {
        public void Reset()
        {
            Collider _collider = this.GetComponent<Collider>();

            if(!_collider)
            {
                Debug.LogWarning("The " + this.GetType().Name + " component on " + this.gameObject.name + " GameObject requires a Collider component in order to function.");
            }
        }

        public override DBeamPathRedirectionPoint resolveHit(DVector3 _hitVector, RaycastHit hit)
        {
            return new DBeamPathRedirectionPoint(this, hit.point.ToDouble(), hit.point.ToDouble(), DVector3.Reflect(_hitVector, hit.normal.ToDouble()));
        }

        public override void lostHit()
        {
            //Do Nothing
        }
    }
}
