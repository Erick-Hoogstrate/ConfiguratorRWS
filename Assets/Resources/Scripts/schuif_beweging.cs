using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace u040.prespective.standardcomponents.kinetics.motor.linearactuator
{
    public class schuif_beweging : MonoBehaviour
    {
        public  DLinearActuator LinearActuator;
        public bool ToggleOpen;
        public bool ToggleStop;
        public bool ToggleClose;

        public void Toggle(bool oToggle)
        {
            ToggleOpen = oToggle;
        }

        void Update()
        {
            if (ToggleOpen)
            {
                LinearActuator.Target = 1f;
            }
            if (ToggleStop)
            {
                LinearActuator.Target = LinearActuator.Position;
            }
            if (ToggleClose)
            {
                LinearActuator.Target = 0f;
            }
        }
    }
}
