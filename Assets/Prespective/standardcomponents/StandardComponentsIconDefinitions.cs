#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using u040.prespective.utility.editor;
using u040.prespective.standardcomponents.materialhandling.beltsystem;
using u040.prespective.standardcomponents;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;
using u040.prespective.prepair.inspector;
using u040.prespective.standardcomponents.materialhandling.gripper;

public class StandardComponentsIconDefinitions : HierarchyWindowIconRuleAdditions
{
#pragma warning disable 0618
    [ObfuscationAttribute(Exclude = true, StripAfterObfuscation = false)]
    public override List<HierarchyWindowIconClassRules> AddedIconRules
    {
        get
        {
            return new List<HierarchyWindowIconClassRules>()
                {
                new HierarchyWindowIconClassRules(typeof(BeltSystem),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "Belt_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),

                new HierarchyWindowIconClassRules(typeof(IActuator),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "Actuator_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),

                new HierarchyWindowIconClassRules(typeof(LinearActuator),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "Actuator_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),

                new HierarchyWindowIconClassRules(typeof(ControlPanelInterface),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "ControlPanel_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),

                new HierarchyWindowIconClassRules(typeof(GripperBase),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "Gripper_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),

                new HierarchyWindowIconClassRules(typeof(ISensor),
                new Vector2Int(101, 1),
                new HierarchyWindowIconRule[]
                {
                new HierarchyWindowIconRule(
                    "Sensor_Icon_16x16",
                    new Vector2Int(20, 20),
                    new Func<UnityEngine.Object, bool>((_script)=>{
                        return true;
                        }))
                }),
            };
        }
    }
#pragma warning restore 0618
}
#endif
