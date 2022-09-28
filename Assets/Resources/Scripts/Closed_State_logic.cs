// using System;
// using System.Collections.Generic;
// using u040.prespective.prelogic;
// using u040.prespective.prelogic.component;
// using u040.prespective.prelogic.signal;
// using UnityEngine;

// public class Closed_State_logic : PreLogicComponent
// {
// #if UNITY_EDITOR || UNITY_EDITOR_BETA
//     [HideInInspector] public int toolbarTab;
// #endif

//     [Header("Logic and I/O")]

//     [Tooltip("BooleanToggle component for the in or output")]
//     public GateState BooleanToggle;

//     [Tooltip("Input value (used for output from the PLC)")]
//     public bool oToggle;

//     [Header("Debug Values")]

//     [Tooltip("Boolean value stored in the I/O unit")]
//     public bool StoredBoolValue;

//     #region <<PLC Signals>>
//     #region <<Signal Definitions>>
//     /// <summary>
//     /// Declare the IO signals
//     /// </summary>
//     /// 

//     public override List<SignalDefinition> SignalDefinitions
//     {
//         get
//         {
//             return new List<SignalDefinition>
//             {
//                 new SignalDefinition("oToggle", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Toggle Bool", onSignalChanged, null, false)
//             };
//         }
//     }
//     #endregion
//     #region <<PLC Outputs>>
//     /// <summary>
//     /// General callback for the IOs
//     /// </summary>
//     /// <param name="_signal">the signal that has changed</param>
//     /// <param name="_newValue">the new value</param>
//     /// <param name="_newValueReceived">the time of the value change</param>
//     /// <param name="_oldValue">the old value</param>
//     /// <param name="_oldValueReceived">the time of the old value change</param>
//     void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
//     {
//         switch (_signal.definition.defaultSignalName)
//         {
//             case "oToggle":
//                 oToggle = (bool)_newValue;
//                 if (StoredBoolValue != oToggle)
//                 {
//                     StoredBoolValue = oToggle;
//                     this.BooleanToggle.Toggle(oToggle);
//                 }
//                 break;
//             default:
//                 Debug.LogWarning("Unknown Signal received:" + _signal.definition.defaultSignalName);
//                 break;
//         }
//     }
//     #endregion
// }
// #endregion


/*using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class Closed_State_logic : PreLogicComponent
{
    [Header("Logic and I/O")]
    
    [Tooltip("Input value (used for output from the PLC)")]
    public bool oToggle;

    [Header("Debug Values")]

    [Tooltip("Boolean value stored in the I/O unit")]
    public bool StoredBoolValue;

    #region <<PLC Signals>>
    #region <<Signal Definitions>>
    /// <summary>
    /// Declare the IO signals
    /// </summary>
    /// 

    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
            {
                new SignalDefinition("oToggle", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Toggle Bool", onSignalChanged, null, false)
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
            case "oToggle":
                oToggle = (bool)_newValue;
                if (StoredBoolValue != oToggle)
                {
                    StoredBoolValue = oToggle;
                }
                break;
            default:
                Debug.LogWarning("Unknown Signal received:" + _signal.definition.defaultSignalName);
                break;
        }
    }
    #endregion
}
#endregion
*/


using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class Closed_State_logic : PreLogicComponent
{
    [Header("Logic and I/O")]

    [Tooltip("Input value (used for output from the PLC)")]
    public bool oToggle;

    [Header("Debug Values")]

    [Tooltip("Boolean value stored in the I/O unit")]
    public bool StoredBoolValue;

    #region <<PLC Signals>>
    #region <<Signal Definitions>>
    /// <summary>
    /// Declare the IO signals
    /// </summary>
    /// 

    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
            {
                new SignalDefinition(transform.name, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Toggle Bool", onSignalChanged, null, false)
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
            case "oToggle":
                oToggle = (bool)_newValue;
                if (StoredBoolValue != oToggle)
                {
                    StoredBoolValue = oToggle;
                }
                break;
            default:
                Debug.LogWarning("Unknown Signal received:" + _signal.definition.defaultSignalName);
                break;
        }
    }
    #endregion
}
#endregion




