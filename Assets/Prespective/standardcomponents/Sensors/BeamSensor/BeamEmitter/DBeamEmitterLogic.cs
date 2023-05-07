using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.systems.beam.logic
{
    /// <summary>
    /// Logic of the DBeam Emitter
    /// </summary>
    public class DBeamEmitterLogic : StandardLogicComponent<DBeamEmitter>
    {
        #region << CONSTANTS >>   
        private const string O_ACTIVE = "oActive";
        private const string I_ACTIVE = "iActive";
        #endregion
        #region << PROPERTIES >>
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName(I_ACTIVE), () => Target.IsActive }
                };
            }
        }
        #region <<PLC Signals>>
        #region <<Signal Definitions>>
        /// <summary>
        /// Declare the IO signals
        /// </summary>
        public override List<SignalDefinition> SignalDefinitions
        {
            get
            {
                return new List<SignalDefinition>() {
                    new SignalDefinition(I_ACTIVE, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Sensor active", null, null, true),
                    new SignalDefinition(O_ACTIVE, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Sensor active", onSignalChanged, null, true),
                };
            }
        }
        #endregion
        #region <<PLC Outputs>>
        /// <summary>
        /// General callback for the IOs
        /// </summary>
        /// <param name="_signal">the signal that has changed</param>
        /// <param name="_newValue">the new value</param>
        /// <param name="_newValueReceived">the time of the value change</param>
        /// <param name="_oldValue">the old value</param>
        /// <param name="_oldValueReceived">the time of the old value change</param>
        void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
        {
            switch (_signal.Definition.DefaultSignalName)
            {
                case O_ACTIVE:
                    Target.IsActive = (bool)_newValue;
                    break;

                default:
                    Debug.LogWarning("Unrecognized PLC output registered");
                    break;
            }

        }
        #endregion
        #endregion
        #endregion
    }
}
