using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class DLinearEncoderLogic : StandardLogicComponent<DLinearEncoder>
    {
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iValue"), () => Target.OutputSignal }
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
                    new SignalDefinition("iValue", PLCSignalDirection.INPUT, SupportedSignalType.REAL64, "", "Value", null, null, 0d),
                };
            }
        }
        #endregion
        #endregion
    }
}
