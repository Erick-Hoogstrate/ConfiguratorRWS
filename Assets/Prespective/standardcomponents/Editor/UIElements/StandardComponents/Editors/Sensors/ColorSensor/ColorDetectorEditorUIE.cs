using System.Reflection;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.sensors.colorsensor.editor
{
    [CustomEditor(typeof(ColorDetector))]
    public class ColorDetectorEditorUIE : StandardComponentEditorUIE<ColorDetector>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField outputSignal;
        TextField matchPercentage;
        #endregion
        #region << Property Fields>>
        IMGUIContainer warningContainer;
        ObjectField colorSensor;
        ColorField referenceColor;
        Slider thresholdSlider;
        FloatField threshold;
        PropertyField onSignalHigh;
        PropertyField onSignalLow;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField outputSignalControlPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/ColorSensor/ColorDetectorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignal.value = component.OutputSignal ? "High" : "Low" ;
            matchPercentage.value = (component.MatchFactor * 100f).ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            outputSignal = root.Q<TextField>(name: "output-signal");
            outputSignal.isReadOnly = true;

            matchPercentage = root.Q<TextField>(name: "match-percentage");
            matchPercentage.isReadOnly = true;
            #endregion
            #region << Properties >>
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            colorSensor = root.Q<ObjectField>(name: "color-sensor");
            referenceColor = root.Q<ColorField>(name: "reference-color");
            thresholdSlider = root.Q<Slider>(name: "threshold-slider");
            threshold = root.Q<FloatField>(name: "threshold");
            onSignalHigh = root.Q<PropertyField>(name: "on-signal-high");
            onSignalHigh.bindingPath = "onSignalHigh";
            onSignalLow = root.Q<PropertyField>(name: "on-signal-low");
            onSignalLow.bindingPath = "onSignalLow";

            warningContainer.onGUIHandler = OnInspectorGUI;
            UIUtility.SetDisplay(warningContainer, component.ColorSensor == null);

            UIUtility.InitializeField
            (
                colorSensor,
                () => component.ColorSensor,
                e =>
                {
                    component.ColorSensor = (ColorSensor)e.newValue;
                    UIUtility.SetDisplay(warningContainer, component.ColorSensor == null);
                },
                typeof(ColorSensor)
            );

            UIUtility.InitializeField
            (
                referenceColor,
                () => component.ReferenceColor,
                e =>
                {
                    component.ReferenceColor = e.newValue;
                }
            );

            thresholdSlider.lowValue = 0f;
            thresholdSlider.highValue = 100f;
            UIUtility.InitializeField
            (
                thresholdSlider,
                () => component.Threshold * 100f,
                e =>
                {
                    component.Threshold = e.newValue/100f;
                    threshold.SetValueWithoutNotify(component.Threshold*100f);
                }
            );

            UIUtility.InitializeField
            (
                threshold,
                () => component.Threshold * 100f,
                e =>
                {
                    component.Threshold = e.newValue / 100f;
                    thresholdSlider.SetValueWithoutNotify(component.Threshold*100f);
                }
            );
            #endregion
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Without a Color Sensor this component will not function.", MessageType.Error);
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
