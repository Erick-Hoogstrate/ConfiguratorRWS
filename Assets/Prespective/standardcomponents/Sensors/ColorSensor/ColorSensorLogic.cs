using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.sensors.colorsensor
{
    #pragma warning disable 0618
    public class ColorSensorLogic : StandardLogicComponent<ColorSensor>
    {
        public int iRed = 0;
        public int iGreen = 0;
        public int iBlue = 0;
        public bool iActive = true;
        public bool oActive = true;

        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iSensorOutput"), () => Target.IsActive },
                    { GetSignalInstanceByName("iRed"), () => Mathf.RoundToInt(Target.OutputSignal.r * 255f) },
                    { GetSignalInstanceByName("iGreen"), () => Mathf.RoundToInt(Target.OutputSignal.g * 255f) },
                    { GetSignalInstanceByName("iBlue"), () => Mathf.RoundToInt(Target.OutputSignal.b * 255f) }
                };
            }
        }

        private void Reset()
        {
            this.implicitNamingRule.instanceNameRule = "GVLs." + this.GetType().Name + "[{{INDEX_IN_PARENT}}]";
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
                    //Inputs only
                    new SignalDefinition("iSensorOutput", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Flagged", null, null, false),
                    new SignalDefinition("iRed", PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", "Red color value", null, null, 0),
                    new SignalDefinition("iGreen", PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", "Green color value", null, null, 0),
                    new SignalDefinition("iBlue", PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", "Blue color value", null, null, 0),

                    //Input / output
                    new SignalDefinition("iActive", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Active", null, null, true),
                    new SignalDefinition("oActive", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Active", onSignalChanged, null, true),

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
