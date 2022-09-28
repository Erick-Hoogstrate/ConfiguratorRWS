using System;
using System.Collections.Generic;
using System.Reflection;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.kinetics.motor.linearactuator
{
    public class DLinearActuatorLogic : PreLogicComponent
    {
#pragma warning disable 0414 
        [SerializeField] [Obfuscation] private int toolbarTab; 
#pragma warning restore 0414 


        public DLinearActuator LinearActuator;

        //Inputs
        public double iPosition = 0f;

        //Outputs
        public double oTarget = 0f;
        private readonly double TOLERANCE = Double.Epsilon;

        #region <<PLC Signals>>
        #region <<Signal Definitions>>
        /// <summary>
        /// Declare the IO signals
        /// </summary>
        public override List<SignalDefinition> SignalDefinitions
        {
            get
            {
                return new List<SignalDefinition>() {
                    ////Input Only
                    new SignalDefinition("iPosition", PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Current Position (%)", _baseValue: 0f),

                    ////Outputs only
                    new SignalDefinition("oTarget", PLCSignalDirection.OUTPUT, SupportedSignalType.REAL64, _xmlNote: "Target Position (%)", _onValueChange: onSignalChanged, _baseValue: 0f),
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
                case "oTarget":
                    oTarget = (double)_newValue;
                    LinearActuator.Target = oTarget;
                    break;

                default:
                    Debug.LogWarning("Unrecognized PLC output registered");
                    break;
            }
        }
        #endregion
        #endregion

        #region <<Update>>
        /// <summary>
        /// update the simulation component
        /// </summary>
        /// <param name="_simFrame">the current frame since start</param>
        /// <param name="_deltaTime">the time since last frame</param>
        /// <param name="_totalSimRunTime">total run time of the simulation</param>
        /// <param name="_simStart">the time the simulation started</param>
        public override void OnSimulatorUpdated(
            int _simFrame,
            float _deltaTime,
            float _totalSimRunTime,
            DateTime _simStart)
        {
            if (LinearActuator != null)
            {
                readComponent();
            }
            else
            {
                Debug.LogWarning("No LinearActuator has been assigned.");
            }
        }


        void readComponent()
        {
            if (Math.Abs(LinearActuator.Position - iPosition) > TOLERANCE)
            {
                iPosition = LinearActuator.Position;
                WriteValue("iPosition", iPosition);
            }
        }
        #endregion
    }
}
