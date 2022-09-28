using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor
{
    public class DDCMotorLogic : StandardLogicComponent<DDCMotor>
    {
        private const string iVelocity = "iVelocity";
        private const string oPreferredVelocity = "oPreferredVelocity";

        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    {
                        GetSignalInstanceByName(iVelocity), () => Target.Velocity
                    }
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
                return new List<SignalDefinition>()
                {
                    ////Input Only
                    new SignalDefinition(iVelocity, PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Current velocity in degrees/s", _baseValue: 0d),

                    ////Outputs only
                    new SignalDefinition(oPreferredVelocity, PLCSignalDirection.OUTPUT, SupportedSignalType.REAL64, _xmlNote: "Preferred Velocity in degrees/s", _onValueChange: onSignalChanged,
                        _baseValue: 0d)
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
                case oPreferredVelocity:
                    Target.TargetVelocity = (double) _newValue;
                    break;

                default:
                    Debug.LogWarning("Unrecognized PLC output registered");
                    break;
            }
        }

        #endregion

        #endregion
    }
}
