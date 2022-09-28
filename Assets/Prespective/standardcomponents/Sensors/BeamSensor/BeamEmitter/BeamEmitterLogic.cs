using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.sensors.beamsensor
{
    #pragma warning disable 0618
    public class BeamEmitterLogic : StandardLogicComponent<BeamEmitter>
    {
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iActive"), () => Target.IsActive }
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
                    new SignalDefinition("iActive", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Sensor active", null, null, true),
                    new SignalDefinition("oActive", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Sensor active", onSignalChanged, null, true),
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
            switch (_signal.definition.defaultSignalName)
            {
                case "oActive":
                    Target.IsActive = (bool)_newValue;
                    break;

                default:
                    Debug.LogWarning("Unrecognized PLC output registered");
                    break;
            }

        }
        #endregion
        #endregion
    }
    #pragma warning restore 0618
}
