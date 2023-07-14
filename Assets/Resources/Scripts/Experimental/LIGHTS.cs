using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;
using u040.prespective.standardcomponents.userinterface.lights;
using u040.prespective.standardcomponents.virtualhardware.actuators.lights;

public static class ComponentExtensions
{
    public static void EnsureComponent<T>(this Component component, ref T output) where T : Component
    {
        if (!output && !component.TryGetComponent<T>(out output))
        {
            output = component.gameObject.AddComponent<T>();
        }
    }
}

[RequireComponent(typeof(CO_LIGHTS))]
public class LIGHTS : PreLogicComponent
{
    public CO_LIGHTS CoLights;
    private new IndicatorLight light;
    public string AdressQ = "MAIN.state0.dvar_M2_M_North_HW_EnteringTL_BNH_East_Q_";
    public string AdressI = "INPUTS.ivar_North_HW_EnteringTL_BNH_East_";
    private string ComponentName = "Red";


    bool oldValue;
    public bool ActuatorBoolean;


    private void Update()
    {
        CoLights.SensorBoolean = CoLights.Detector.OutputSignal;

        if (oldValue != CoLights.SensorBoolean)
        {
            oldValue = CoLights.SensorBoolean;
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
        light = GetComponent<CO_LIGHTS>().Light;
        if (_signal.Definition.DefaultSignalName == AdressQ)
        {
            CoLights.ActuatorBoolean = (bool)_newValue;
            CoLights.Overwrite = (bool)_newValue;
            /*            ActuatorBoolean = (bool)_newValue;
                        ActuatorBoolean = ReadValue(Adress, out _oldValue);*/
            light.SetActive((bool)_newValue);
        }
        else
        {
            Debug.LogWarning("Unrecognized PLC output registered");
        }

    }

    // Remove default Signal Naming Rule Override list size of size 1. 
    private void OnValidate()
    {
        this.EnsureComponent<CO_LIGHTS>(ref CoLights);
        SignalNamingRuleOverrides.Clear();
    }
}
