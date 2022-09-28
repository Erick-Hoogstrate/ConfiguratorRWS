using System.Collections.Generic;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.sensors.proximitysensor.editor
{
    [CustomEditor(typeof(ProximitySensor))]
    public class ProximitySensorEditorUIE : StandardComponentEditorUIE<ProximitySensor>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField outputSignal;
        Label detectedObjectsLabel;
        Label noObjectsDetected;
        VisualElement detectedObjectsContainer;
        SerializedProperty detectedObjectsListProperty;
        #endregion
        #region << Property Fields>>
        Toggle generateRigidBodies;
        IMGUIContainer warningContainer;
        PropertyField onSignalHigh;
        PropertyField onSignalLow;
        ObjectField addTrigger;
        Label noTriggersAssigned;
        VisualElement triggersContainer;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField outputSignalControlPanel;
        #endregion
        string detectedObjectsText = "Detected Objects";
        List<Collider> detectedObjectsList;

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/ProximitySensorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            outputSignal.value = component.OutputSignal ? "High" : "Low";

            detectedObjectsLabel.text = detectedObjectsText + " (" + detectedObjectsListProperty.arraySize + ")";
            UpdateDetectedObjectsContainer();
        }

        protected override void Initialize()
        {
            base.Initialize();

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
            generateRigidBodies = root.Q<Toggle>(name: "generate-rigidbodies");
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            onSignalHigh = root.Q<PropertyField>(name: "on-signal-high");
            onSignalHigh.bindingPath = "onSignalHigh";
            onSignalLow = root.Q<PropertyField>(name: "on-signal-low");
            onSignalLow.bindingPath = "onSignalLow";
            addTrigger = root.Q<ObjectField>(name: "add-trigger");
            noTriggersAssigned = root.Q<Label>(name: "no-triggers-assigned");
            triggersContainer = root.Q<VisualElement>(name: "triggers-container");

            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                generateRigidBodies,
                () => component.GenerateTriggerRigidbodies,
                e =>
                {
                    component.GenerateTriggerRigidbodies = e.newValue;
                    generateRigidBodies.SetValueWithoutNotify(component.GenerateTriggerRigidbodies);
                }
            );

            UIUtility.InitializeField
            (
                addTrigger,
                () => null,
                e =>
                {
                    if (addTrigger.value != null)
                    {
                        bool createNewFinger;
                        createNewFinger = component.AddTrigger((Collider)e.newValue);
                        if (createNewFinger)
                        {
                            UpdateTriggerDisplay();
                            AddTriggerSettings((Collider)addTrigger.value);
                        }
                        addTrigger.SetValueWithoutNotify(null);
                    }
                },
                typeof(Collider)
            );

            if (component.TriggerList.Count == 0)
            {
                UIUtility.SetDisplay(noTriggersAssigned, true);
                UIUtility.SetDisplay(triggersContainer, false);
            }
            else
            {
                triggersContainer.Clear();

                for (int i = 0; i < component.TriggerList.Count; i++)
                {
                    AddTriggerSettings(component.TriggerList[i]);
                }

                UIUtility.SetDisplay(noTriggersAssigned, false);
                UIUtility.SetDisplay(triggersContainer, true);
            }
            #endregion

            ToggleProperties(!EditorApplication.isPlaying);
            EditorApplication.playModeStateChanged += state =>
            {
                ToggleProperties(!(state == PlayModeStateChange.EnteredPlayMode));
            };
            UpdateTriggerDisplay();
        }

        private void ToggleProperties(bool _bool) 
        {
            generateRigidBodies.SetEnabled(_bool);
            onSignalHigh.SetEnabled(_bool);
            onSignalLow.SetEnabled(_bool);
            addTrigger.SetEnabled(_bool);
            triggersContainer.SetEnabled(_bool);
        }

        private int previousArraySize = 0;
        private void UpdateDetectedObjectsContainer()
        {
            int arraySize = detectedObjectsListProperty.arraySize;
            if (arraySize != previousArraySize) //Fix me: if in one tick content changed but array size doenst, no refresh.
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

        private void AddTriggerSettings(Collider _collider)
        {
            VisualElement triggerContainer = new VisualElement();
            triggerContainer.style.flexDirection = FlexDirection.Row;

            ObjectField trigger = new ObjectField();
            trigger.style.flexGrow = 1;
            Button remove = new Button();
            remove.text = "X";
            remove.style.width = 20;

            trigger.objectType = typeof(Collider);
            trigger.Q<VisualElement>(className: "unity-object-field__selector").RemoveFromHierarchy();
            trigger.allowSceneObjects = false;

            UIUtility.InitializeField
            (
                trigger,
                () => _collider,
                e =>
                {
                    trigger.SetValueWithoutNotify(_collider);
                }
            );

            remove.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                RemoveFinger(_collider, triggerContainer);
                UpdateTriggerDisplay();
            });

            triggerContainer.Add(trigger);
            triggerContainer.Add(remove);

            triggersContainer.Add(triggerContainer);
        }

        private void RemoveFinger(Collider _trigger, VisualElement _triggercontainer)
        {
            component.RemoveTrigger(_trigger);
            _triggercontainer.parent.Remove(_triggercontainer);
        }

        private void UpdateTriggerDisplay()
        {
            bool isTrigger = component.TriggerList.Count == 0;
            UIUtility.SetDisplay(noTriggersAssigned, isTrigger);
            UIUtility.SetDisplay(triggersContainer, !isTrigger);
            UIUtility.SetDisplay(warningContainer, isTrigger);
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
