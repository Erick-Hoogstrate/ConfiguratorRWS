using System.Reflection;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.sensors.beamsensor.editor
{
    [CustomEditor(typeof(DBeamReceiver))]
    public class DBeamReceiverEditorUIE : StandardComponentEditorUIE<DBeamReceiver>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField outputSignal;
        #endregion
        #region << Property Fields>>
        PropertyField onSignalHigh;
        PropertyField onSignalLow;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField outputSignalControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/BeamSensor/DBeamReceiverLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignal.value = component.OutputSignal ? "High" : "Low";
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            outputSignal = root.Q<TextField>(name: "output-signal");
            outputSignal.isReadOnly = true;
            #endregion
            #region << Properties >>
            onSignalHigh = root.Q<PropertyField>(name: "on-signal-high");
            onSignalHigh.bindingPath = "onSignalHigh";

            onSignalLow = root.Q<PropertyField>(name: "on-signal-low");
            onSignalLow.bindingPath = "onSignalLow";
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.ToggleNoBoxAndReadOnly(stateControlPanel, true);

            outputSignalControlPanel = new TextField("Output Signal");
            UIUtility.ToggleNoBoxAndReadOnly(outputSignalControlPanel, true);

            Button enableDisableButton = new Button();
            enableDisableButton.text = component.IsActive ? "Disable" : "Enable";
            enableDisableButton.AddToClassList("content-fit-button");
            enableDisableButton.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.IsActive = !component.IsActive;
                enableDisableButton.text = component.IsActive ? "Disable" : "Enable";
            });

            ScheduleControlPanelUpdate(stateControlPanel);

            _container.Add(stateControlPanel);
            _container.Add(outputSignalControlPanel);
            _container.Add(enableDisableButton);
        }

        protected override void UpdateControlPanelData()
        {
            stateControlPanel.value = component.IsActive ? "Active" : "Inactive";
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignalControlPanel.value = component.OutputSignal ? "High" : "Low";
        }
    }
}
