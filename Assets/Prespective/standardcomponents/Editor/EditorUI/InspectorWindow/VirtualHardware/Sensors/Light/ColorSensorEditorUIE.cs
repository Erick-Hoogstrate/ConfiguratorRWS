using u040.prespective.standardcomponents.virtualhardware.sensors.light;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.scenegui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.light
{
    [CustomEditor(typeof(ColorSensor))]
    public class ColorSensorEditorUIE : StandardComponentEditorUIE<ColorSensor>
    {
        #region << FIELDS >>
        //Live Data Fields 
        private TextField stateField;
        private ColorField outputSignalField;
        
        //Property Fields
        private VisualElement warningContainer;
        private Vector2Field rangeField;
        private ColorField voidColorField;
        private PropertyField onValueChangedField;
        private Toggle fixedRenderingToggle;
        
        //Control Panel Properties
        private TextField stateControlPanel;
        private ColorField outputSignalControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ColorSensorEditorLayout";
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
            outputSignalField.value = component.OutputSignal;
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            stateField = root.Q<TextField>(name: "state");
            stateField.isReadOnly = true;

            outputSignalField = root.Q<ColorField>(name: "output-signal");
            outputSignalField.showEyeDropper = false;
            outputSignalField.showAlpha = false;
            outputSignalField.Q<IMGUIContainer>().SetEnabled(false);

            #endregion
            #region << Properties >>

            warningContainer = root.Q<VisualElement>(name: "warning-container");
            IMGUIContainer warningMessage = new IMGUIContainer(OnInspectorGUI);
            warningContainer.Clear();
            warningContainer.Add(warningMessage);
            rangeField = root.Q<Vector2Field>(name: "range");
            voidColorField = root.Q<ColorField>(name: "void-color");
            voidColorField.showAlpha = false;
            onValueChangedField = root.Q<PropertyField>(name: "on-value-changed");
            fixedRenderingToggle = root.Q<Toggle>(name: "fixed-rendering");

            UIUtility.SetDisplay(warningContainer, component.FixedRendering);

            UIUtility.InitializeField
            (
                rangeField,
                component,
                () => component.Range,
                _e =>
                {
                    Undo.RecordObject(component, "Changed range");
                    component.Range = _e.newValue;
                    rangeField.SetValueWithoutNotify(component.Range);
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                voidColorField,
                component,
                () => component.VoidColor,
                _e =>
                {
                    Undo.RecordObject(component, "Changed void color");
                    component.VoidColor = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                fixedRenderingToggle,
                component,
                () => component.FixedRendering,
                _e =>
                {
                    Undo.RecordObject(component, "Changed fixed rendering");
                    component.FixedRendering = _e.newValue;
                    UIUtility.SetDisplay(warningContainer, component.FixedRendering);
                }
            );
            #endregion

            setFieldsForPlaymode(!EditorApplication.isPlaying);
            EditorApplication.playModeStateChanged += _state =>
            {
                setFieldsForPlaymode(_state != PlayModeStateChange.EnteredPlayMode);
            };
        }

        private void setFieldsForPlaymode(bool _bool) 
        {
            rangeField.SetEnabled(_bool);
            voidColorField.SetEnabled(_bool);
            onValueChangedField.SetEnabled(_bool);
            fixedRenderingToggle.SetEnabled(_bool);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Using multiple ColorSensors with FixedRendering could slow your system down drastically.", MessageType.Warning);
        }

        internal void OnSceneGUI()
        {
            //Draw Range Indicator
            Transform transform = component.SensorCamera != null ? component.SensorCamera.transform : component.transform;

            Vector3 origin = transform.position;
            Vector3 rangeStart = origin + transform.forward.normalized * component.Range.x;
            Vector3 rangeEnd = origin + transform.forward.normalized * component.Range.y;
                
            float handleSize = HandleUtility.GetHandleSize(transform.position);

            //Draw lines
            Handles.color = Color.green;
            Handles.DrawLine(rangeStart, rangeEnd);

            Handles.color = Color.red;
            Handles.DrawLine(origin, rangeStart);

            //Draw draggable dots
            Handles.color = Color.red;
            Vector3 newPosX = Handles.FreeMoveHandle(rangeStart, Quaternion.identity, 0.05f * handleSize, Vector3.zero, Handles.DotHandleCap);

            Handles.color = Color.green;
            Vector3 newPosY = Handles.FreeMoveHandle(rangeEnd, Quaternion.identity, 0.05f * handleSize, Vector3.zero, Handles.DotHandleCap);

            if (Application.isPlaying)
            {
                return;
            }

            if (newPosX != rangeStart || newPosY != rangeEnd)
            {
                Vector3 newYDotVector = component.transform.InverseTransformVector(Vector3.Project(newPosY - origin, component.transform.forward));
                Vector3 newXDotVector = component.transform.InverseTransformVector(Vector3.Project(newPosX - origin, component.transform.forward));

                float newYDot = newYDotVector.z;
                float newXDot = newXDotVector.z;

                Undo.RecordObject(component, "Changed range");
                component.Range = new Vector2(newXDot, newYDot);
                rangeField.SetValueWithoutNotify(component.Range);
            }

            //Draw scene labels
            float newX = PrespectiveHandles2D.DelayedSceneFloatField(null, component.Range.x, "m", (origin + rangeStart) * 0.5f, Vector2.zero);
            float newY = PrespectiveHandles2D.DelayedSceneFloatField(null, component.Range.y - component.Range.x, "m", (rangeStart + rangeEnd) * 0.5f, Vector2.zero);

            if (newX != component.Range.x || newY != (component.Range.y - component.Range.x))
            {
                Undo.RecordObject(component, "Changed range");
                component.Range = new Vector2(newX, newY + newX);
                rangeField.SetValueWithoutNotify(component.Range);
            }
        }


        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.SetReadOnlyState(stateControlPanel, true);

            outputSignalControlPanel = new ColorField("Output Signal");
            outputSignalControlPanel.showEyeDropper = false;
            outputSignalControlPanel.showAlpha = false;
            outputSignalControlPanel.Q<IMGUIContainer>().SetEnabled(false);

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
            outputSignalControlPanel.value = component.OutputSignal;
        }
    }
}
