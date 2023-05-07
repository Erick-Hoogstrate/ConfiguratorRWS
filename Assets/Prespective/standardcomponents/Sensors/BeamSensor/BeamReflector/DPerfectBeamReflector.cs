using u040.prespective.prepair.virtualhardware.sensors;
using u040.prespective.prepair.virtualhardware.systems.beam;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.systems.beam
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DPerfectBeamReflector : DBaseBeamReflector, ISensor
    {
        #region<unity functions>
        /// <summary>
        /// Unity reset
        /// </summary>
        public void Reset()
        {
            Collider collider = this.GetComponent<Collider>();

            if(!collider)
            {
                Debug.LogWarning("The " + this.GetType().Name + " component on " + this.gameObject.name + " GameObject requires a Collider component in order to function.");
            }
        }
        #endregion

        #region<hit>
        /// <summary>
        /// This method is triggered when the Beam Reflector is being hit by a beam
        /// </summary>
        /// <param name="_hitVector">The direction the beam is coming from</param>
        /// <param name="_hit">The hit info from the ray cast</param>
        /// <param name="_emitter">The Beam Emitter sending out the beam</param>
        /// <returns></returns>
        public override DBeamPathRedirectionPoint ResolveHit(Vector3 _hitVector, RaycastHit _hit, IDBeamEmitter _emitter)
        {
            return new DBeamPathRedirectionPoint(this, _hit.point, _hit.point, Vector3.Reflect(_hitVector, _hit.normal));
        }

        /// <summary>
        /// This method is triggered when the Beam Reflector is no longer being hit by a beam
        /// </summary>
        /// <param name="_emitter">The Beam Emitter sending out the beam</param>
        public override void LostHit(IDBeamEmitter _emitter)
        {
            //Do Nothing
        }
        #endregion
    }
}
