using u040.prespective.standardcomponents.virtualhardware.sensors.light;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.light
{
    [CustomEditor(typeof(ContrastSensor))]
    public class ContrastSensorEditorUIE : StandardComponentEditorUIE<ContrastSensor>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField state;
        private TextField outputSignal;
        private TextField matchPercentageBase;
        private TextField matchPercentageBackground;

        //Property Fields
        private IMGUIContainer warningContainer;
        private ObjectField colorSensorField;
        private ColorField baseColorField;
        private ColorField backgroundColorField;
        private PropertyField onSignalHighField;
        private PropertyField onSignalLowField;

        //Control Panel Properties
        private TextField stateControlPanel;
        private TextField outputSignalControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ContrastSensorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignal.value = component.OutputSignal ? "High" : "Low";
            matchPercentageBase.value = (component.MatchFactorBase * 100f).ToString();
            matchPercentageBackground.value = (component.MatchFactorBackground * 100f).ToString();
        }

        protected override void initialize()
        {            base.initialize();

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
            colorSensorField = root.Q<ObjectField>(name: "color-sensor");
            baseColorField = root.Q<ColorField>(name: "base-color");
            backgroundColorField = root.Q<ColorField>(name: "background-color");
            onSignalHighField = root.Q<PropertyField>(name: "on-signal-high");
            onSignalLowField = root.Q<PropertyField>(name: "on-signal-low");

            warningContainer.onGUIHandler = OnInspectorGUI;
            UIUtility.SetDisplay(warningContainer, component.ColorSensor == null);

            UIUtility.InitializeField
            (
                colorSensorField,
                component,
                () => component.ColorSensor,
                _e =>
                {
                    component.ColorSensor = (ColorSensor)_e.newValue;
                    UIUtility.SetDisplay(warningContainer, component.ColorSensor == null);
                },
                typeof(ColorSensor)
            );

            UIUtility.InitializeField
            (
                baseColorField,
                component,
                () => component.BaseColor,
                _e =>
                {
                    component.BaseColor = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                backgroundColorField,
                component,
                () => component.BackgroundColor,
                _e =>
                {
                    component.BackgroundColor = _e.newValue;
                }
            );
            #endregion

            EditorApplication.playModeStateChanged += _state =>
            {
                colorSensorField.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
                baseColorField.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
                backgroundColorField.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
                onSignalHighField.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
                onSignalLowField.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Without a Color Sensor this component will not function.", MessageType.Error);
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.SetReadOnlyState(stateControlPanel, true);

            outputSignalControlPanel = new TextField("Output Signal");
            UIUtility.SetReadOnlyState(outputSignalControlPanel, true);

            Button enableDisableButton = new Button();
            enableDisableButton.text = component.IsActive ? "Disable" : "Enable";
            enableDisableButton.AddToClassList("content-fit-button");
            enableDisableButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.IsActive = !component.IsActive;
                enableDisableButton.text = component.IsActive ? "Disable" : "Enable";
            });

            scheduleControlPanelUpdate(stateControlPanel);

            _container.Add(stateControlPanel);
            _container.Add(outputSignalControlPanel);
            _container.Add(enableDisableButton);
        }

        protected override void updateControlPanelData()
        {
            stateControlPanel.value = component.IsActive ? "Active" : "Inactive";
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignalControlPanel.value = component.OutputSignal ? "High" : "Low";
        }
    }
}
