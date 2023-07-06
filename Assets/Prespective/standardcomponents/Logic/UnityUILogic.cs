using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

namespace u040.prespective.standardcomponents.logic
{
    public class UnityUILogic : PreLogicComponent
    {
#pragma warning disable 0414
        [SerializeField] private int toolbarTab;
#pragma warning restore 0414

        private const string INPUT_PREFIX = "i";
        private const string OUTPUT_PREFIX = "o";

        /// <summary>
        /// Print debug logs to console
        /// </summary>
        [SerializeField] private List<string> signalNames = new List<string>();
        public List<string> SignalNames
        {
            get { return signalNames; }
            set
            {
                List<string> tempNameList = new List<string>();
                List<string> tempDateList = new List<string>();
                for (int i = 0; i < value.Count; i++)
                {
                    string name = value[i];
                    name = name.Replace(" ", "_");
                    tempNameList.Add(name);
                    tempDateList.Add(DateTime.MinValue.ToLongTimeString());
                }
                signalNames = tempNameList;
                lastTriggeredTime = tempDateList;
            }
        }

        [SerializeField] private List<string> lastTriggeredTime = new List<string>();
        /// <summary>
        /// A list which holds timestamps of when individual signals were last triggered
        /// </summary>
        public List<string> LastTriggeredTime
        {
            get { return lastTriggeredTime; }
        }

        public string DefaultTimestamp { get; } = DateTime.MinValue.ToLongTimeString();

        internal void Reset()
        {
            this.ImplicitNamingRule.InstanceNameRule = "GVLs." + this.GetType().Name + "[{{INDEX_IN_PARENT}}]";
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
                List<SignalDefinition> signalDefList = new List<SignalDefinition>();

                for (int i = 0; i < SignalNames.Count; i++)
                {
                    SignalDefinition newInputSignalDef = new SignalDefinition(INPUT_PREFIX + SignalNames[i], PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", SignalNames[i] + " received", null, null, false);
                    SignalDefinition newOutputSignalDef = new SignalDefinition(OUTPUT_PREFIX + SignalNames[i], PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", SignalNames[i] + " confirmed", onSignalChanged, null, false);

                    if (!signalDefList.Contains(newInputSignalDef) && !signalDefList.Contains(newOutputSignalDef))
                    {
                        signalDefList.Add(newInputSignalDef);
                        signalDefList.Add(newOutputSignalDef);
                    }
                    else { Debug.LogError("Cannot add \"" + SignalNames[i] + "\" to the list since it already exists within it."); }
                }
                return signalDefList;
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
            if ((bool)_newValue)
            {
                string signalName = _signal.Definition.DefaultSignalName.Substring(1);
                string inputSignalname = INPUT_PREFIX + signalName;
                WriteValue(inputSignalname, false);
            }
        }
        #endregion

        /// <summary>
        /// Trigger a signal by its ID
        /// </summary>
        /// <param name="_signalID"></param>
        public void SendSignal(int _signalID)
        {
            if (_signalID >= 0 && _signalID < SignalNames.Count)
            {
                SendSignal(SignalNames[_signalID]);
            }
            else { Debug.LogError("Cannot send signal \"" + _signalID.ToString() + "\" since it is not defined."); }
        }

        /// <summary>
        /// Trigger a signal by its name
        /// </summary>
        /// <param name="_signalName"></param>
        public void SendSignal(string _signalName)
        {
            if (SignalNames.Contains(_signalName))
            {
                if (WriteValue(INPUT_PREFIX + _signalName, true))
                {
                    saveTimeStamp(_signalName);
                }
                else { Debug.LogError("Unable to set value " + _signalName); }
            }
            else { Debug.LogError("Cannot send signal \"" + _signalName + "\" since it is not defined."); }
        }

        private void saveTimeStamp(string _signalName)
        {
            lastTriggeredTime[signalNames.IndexOf(_signalName)] = DateTime.Now.ToLongTimeString();
        }
        #endregion
    }
}
