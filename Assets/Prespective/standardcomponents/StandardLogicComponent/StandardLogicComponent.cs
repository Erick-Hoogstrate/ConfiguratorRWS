using System;
using System.Collections.Generic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents
{
    public abstract class StandardLogicComponent : PreLogicComponent
    {
        public virtual Type TargetType { get; }

        public virtual Component TargetComponent { get; set; }

        protected abstract Dictionary<SignalInstance, Func<object>> customInputSignalMemberGetters { get; }
    }

    public abstract class StandardLogicComponent<T> : StandardLogicComponent where T : Component
    {
        public T Target;

        public override Type TargetType => typeof(T);

        public override Component TargetComponent
        {
            get => Target;

            set => Target = (T) value;
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

        #region << Updates >>

        /// <summary>
        /// update the simulation component
        /// </summary>
        /// <param name="_simFrame">the current frame since start</param>
        /// <param name="_deltaTime">the time since last frame</param>
        /// <param name="_totalSimRunTime">total run time of the simulation</param>
        /// <param name="_simStart">the time the simulation started</param>
        public override void OnSimulatorUpdated(int _simFrame, float _deltaTime, float _totalSimRunTime, DateTime _simStart)
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
                WriteValue(inputSignalMemberGetter.Key.definition.defaultSignalName, inputSignalMemberGetter.Value());
            }
        }

        #endregion
    }
}
