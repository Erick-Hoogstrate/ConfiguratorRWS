using System.Reflection;
using u040.prespective.prepair.inspector;
using UnityEngine;
using u040.prespective.prepair.components.sensors;
using u040.prespective.standardcomponents;
using u040.prespective.math.doubles;
using u040.prespective.math;

namespace u040.prespective.standardcomponents.sensors.beamsensor
{
    public class DBeamReceiver : QuantitativeSensor, IDBeamTarget, ISensor
    {
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        public DBeamPathRedirectionPoint resolveHit(DVector3 _hitVector, RaycastHit hit)
        {
            this.Flagged = true;
            return new DBeamPathRedirectionPoint(this, hit.point.ToDouble(), hit.point.ToDouble(), DVector3.Zero); //Vector3.zero means absorb ray
        }

        public void lostHit()
        {
            this.Flagged = false;
        }
    }
}
