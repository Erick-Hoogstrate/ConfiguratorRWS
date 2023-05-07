using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.position.logic
{
    /// <summary>
    /// Logic of the DRotarySwitch
    /// </summary>
    public class DRotarySwitchLogic : StandardLogicComponent<DRotarySwitch>
    {
        #region << CONSTANTS >>
        private const string I_SELECTED_ID = "iSelectedID";
        #endregion

        #region << PROPERTIES >>
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                {
                    return new Dictionary<SignalInstance, Func<object>>
                    {
                        {GetSignalInstanceByName(I_SELECTED_ID), () => Target.SelectedState.Id}
                    };
                }
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
                    //Inputs
                    new SignalDefinition(I_SELECTED_ID, PLCSignalDirection.INPUT, SupportedSignalType.INT32, _xmlNote: "Selected ID", _baseValue: -1),
                };
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
