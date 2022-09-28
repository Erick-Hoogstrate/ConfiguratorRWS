using System.Collections.Generic;
using System.Reflection;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches
{
    public class Gate_logic_open : PreLogicComponent
    {
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        public SlideSwitch SlideSwitch;
        public bool toggle;

        public override List<SignalDefinition> SignalDefinitions
        {
            get
            {
                return new List<SignalDefinition>()
            {
                new SignalDefinition("iGateOpen", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Value", null, null, false),
            };
            }
        }

        #region <<Update>>
        private void FixedUpdate()
        {
            readComponent();
        }
        #endregion


        void readComponent()

        {
            if (SlideSwitch.SelectedState.Id == 0)
            {
                toggle = true;
            }
            else
            {
                toggle = false;
            }
            WriteValue("iGateOpen", toggle);
        }
    }
}

