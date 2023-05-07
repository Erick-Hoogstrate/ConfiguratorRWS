using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.uistatepersistence;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.gripper.fingers
{
    public abstract class DGripperFingerEditorUIE<T> : StandardComponentEditorUIE<T> where T : DGripperFinger
    {
        #region << Live Data Fields >>
        Foldout detectedObjectsContainer;
        #endregion
        #region << Property Fields >>
        VisualElement fingerSettingsContainer;
        ObjectField triggerField;
        Toggle generateRigidbodyToggle;
        Label noDetectedObjectsLabel;
        VisualElement objectLocators;
        #endregion

        string detectedObjectsTitle = "Detected Objects";

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            detectedObjectsContainer.text = detectedObjectsTitle + " (" + component.DetectedObjects.Count + ")";
            if (component.DetectedObjects.Count > 0)
            {
                objectLocators?.Clear();
                UIUtility.SetDisplay(noDetectedObjectsLabel, false);
                objectLocators = UIUtility.CreateObjectLocatorFields(component.DetectedObjects);
                detectedObjectsContainer.Add(objectLocators);
            }
            else
            {
                objectLocators?.Clear();
                UIUtility.SetDisplay(noDetectedObjectsLabel, true);
            }
        }

        protected override void initialize()
        {
            #region << Live Data >>
            detectedObjectsContainer = root.Q<Foldout>(name: "detected-objects");
            UIStateUtility.InitTrackedFoldout(detectedObjectsContainer, component);

            noDetectedObjectsLabel = root.Q<Label>(name: "no-detected-objects");
            #endregion
            #region << Properties >>
            fingerSettingsContainer = root.Q<VisualElement>(name: "finger-settings");
            triggerField = root.Q<ObjectField>(name: "trigger");
            generateRigidbodyToggle = root.Q<Toggle>(name: "generate-rigidbody");
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                fingerSettingsContainer.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };

            updateListSchedule = detectedObjectsContainer.schedule.Execute(() => updateLiveData()).Every(250);

            UIUtility.InitializeField
            (
                triggerField,
                component,
                () => component.Trigger,
                e =>
                {
                    Collider _trigger = (Collider)e.newValue;
                    component.Trigger = _trigger;
                    if (!_trigger.isTrigger)
                    {
                        triggerField.SetValueWithoutNotify(null);
                    }
                },
                typeof(Collider)
            );

            UIUtility.InitializeField
            (
                generateRigidbodyToggle,
                component,
                () => component.GenerateRigidbody,
                e =>
                {
                    component.GenerateRigidbody = e.newValue;
                }
            );
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            Label notImplemented = new Label("No properties implemented yet.");
            notImplemented.AddToClassList("font-italic");

            _container.Add(notImplemented);
        }
    }
}
