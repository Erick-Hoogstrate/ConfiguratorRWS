using UnityEngine;
using System.Collections.Generic;
using u040.prespective.prepair.virtualhardware.sensors;
using u040.prespective.prepair.virtualhardware.systems.beam;

namespace u040.prespective.standardcomponents.virtualhardware.systems.beam
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DBeamReceiver : QuantitativeSensor, IDBeamTarget, ISensor
    {
        #region<properties>
        private List<IDBeamEmitter> hitEmitters = new List<IDBeamEmitter>();
        #endregion

        #region<hit>
        /// <summary>
        /// This method is triggered when the Beam Receiver is being hit by a beam
        /// </summary>
        /// <param name="_hitVector">The direction the beam is coming from</param>
        /// <param name="_hit">The hit info from the raycast</param>
        /// <param name="_emitter">The Beam Emitter sending out the beam</param>
        /// <returns></returns>
        public DBeamPathRedirectionPoint ResolveHit(Vector3 _hitVector, RaycastHit _hit, IDBeamEmitter _emitter)
        {
            if (!hitEmitters.Contains(_emitter))
            {
                hitEmitters.Add(_emitter);
            }
            this.flagged = true;
            return new DBeamPathRedirectionPoint(this, _hit.point, _hit.point, Vector3.zero);
        }

        /// <summary>
        /// This method is triggered when the Beam Receiver is no longer being hit by a beam
        /// </summary>
        /// <param name="_emitter">The Beam Emitter sending out the beam</param>
        public void LostHit(IDBeamEmitter _emitter)
        {
            hitEmitters.Remove(_emitter);
            if (hitEmitters.Count == 0)
            {
                this.flagged = false;
            }
        }
        #endregion
    }
}
