// using System;
// using System.Collections;
// using System.Collections.Generic;
// using u040.prespective.prelogic;
// using u040.prespective.prelogic.component;
// using u040.prespective.prelogic.signal;
// using UnityEngine;

// public class button_logic : PreLogicComponent
// {
//     public clickable_button clickable_button;
    
 


//     public override List<SignalDefinition> SignalDefinitions
//     {
//         get
//         {
//             return new List<SignalDefinition>()
//             {
//                 new SignalDefinition("iValue", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Value", null, null, false),
//             };
//         }
//     }

//     protected override void onSimulatorUpdated(int _simFrame, float _deltaTime, float _totalSimRunTime, DateTime _simStart)
//     {
//         WriteValue("iValue", clickable_button.button);
        
//     }
// }
