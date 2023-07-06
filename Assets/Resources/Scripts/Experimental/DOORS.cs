using System;
using System.Linq;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;

[RequireComponent(typeof(CO_DOORS))]
public class DOORS : PreLogicComponent
{
    public CO_DOORS CoDoors;
    public string AdressQ = "MAIN.state0.dvar_M2_M_North_HW_Gate_BNH_East_Q_";
    public string AdressI = "INPUTS.ivar_North_HW_Gate_BNH_East_";
    public string ComponentName = "";
    private string ComponentNameI = "Closed";


    bool oldValue;
    public bool ActuatorBoolean;

    private void Start()
    {
        this.EnsureComponent<CO_DOORS>(ref CoDoors);
    }
    private void Update()
    {
        if (oldValue != CoDoors.SensorBoolean)
        {
            oldValue = CoDoors.SensorBoolean;
            WriteValue(AdressI, oldValue);
        }
    }

    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
            {
                new SignalDefinition(AdressI, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Sensor Value", null, null, false),
                new SignalDefinition(AdressQ, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Actuator Value", onSignalChanged, null, false),
            };
        }
    }

    void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
    {
        //light = GetComponent<CO_LIGHTS>().Light;
        if (_signal.Definition.DefaultSignalName == AdressQ)
        {
            CoDoors.ActuatorBoolean = (bool)_newValue;
            CoDoors.Overwrite = (bool)_newValue;
            /*            ActuatorBoolean = (bool)_newValue;
                        ActuatorBoolean = ReadValue(Adress, out _oldValue);*/
            if ((bool)_newValue)
            {
                Debug.LogWarning("Dicht");
                GetComponent<CO_DOORS>().BarLinkageSovler.Target = 1f;
            }
            else if (!(bool)_newValue)
            {
                Debug.LogWarning("Open");
                GetComponent<CO_DOORS>().BarLinkageSovler.Target = 0;
            }
            
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
        ComponentName = this.name.Split("_").Last();
/*        if (ComponentName == "Close")
        {
            ComponentNameI = "Closed";
        }
        else
        {
            ComponentNameI = ComponentName;
        }*/
    }
}
