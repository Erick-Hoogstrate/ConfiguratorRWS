using u040.prespective.standardcomponents.virtualhardware.sensors.light;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.light
{
    [CustomEditor(typeof(ColorDetector))]
    public class ColorDetectorEditorUIE : StandardComponentEditorUIE<ColorDetector>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField stateField;
        private TextField outputSignalField;
        private TextField matchPercentageField;

        //Property Fields
        private IMGUIContainer warningContainer;
        private ObjectField colorSensorField;
        private ColorField referenceColorField;
        private Slider thresholdSlider;
        private FloatField thresholdField;

        //Control Panel Properties
        private TextField stateControlPanel;
        private TextField outputSignalControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ColorDetectorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            stateField.value = component.IsActive ? "Active" : "Inactive";
            stateField.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignalField.value = component.OutputSignal ? "High" : "Low" ;
            matchPercentageField.value = (component.MatchFactor * 100f).ToString();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            stateField = root.Q<TextField>(name: "state");
            stateField.isReadOnly = true;

            outputSignalField = root.Q<TextField>(name: "output-signal");
            outputSignalField.isReadOnly = true;

            matchPercentageField = root.Q<TextField>(name: "match-percentage");
            matchPercentageField.isReadOnly = true;
            #endregion
            #region << Properties >>
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            colorSensorField = root.Q<ObjectField>(name: "color-sensor");
            referenceColorField = root.Q<ColorField>(name: "reference-color");
            thresholdSlider = root.Q<Slider>(name: "threshold-slider");
            thresholdField = root.Q<FloatField>(name: "threshold");

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
                referenceColorField,
                component,
                () => component.ReferenceColor,
                _e =>
                {
                    component.ReferenceColor = _e.newValue;
                }
            );

            thresholdSlider.lowValue = 0f;
            thresholdSlider.highValue = 100f;
            UIUtility.InitializeField
            (
                thresholdSlider,
                component,
                () => component.Threshold * 100f,
                _e =>
                {
                    component.Threshold = _e.newValue/100f;
                    thresholdField.SetValueWithoutNotify(component.Threshold*100f);
                }
            );

            UIUtility.InitializeField
            (
                thresholdField,
                component,
                () => component.Threshold * 100f,
                _e =>
                {
                    component.Threshold = _e.newValue / 100f;
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
