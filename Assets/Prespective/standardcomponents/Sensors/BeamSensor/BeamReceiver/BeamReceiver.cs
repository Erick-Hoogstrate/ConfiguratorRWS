using System.Reflection;
using UnityEngine;
using u040.prespective.prepair.components.sensors;

namespace u040.prespective.standardcomponents.sensors.beamsensor
{
    public class BeamReceiver : QuantitativeSensor, IBeamTarget, ISensor
    {
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        public BeamPathRedirectionPoint resolveHit(Vector3 _hitVector, RaycastHit hit)
        {
            this.Flagged = true;
            return new BeamPathRedirectionPoint(this, hit.point, hit.point, Vector3.zero); //Vector3.zero means absorb ray
        }

        public void lostHit()
        {
            this.Flagged = false;
        }
    }
}
