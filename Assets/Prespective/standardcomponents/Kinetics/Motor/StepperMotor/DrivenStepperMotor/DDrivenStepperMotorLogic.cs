using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.signal;
using u040.prespective.prepair.physics.kinetics.motor;
using UnityEngine;

namespace u040.prespective.standardcomponents.kinetics.motor.steppermotor
{
    public class DDrivenStepperMotorLogic : StandardLogicComponent<DDrivenStepperMotor>
    {
        protected override Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters
        {
            get
            {
                return new Dictionary<SignalInstance, Func<object>>
                {
                    { GetSignalInstanceByName("iVelocity"), () => Target.Velocity },
                    { GetSignalInstanceByName("iPositionDeg"), () => Target.Position },
                    { GetSignalInstanceByName("iPositionStep"), () => Target.PositionSteps },
                    { GetSignalInstanceByName("iError"), () => Target.Error }
                };
            }
        }

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
                    //Input Only
                    new SignalDefinition("iVelocity", PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Velocity (deg/s)", _baseValue: 0d),
                    new SignalDefinition("iPositionDeg", PLCSignalDirection.INPUT, SupportedSignalType.REAL64, _xmlNote: "Position (deg)", _baseValue: 0d),
                    new SignalDefinition("iPositionStep", PLCSignalDirection.INPUT, SupportedSignalType.INT32, _xmlNote: "Position (step)", _baseValue: 0),
                    new SignalDefinition("iError", PLCSignalDirection.INPUT, SupportedSignalType.BOOL, _xmlNote: "Error State", _baseValue: false),

                    //Outputs only
                    new SignalDefinition("oPreferredVelocity", PLCSignalDirection.OUTPUT, SupportedSignalType.REAL64, _xmlNote: "Preferred Velocity (deg/s)", _onValueChange: onSignalChanged, _baseValue: 0d),
                    new SignalDefinition("oTargetStep", PLCSignalDirection.OUTPUT, SupportedSignalType.INT32, _xmlNote: "Target (step)", _onValueChange: onSignalChanged,_baseValue: 0),
                    new SignalDefinition("oContinuous", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Continuous Rotation", _onValueChange: onSignalChanged, _baseValue: false),
                    new SignalDefinition("oDirection", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Continuous Direction (true = CW, false = CCW)", _onValueChange: onSignalChanged, _baseValue: true),
                    new SignalDefinition("oIsActive", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Is Active", _onValueChange: onSignalChanged, _baseValue: false),
                    new SignalDefinition("oResetError", PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, _xmlNote: "Reset Error", _onValueChange: onSignalChanged, _baseValue: false),
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
                case "oPreferredVelocity":
                     Target.PreferredVelocity = (double)_newValue;
                    break;

                case "oTargetStep":
                    Target.TargetStep = (int)_newValue;
                    break;

                case "oContinuous":
                    Target.Continuous = (bool)_newValue;
                    break;

                case "oDirection":
                    Target.ContinuousDirection = (bool)_newValue ? DDrivenMotor.Direction.CW : DDrivenMotor.Direction.CCW;
                    break;

                case "oIsActive":
                    Target.IsActive = (bool)_newValue;
                    break;

                case "oResetError":
                    if ((bool)_newValue)
                    {
                        Target.ResetError();
                    }
                    break;

                default:
                    Debug.LogWarning("Unrecognized PLC output registered");
                    break;
            }
        }
        #endregion
        #endregion
    }
}
