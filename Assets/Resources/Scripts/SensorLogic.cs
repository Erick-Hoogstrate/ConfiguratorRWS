/*using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class SensorLogic : PreLogicComponent
{
    bool oldValue;
    public string Adress = "INPUTS.ivar_";
    public string SensorName = "";

    public bool SensorBoolean = false;



    #region <<Signal Definitions>>
    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
                {
                    new SignalDefinition(Adress + SensorName, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Sensor Value", null, null, false)
                };
        }
    }
    #endregion

    void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
    {
        //readComponent();
    }


    private void Update()
    {
        if (oldValue != SensorBoolean)
        {
            oldValue = SensorBoolean;
            Debug.Log("Hey");
            WriteValue(Adress + SensorName, oldValue);
        }
    }


    void readComponent()
    {

    }

    // Remove default Signal Naming Rule Override list size of size 1. 
    private void OnValidate()
    {
        signalNamingRuleOverrides.Clear();
    }

}

*/









using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class SensorLogic : PreLogicComponent
{
    bool oldValue;
    public string Adress = "INPUTS.ivar_";
    public string SensorName = "";

    public bool SensorBoolean = false;

    #region <<Signal Definitions>>
    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
                {
                    new SignalDefinition(Adress + SensorName, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", "Sensor Value", null, null, false)
                };
        }
    }
    #endregion

    /*    void OnSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
        {
            if (_signal.definition.defaultSignalName == Adress + SensorName)
            {
                SensorBoolean = (bool)_newValue;
            }
            else
            {
                Debug.LogWarning("Unrecognized PLC output registered");
            }

        }*/

/*    void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
    {
        //readComponent();
    }*/


    private void Update()
    {
        if (oldValue != SensorBoolean)
        {
            oldValue = SensorBoolean;
            Debug.Log("Hey");
            WriteValue(Adress + SensorName, oldValue);
        }
    }

/*
    void readComponent()
    {

    }
*/
    // Remove default Signal Naming Rule Override list size of size 1. 
    private void OnValidate()
    {
        signalNamingRuleOverrides.Clear();
    }

}















