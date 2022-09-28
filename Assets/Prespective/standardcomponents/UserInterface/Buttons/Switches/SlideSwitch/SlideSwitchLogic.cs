using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    #pragma warning disable 0618
    public class SlideSwitchLogic : StandardLogicComponent<SlideSwitch>
    {
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iSelectedID"), () => Target.SelectedState }
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
                    //Inputs
                    //new SignalDefinition("iSelectedState", PLCSignalDirection.INPUT, SupportedSignalType.STRING, "", "Selected State", null, null, "N/A"),
                    new SignalDefinition("iSelectedID", PLCSignalDirection.INPUT, SupportedSignalType.INT32, "", "Selected ID", null, null, -1),
                };
            }
        }
        #endregion
        #endregion
    }
    #pragma warning restore 0618
}
