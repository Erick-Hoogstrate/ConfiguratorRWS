using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.light.logic
{
    /// <summary>
    /// Logic of color sensor
    /// </summary>
    public class ColorSensorLogic : StandardLogicComponent<ColorSensor>
    {
        #region << CONSTANTS >>
        private const string O_ACTIVE = "oActive";

        private const string I_ACTIVE = "iActive";
        private const string I_SENSOR_OUTPUT = "iSensorOutput";
        private const string I_RED = "iRed";
        private const string I_GREEN = "iGreen";
        private const string I_BLUE = "iBlue";
        #endregion
        #region << PROPERTIES >>
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    {GetSignalInstanceByName(I_SENSOR_OUTPUT), () => Target.IsActive},
                    {GetSignalInstanceByName(I_RED), () => Mathf.RoundToInt(Target.OutputSignal.r * 255f)},
                    {GetSignalInstanceByName(I_GREEN), () => Mathf.RoundToInt(Target.OutputSignal.g * 255f)},
                    {GetSignalInstanceByName(I_BLUE), () => Mathf.RoundToInt(Target.OutputSignal.b * 255f)}
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
                    //Inputs only
                    new SignalDefinition(I_ACTIVE, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, _xmlNote: "iActive", _baseValue: true),
                    new SignalDefinition(I_SENSOR_OUTPUT, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", _xmlNote: "Flagged", _baseValue: false),
                    new SignalDefinition(I_RED, PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", _xmlNote: "Red color value", _baseValue: 0),
                    new SignalDefinition(I_GREEN, PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", _xmlNote: "Green color value", _baseValue: 0),
                    new SignalDefinition(I_BLUE, PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", _xmlNote: "Blue color value", _baseValue: 0),

                    //Input / output
                    new SignalDefinition(O_ACTIVE, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "oActive", _onValueChange: onSignalChanged, _baseValue: true),

                    //Output only
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
                    Target.IsActive = (bool) _newValue;
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
