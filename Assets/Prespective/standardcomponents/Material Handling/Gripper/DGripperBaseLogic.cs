using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.systems.gripper.logic
{
    public class DGripperBaseLogic : StandardLogicComponent<DGripperBase>
    {
        
        private const string iCurrentPercentage = "iCurrentPercentage";
        private const string oClose = "oClose";

        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    {
                        GetSignalInstanceByName(iCurrentPercentage), () => Target.ClosePercentage
                    },
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
                    new SignalDefinition(iCurrentPercentage, PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Current Percentage", _baseValue: 0f),

                    //Output only
                    new SignalDefinition(oClose, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Close gripper", _onValueChange: onSignalChanged, _baseValue: false),
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
                case oClose:
                    bool closed = (bool) _newValue;
                    Target.TargetClosePercentage = closed ? 1f : 0f;
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
