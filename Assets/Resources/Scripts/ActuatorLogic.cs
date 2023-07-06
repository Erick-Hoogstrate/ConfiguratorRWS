using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;
public class ActuatorLogic : PreLogicComponent
{
    public string Adress = "MAIN.state0.dvar_M_HWmap_";
    public string ActuatorName = "";

    public bool ActuatorBoolean;

    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
            {
                // new SignalDefinition(Adress + ActuatorName + "_output", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Actuator Value", null, null, false),
                new SignalDefinition(Adress + ActuatorName, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Actuator Value", onSignalChanged, null, false),
            };
        }
    }
    void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
    {
        if (_signal.Definition.DefaultSignalName == Adress + ActuatorName)
        {
            Debug.Log((bool)_newValue);
            Debug.Log(ActuatorBoolean);
            ActuatorBoolean = (bool)_newValue;
            Debug.Log(ActuatorBoolean);
            ActuatorBoolean = ReadValue(Adress + ActuatorName, out _oldValue);
            Debug.Log(_oldValue);
        }
        else
        {
            Debug.LogWarning("Unrecognized PLC output registered");
        }
        
    }

    // Remove default Signal Naming Rule Override list size of size 1. 
    private void OnValidate()
    {
        SignalNamingRuleOverrides.Clear();
    }
}
