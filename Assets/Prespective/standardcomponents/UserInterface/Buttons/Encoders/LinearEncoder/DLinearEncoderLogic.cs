using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.standardcomponents.logic;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.position.logic
{
    public class DLinearEncoderLogic : StandardLogicComponent<DLinearEncoder>
    {
        private const string iValue = "iValue";

        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    {GetSignalInstanceByName(iValue), () => Target.OutputSignal}
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
                    new SignalDefinition(iValue, PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Value", _baseValue: 0),
                };
            }
        }

        #endregion

        #endregion
    }
}
