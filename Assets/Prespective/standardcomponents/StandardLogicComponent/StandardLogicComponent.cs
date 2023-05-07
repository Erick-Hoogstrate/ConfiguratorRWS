using System;
using System.Collections.Generic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.logic
{
    /// <summary>
    /// <description>
    /// This class is used as the base class for StandardLogicComponent with generic type
    /// </description>
    /// <version 
    ///     ver="1.0.0" 
    ///     author="THS" 
    ///     date="210322">
    ///     - Created base class for standard logic components
    /// </version>
    /// </summary>
    public abstract class StandardLogicComponent : PreLogicComponent
    {
        public virtual Type TargetType { get; }

        public virtual Component TargetComponent { get; set; }

        /// <summary>
        /// Dictionary that holds the entries with Signal Instance linked to a function that returns the target's class field as an object value.
        /// To be overriden in the inheriting class.
        /// </summary>
        protected abstract Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters { get; }
    }

    /// <summary>
    /// <description>
    /// Base class for every Standard Logic Component
    /// </description>
    /// <version 
    ///     ver="1.0.0" 
    ///     author="THS" 
    ///     date="210322">
    ///     - Implemented as Wrapped Classes
    /// </version>
    /// </summary>
    /// <typeparam name="T">Type of the targeted Standard Component</typeparam>
    public abstract class StandardLogicComponent<T> : StandardLogicComponent where T : Component
    {
        public T Target;

        /// <summary>
        /// Type of the targeted Standard Component
        /// </summary>
        public override Type TargetType => typeof(T);

        /// <summary>
        /// Instance of the targeted Standard Component
        /// </summary>
        public override Component TargetComponent
        {
            get
            {
                return Target;
            }

            set
            {
                Target = (T)value;
            }
        }

        private Dictionary<SignalInstance, Func<object>> inputSignalMemberGettersBuffer;

        private Dictionary<SignalInstance, Func<object>> inputSignalMemberGetters
        {
            get
            {
                if (inputSignalMemberGettersBuffer == null)
                {
                    inputSignalMemberGettersBuffer = customInputSignalMemberGetters;
                }

                return inputSignalMemberGettersBuffer;
            }
        }

        private Dictionary<string, object> inputSignalBufferValues = new Dictionary<string, object>();

        internal void Awake()
        {
            int nOfSignalDefinitions = SignalDefinitions.Count;

            if (Verbose)
            {
                Debug.Log("Number of signal definitions: " + nOfSignalDefinitions);
            }

            for(int i = 0; i < nOfSignalDefinitions; i++)
            {
                SignalDefinition signalDefinition = SignalDefinitions[i];

                if(signalDefinition.PlcSignalDirection == prelogic.PLCSignalDirection.OUTPUT)
                {
                    return;
                }

                inputSignalBufferValues.Add(signalDefinition.DefaultSignalName, signalDefinition.BaseValue);
            }
        }

        #region << Updates >>

        /// <summary>
        /// update the simulation component
        /// </summary>
        /// <param name="_simFrame">the current frame since start</param>
        /// <param name="_deltaTime">the time since last frame</param>
        /// <param name="_totalSimRunTime">total run time of the simulation</param>
        /// <param name="_simStart">the time the simulation started</param>
        protected override void OnSimulatorUpdated(int _simFrame, float _deltaTime, float _totalSimRunTime, DateTime _simStart)
        {
            if (Target == null)
            {
                Debug.LogError("No " + TargetType.Name + " instance has been set. For " + GetType());
                return;
            }

            writeInputSignals();
        }

        protected void writeInputSignals()
        {
            if (inputSignalMemberGetters == null)
            {
                return;
            }

            foreach (KeyValuePair<SignalInstance, Func<object>> inputSignalMemberGetter in inputSignalMemberGetters)
            {
                SignalInstance signalInstance = inputSignalMemberGetter.Key;
                object currentTargetValue = inputSignalMemberGetter.Value();

                if(inputSignalBufferValues.TryGetValue(signalInstance.Definition.DefaultSignalName, out object bufferValue))
                {
                    if(currentTargetValue != bufferValue)
                    {
                        WriteValue(signalInstance.Definition.DefaultSignalName, currentTargetValue);
                    }
                }
            }
        }

        #endregion
    }
}
