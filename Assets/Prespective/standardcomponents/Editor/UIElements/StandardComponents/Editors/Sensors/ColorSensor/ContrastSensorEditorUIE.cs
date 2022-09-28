using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.sensors.colorsensor.editor
{
    [CustomEditor(typeof(ContrastSensor))]
    public class ContrastSensorEditorUIE : StandardComponentEditorUIE<ContrastSensor>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField outputSignal;
        TextField matchPercentageBase;
        TextField matchPercentageBackground;
        #endregion
        #region << Property Fields>>
        IMGUIContainer warningContainer;
        ObjectField colorSensor;
        ColorField baseColor;
        ColorField backgroundColor;
        PropertyField onSignalHigh;
        PropertyField onSignalLow;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField outputSignalControlPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/ColorSensor/ContrastSensorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignal.value = component.OutputSignal ? "High" : "Low";
            matchPercentageBase.value = (component.MatchFactorBase * 100f).ToString();
            matchPercentageBackground.value = (component.MatchFactorBackground * 100f).ToString();
        }

        protected override void Initialize()
        {            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            outputSignal = root.Q<TextField>(name: "output-signal");
            outputSignal.isReadOnly = true;

            matchPercentageBase = root.Q<TextField>(name: "match-percentage-base");
            matchPercentageBase.isReadOnly = true;

            matchPercentageBackground = root.Q<TextField>(name: "match-percentage-background");
            matchPercentageBackground.isReadOnly = true;

            #endregion
            #region << Properties >>
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            colorSensor = root.Q<ObjectField>(name: "color-sensor");
            baseColor = root.Q<ColorField>(name: "base-color");
            backgroundColor = root.Q<ColorField>(name: "background-color");

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
                baseColor,
                () => component.BaseColor,
                e =>
                {
                    component.BaseColor = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                backgroundColor,
                () => component.BackgroundColor,
                e =>
                {
                    component.BackgroundColor = e.newValue;
                }
            );
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                colorSensor.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                baseColor.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                backgroundColor.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                onSignalHigh.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                onSignalLow.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };
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
