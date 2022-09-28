using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor
{
    #pragma warning disable 0618
    public class DCMotorLogic : StandardLogicComponent<DCMotor>
    {
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iVelocity"), () => Target.Velocity }
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
                    ////Input Only
                    new SignalDefinition("iVelocity", PLCSignalDirection.INPUT, SupportedSignalType.REAL32, "", "Current velocity in degrees/s", null, null, 0f),

                    ////Outputs only
                    new SignalDefinition("oPreferredVelocity", PLCSignalDirection.OUTPUT, SupportedSignalType.REAL32, "", "Preferred Velocity in degrees/s", onSignalChanged, null, 0f),
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
                case "oPreferredVelocity":
                    Target.TargetVelocity = (float)_newValue;
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
