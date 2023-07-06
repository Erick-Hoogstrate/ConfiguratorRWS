using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.lights
{
    using u040.prespective.prepair.virtualhardware.actuators;
    using u040.prespective.standardcomponents.virtualhardware.actuators.lights;
    using u040.prespective.standardcomponents.virtualhardware.sensors.light;

    public class CO_LIGHTS : MonoBehaviour, IActuator
    {
        public IndicatorLight Light;
        public ColorDetector Detector;
        //public string Adress = "MAIN.state0.dvar_M2_M_North_HW_EnteringTL_BNH_East_Q_";
        //public string ActuatorName = "Red";

        public bool ActuatorBoolean;
        public bool Overwrite;
        private bool backup;

        public bool SensorBoolean;

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
            if (Overwrite != backup)
            {
                //backup = Overwrite;
                Light.SetActive(Overwrite);
                //Overwrite = backup;
                backup = ActuatorBoolean;
            }
            else
            {
                Light.SetActive(ActuatorBoolean);
            }
        }

    }
}