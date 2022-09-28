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
    [CustomEditor(typeof(ColorSensor))]
    public class ColorSensorEditorUIE : StandardComponentEditorUIE<ColorSensor>
    {
        #region << Live Data Fields >>
        TextField state;
        ColorField outputSignal;
        #endregion
        #region << Property Fields>>
        VisualElement warningContainer;
        Vector2Field range;
        ColorField voidColor;
        PropertyField onValueChanged;
        Toggle fixedRendering;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        ColorField outputSignalControlPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/ColorSensor/ColorSensorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;            
            outputSignal.value = component.OutputSignal;
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            outputSignal = root.Q<ColorField>(name: "output-signal");
            outputSignal.showEyeDropper = false;
            outputSignal.showAlpha = false;
            outputSignal.Q<IMGUIContainer>().SetEnabled(false);

            #endregion
            #region << Properties >>

            warningContainer = root.Q<VisualElement>(name: "warning-container");
            IMGUIContainer warningMessage = new IMGUIContainer(OnInspectorGUI);
            warningContainer.Add(warningMessage);
            range = root.Q<Vector2Field>(name: "range");
            voidColor = root.Q<ColorField>(name: "void-color");
            voidColor.showAlpha = false;
            onValueChanged = root.Q<PropertyField>(name: "on-value-changed");
            onValueChanged.bindingPath = "onValueChanged";
            fixedRendering = root.Q<Toggle>(name: "fixed-rendering");

            UIUtility.SetDisplay(warningContainer, component.FixedRendering);

            UIUtility.InitializeField
            (
                range,
                () => component.Range,
                e =>
                {
                    component.Range = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                voidColor,
                () => component.VoidColor,
                e =>
                {
                    component.VoidColor = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                fixedRendering,
                () => component.FixedRendering,
                e =>
                {
                    component.FixedRendering = e.newValue;
                    UIUtility.SetDisplay(warningContainer, component.FixedRendering);
                }
            );
            #endregion

            SetFieldsForPlaymode(!EditorApplication.isPlaying);
            EditorApplication.playModeStateChanged += state =>
            {
                SetFieldsForPlaymode(!(state == PlayModeStateChange.EnteredPlayMode));
            };
        }

        private void SetFieldsForPlaymode(bool _bool) 
        {
            range.SetEnabled(_bool);
            voidColor.SetEnabled(_bool);
            onValueChanged.SetEnabled(_bool);
            fixedRendering.SetEnabled(_bool);
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Using multiple ColorSensors with FixedRendering could slow your system down drastically.", MessageType.Warning);
        }

        private void OnSceneGUI()
        {
            if (component.Range.x < component.Range.y)
            {
                //Draw Range Indicator
                Transform _transform = component.SensorCamera != null ? component.SensorCamera.transform : component.transform;

                Handles.color = Color.red;
                Vector3 _origin = _transform.position;
                Vector3 _rangeStart = _origin + (_transform.forward * component.Range.x);
                Vector3 _rangeEnd = _origin + (_transform.forward * component.Range.y);
                Handles.DrawLine(_rangeStart, _rangeEnd);
                float _handleSize = HandleUtility.GetHandleSize(_transform.position);
                Handles.DotHandleCap(0, _rangeStart, Quaternion.identity, 0.05f * _handleSize, EventType.Repaint);
                Handles.DotHandleCap(0, _rangeEnd, Quaternion.identity, 0.05f * _handleSize, EventType.Repaint);
            }
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.ToggleNoBoxAndReadOnly(stateControlPanel, true);

            outputSignalControlPanel = new ColorField("Output Signal");
            outputSignalControlPanel.showEyeDropper = false;
            outputSignalControlPanel.showAlpha = false;
            outputSignalControlPanel.Q<IMGUIContainer>().SetEnabled(false);

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
            outputSignalControlPanel.value = component.OutputSignal;
        }
    }
}
