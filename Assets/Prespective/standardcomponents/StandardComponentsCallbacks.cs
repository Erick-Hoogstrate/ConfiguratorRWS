#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using u040.prespective.core.editor;
using u040.prespective.utility;
using u040.prespective.standardcomponents.materialhandling.gripper;
using u040.prespective.standardcomponents.kinetics.motor.dcmotor;
using u040.prespective.standardcomponents.kinetics.motor.servomotor;
using u040.prespective.standardcomponents.kinetics.motor.steppermotor;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;
using u040.prespective.standardcomponents.materialhandling.beltsystem;
using u040.prespective.standardcomponents.sensors.beamsensor;
using u040.prespective.standardcomponents.sensors.proximitysensor;
using u040.prespective.standardcomponents.userinterface.buttons.encoders;
using u040.prespective.standardcomponents.userinterface.buttons.switches;
using u040.prespective.standardcomponents.userinterface.lights;
using u040.prespective.standardcomponents.userinterface.unityui;
using u040.prespective.standardcomponents.sensors.colorsensor;
using u040.prespective.standardcomponents.belt;
using u040.prespective.prepair.inspector;

public class PrepairCallbacks : ICallbackRegister
{
    public List<CallbackEntry> GetCallBackEntries()
    {
        return new List<CallbackEntry>
        {
            new CallbackEntry("add-control-panel", () => PreSpectiveUtility.AddComponentToSelection(typeof(ControlPanelInterfaceUIE), "Control Panel")),
            new CallbackEntry("dc-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDCMotor), typeof(DDCMotor).Name)),
            new CallbackEntry("dc-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDCMotorLogic), typeof(DDCMotorLogic).Name)),
            new CallbackEntry("driven-servo-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenServoMotor), typeof(DDrivenServoMotor).Name)),
            new CallbackEntry("driven-servo-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenServoMotorLogic), typeof(DDrivenServoMotorLogic).Name)),
            new CallbackEntry("limited-servo-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLimitedServoMotor), typeof(DLimitedServoMotor).Name)),
            new CallbackEntry("limited-servo-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLimitedServoMotorLogic), typeof(DLimitedServoMotorLogic).Name)),
            new CallbackEntry("driven-servo-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenServoMotor), typeof(DDrivenServoMotor).Name)),
            new CallbackEntry("driven-servo-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenServoMotorLogic), typeof(DDrivenServoMotorLogic).Name)),
            new CallbackEntry("continuous-servo-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DContinuousServoMotor), typeof(DContinuousServoMotor).Name)),
            new CallbackEntry("continuous-servo-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DContinuousServoMotorLogic), typeof(DContinuousServoMotorLogic).Name)),
            new CallbackEntry("driven-stepper-motor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenStepperMotor), typeof(DDrivenStepperMotor).Name)),
            new CallbackEntry("driven-stepper-motor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DDrivenStepperMotorLogic), typeof(DDrivenStepperMotorLogic).Name)),
            new CallbackEntry("linear-actuator-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLinearActuator), typeof(DLinearActuator).Name)),
            new CallbackEntry("linear-actuator-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLinearActuatorLogic), typeof(DLinearActuatorLogic).Name)),
            new CallbackEntry("indicator-light-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(IndicatorLight), typeof(IndicatorLight).Name, true, PrimitiveType.Sphere)),
            new CallbackEntry("indicator-light-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(IndicatorLightLogic), typeof(IndicatorLightLogic).Name)),
            new CallbackEntry("gripper-base-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DGripperBase), typeof(DGripperBase).Name)),
            new CallbackEntry("gripper-base-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DGripperBaseLogic), typeof(DGripperBaseLogic).Name)),
            new CallbackEntry("parallel-gripper-finger-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DParallelGripperFinger), typeof(DParallelGripperFinger).Name)),
            new CallbackEntry("angular-gripper-finger-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DAngularGripperFinger), typeof(DAngularGripperFinger).Name)),
            new CallbackEntry("vacuum-gripper-finger-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DVacuumGripperFinger), typeof(DVacuumGripperFinger).Name)),
            new CallbackEntry("belt-system-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DBeltSystem), typeof(DBeltSystem).Name)),
            new CallbackEntry("belt-renderer", () => PreSpectiveUtility.AddComponentToSelection(typeof(BeltRenderer), typeof(BeltRenderer).Name)),
            new CallbackEntry("belt-roll", () => PreSpectiveUtility.AddComponentToSelection(typeof(BeltRoll), typeof(BeltRoll).Name)),
            new CallbackEntry("beam-emitter-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DBeamEmitter), typeof(DBeamEmitter).Name)),
            new CallbackEntry("beam-emitter-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DBeamEmitterLogic), typeof(DBeamEmitterLogic).Name)),
            new CallbackEntry("beam-receiver-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DBeamReceiver), typeof(DBeamReceiver).Name)),
            new CallbackEntry("beam-receiver-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DBeamReceiverLogic), typeof(DBeamReceiverLogic).Name)),
            new CallbackEntry("beam-reflector-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DPerfectBeamReflector), typeof(DPerfectBeamReflector).Name, true, PrimitiveType.Sphere)),
            new CallbackEntry("color-sensor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ColorSensor), typeof(ColorSensor).Name)),
            new CallbackEntry("color-sensor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ColorSensorLogic), typeof(ColorSensorLogic).Name)),
            new CallbackEntry("color-detector-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ColorDetector), typeof(ColorDetector).Name)),
            new CallbackEntry("color-detector-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ColorDetectorLogic), typeof(ColorDetectorLogic).Name)),
            new CallbackEntry("contrast-sensor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ContrastSensor), typeof(ContrastSensor).Name)),
            new CallbackEntry("contrast-sensor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ContrastSensorLogic), typeof(ContrastSensorLogic).Name)),
            new CallbackEntry("proximity-sensor-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ProximitySensor), typeof(ProximitySensor).Name)),
            new CallbackEntry("proximity-sensor-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(ProximitySensorLogic), typeof(ProximitySensorLogic).Name)),
            new CallbackEntry("slide-switch-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DSlideSwitch), typeof(DSlideSwitch).Name)),
            new CallbackEntry("slide-switch-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DSlideSwitchLogic), typeof(DSlideSwitchLogic).Name)),
            new CallbackEntry("rotary-switch-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DRotarySwitch), typeof(DRotarySwitch).Name)),
            new CallbackEntry("rotary-switch-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DRotarySwitchLogic), typeof(DRotarySwitchLogic).Name)),
            new CallbackEntry("linear-encoder-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLinearEncoder), typeof(LinearEncoder).Name)),
            new CallbackEntry("linear-encoder-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DLinearEncoderLogic), typeof(DLinearEncoderLogic).Name)),
            new CallbackEntry("rotary-encoder-add-physical-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DRotaryEncoder), typeof(DRotaryEncoder).Name)),
            new CallbackEntry("rotary-encoder-add-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(DRotaryEncoderLogic), typeof(DRotaryEncoderLogic).Name)),
            new CallbackEntry("add-unity-ui-logic-component", () => PreSpectiveUtility.AddComponentToSelection(typeof(UnityUILogic), typeof(UnityUILogic).Name))
        };
    }
}
#endif
