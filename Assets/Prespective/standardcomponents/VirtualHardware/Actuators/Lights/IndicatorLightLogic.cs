using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.lights.logic
{
    public class IndicatorLightLogic : StandardLogicComponent<IndicatorLight>
    {
        private const string oActive = "oActive";
        private const string oIntensity = "oIntensity";

        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get { return null; }
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
                    new SignalDefinition(oActive, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Light active", _onValueChange: onSignalChanged, _baseValue: false),
                    new SignalDefinition(oIntensity, PLCSignalDirection.OUTPUT, SupportedSignalType.REAL32, _xmlNote: "Light intensity", _onValueChange: onSignalChanged, _baseValue: 1f),
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
                case oActive:
                    Target.SetActive((bool) _newValue);
                    break;

                case oIntensity:
                    Target.Intensity = (float) _newValue;
                    ;
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
