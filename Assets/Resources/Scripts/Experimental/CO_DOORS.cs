using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;

public class CO_DOORS : MonoBehaviour
{
    [Tooltip("Boolean Value of the toggle")]
    public DLinearActuator BarLinkageSovler;

    public bool ActuatorBoolean;
    public bool Overwrite;
    private bool backup;

    public bool SensorBoolean;


    public void Toggle(bool oToggle)
    {
        ActuatorBoolean = oToggle;
    }

    /*void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            BarLinkageSovler.Target = 1f;
        }
        if (gameObject.name == "Close" && ActuatorBoolean)
        {
            BarLinkageSovler.Target = 0f;
        }
        if (gameObject.name == "Open" && ActuatorBoolean)
        {
            BarLinkageSovler.Target = 1f;
        }
        if (gameObject.name == "CloseGate" && ActuatorBoolean)
        {
            BarLinkageSovler.Target = 1f;
        }
        if (gameObject.name == "OpenGate" && ActuatorBoolean)
        {
            BarLinkageSovler.Target = 0f;
        }
    }*/

    void Start()
    {
        Invoke("Initialize",0.1f);
    }

    void Initialize()
    {
        Overwrite = ActuatorBoolean;
        backup = ActuatorBoolean;
    }


    void OnValidate()
    {
        if (Overwrite != backup || ActuatorBoolean && GetComponent<DOORS>().ComponentName == "Close")
        {
            Debug.LogWarning("Dicht");
            BarLinkageSovler.Target = 1f;

            backup = ActuatorBoolean;
        }
        else if(ActuatorBoolean && GetComponent<DOORS>().ComponentName == "Open")
        {
            Debug.LogWarning("Open");
            BarLinkageSovler.Target = 0;
        }
    }

}
