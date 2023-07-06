using System.Collections.Generic;
using u040.prespective.standardcomponents.virtualhardware.sensors.position;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    [CustomEditor(typeof(ProximitySensor))]
    public class ProximitySensorEditorUIE : StandardComponentEditorUIE<ProximitySensor>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField state;
        private TextField outputSignal;
        private Label detectedObjectsLabel;
        private Label noObjectsDetected;
        private VisualElement detectedObjectsContainer;
        private SerializedProperty detectedObjectsListProperty;
        
        //Property Fields
        private Toggle generateRigidBodiesToggle;
        private IMGUIContainer warningContainer;
        private PropertyField onSignalHighField;
        private PropertyField onSignalLowField;
        private ObjectField addTriggerField;
        private Label noTriggersAssignedLabel;
        private VisualElement triggersContainer;
        
        //Control Panel Properties >>
        private TextField stateControlPanel;
        private TextField outputSignalControlPanel;
        
        private string detectedObjectsText = "Detected Objects";
        private List<Collider> detectedObjectsList;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ProximitySensorEditorLayout";
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

            detectedObjectsLabel.text = detectedObjectsText + " (" + detectedObjectsListProperty.arraySize + ")";
            updateDetectedObjectsContainer();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            outputSignal = root.Q<TextField>(name: "output-signal");
            outputSignal.isReadOnly = true;

            detectedObjectsLabel = root.Q<Label>(name: "detected-objects-label");
            noObjectsDetected = root.Q<Label>(name: "no-objects-detected");
            detectedObjectsContainer = root.Q<VisualElement>(name: "detected-objects-container");

            detectedObjectsListProperty = serializedObject.FindProperty("colliderList");
            detectedObjectsList = new List<Collider>();
            #endregion
            #region << Properties >>
            generateRigidBodiesToggle = root.Q<Toggle>(name: "generate-rigidbodies");
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            onSignalHighField = root.Q<PropertyField>(name: "on-signal-high");
            onSignalLowField = root.Q<PropertyField>(name: "on-signal-low");
            addTriggerField = root.Q<ObjectField>(name: "add-trigger");
            noTriggersAssignedLabel = root.Q<Label>(name: "no-triggers-assigned");
            triggersContainer = root.Q<VisualElement>(name: "triggers-container");

            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                generateRigidBodiesToggle,
                component,
                () => component.GenerateTriggerRigidbodies,
                _e =>
                {
                    component.GenerateTriggerRigidbodies = _e.newValue;
                    generateRigidBodiesToggle.SetValueWithoutNotify(component.GenerateTriggerRigidbodies);
                }
            );

            UIUtility.InitializeField
            (
                addTriggerField,
                component,
                () => null,
                _e =>
                {
                    if (addTriggerField.value != null)
                    {
                        bool createNewFinger;
                        createNewFinger = component.AddTrigger((Collider)_e.newValue);
                        if (createNewFinger)
                        {
                            updateTriggerDisplay();
                            addTriggerSettings((Collider)addTriggerField.value);
                        }
                        addTriggerField.SetValueWithoutNotify(null);
                    }
                },
                typeof(Collider)
            );

            if (component.TriggerList.Count == 0)
            {
                UIUtility.SetDisplay(noTriggersAssignedLabel, true);
                UIUtility.SetDisplay(triggersContainer, false);
            }
            else
            {
                triggersContainer.Clear();

                for (int i = 0; i < component.TriggerList.Count; i++)
                {
                    addTriggerSettings(component.TriggerList[i]);
                }

                UIUtility.SetDisplay(noTriggersAssignedLabel, false);
                UIUtility.SetDisplay(triggersContainer, true);
            }
            #endregion

            toggleProperties(!EditorApplication.isPlaying);
            EditorApplication.playModeStateChanged += _state =>
            {
                toggleProperties(!(_state == PlayModeStateChange.EnteredPlayMode));
            };
            updateTriggerDisplay();
        }

        private void toggleProperties(bool _bool) 
        {
            generateRigidBodiesToggle.SetEnabled(_bool);
            onSignalHighField.SetEnabled(_bool);
            onSignalLowField.SetEnabled(_bool);
            addTriggerField.SetEnabled(_bool);
            triggersContainer.SetEnabled(_bool);
        }

        private int previousArraySize = 0;
        private void updateDetectedObjectsContainer()
        {
            int arraySize = detectedObjectsListProperty.arraySize;
            if (arraySize != previousArraySize) //Fix me: if in one tick content changed but array size doesn't, no refresh.
            {
                previousArraySize = arraySize;
                detectedObjectsContainer.Clear();
                if (arraySize == 0)
                {
                    UIUtility.SetDisplay(noObjectsDetected, true);
                    UIUtility.SetDisplay(detectedObjectsContainer, false);
                }
                else
                {
                    UIUtility.SetDisplay(noObjectsDetected, false);
                    UIUtility.SetDisplay(detectedObjectsContainer, true);

                    for (int i = 0; i < arraySize; i++)
                    {
                        detectedObjectsContainer.Add(UIUtility.CreateObjectLocatorField((Collider)detectedObjectsListProperty.GetArrayElementAtIndex(i).objectReferenceValue));
                    }
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Without at least one Trigger Collider assigned, this component cannot function properly.", MessageType.Error);
        }

        private void addTriggerSettings(Collider _collider)
        {
            VisualElement triggerContainer = new VisualElement();
            triggerContainer.style.flexDirection = FlexDirection.Row;

            ObjectField triggerField = new ObjectField();
            triggerField.style.flexGrow = 1;
            Button remove = new Button();
            remove.text = "X";
            remove.style.width = 20;

            triggerField.objectType = typeof(Collider);
            triggerField.Q<VisualElement>(className: "unity-object-field__selector").RemoveFromHierarchy();
            triggerField.allowSceneObjects = false;

            UIUtility.InitializeField
            (
                triggerField,
                component,
                () => _collider,
                _e =>
                {
                    triggerField.SetValueWithoutNotify(_collider);
                }, typeof(Collider)
            );

            remove.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                removeFinger(_collider, triggerContainer);
                updateTriggerDisplay();
            });

            triggerContainer.Add(triggerField);
            triggerContainer.Add(remove);

            triggersContainer.Add(triggerContainer);
        }

        private void removeFinger(Collider _trigger, VisualElement _triggercontainer)
        {
            component.RemoveTrigger(_trigger);
            _triggercontainer.parent.Remove(_triggercontainer);
        }

        private void updateTriggerDisplay()
        {
            bool isTrigger = component.TriggerList.Count == 0;
            UIUtility.SetDisplay(noTriggersAssignedLabel, isTrigger);
            UIUtility.SetDisplay(triggersContainer, !isTrigger);
            UIUtility.SetDisplay(warningContainer, isTrigger);
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
