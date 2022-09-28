using System.Collections;
using System.Collections.Generic;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.gripper.editor
{
    public abstract class GripperFingerEditorUIE<T> : StandardComponentEditorUIE<T> where T : GripperFinger
    {
        #region << Live Data Fields >>
        Foldout detectedObjectsContainer;
        #endregion
        #region << Property Fields >>
        VisualElement fingerSettingsContainer;
        ObjectField trigger;
        Toggle generateRigidbody;
        #endregion

        string detectedObjectsTitle = "Detected Objects";

        protected override void ExecuteOnEnable()
        {
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            detectedObjectsContainer.text = detectedObjectsTitle + " (" + component.DetectedObjects.Count + ")";
            if (component.DetectedObjects.Count > 0)
            {
                detectedObjectsContainer.Clear();
                detectedObjectsContainer.Add(UIUtility.CreateObjectLocatorFields(component.DetectedObjects));
            }
        }

        protected override void Initialize()
        {
            #region << Live Data >>
            detectedObjectsContainer = root.Q<Foldout>(name: "detected-objects");
            #endregion
            #region << Properties >>
            fingerSettingsContainer = root.Q<VisualElement>(name: "finger-settings");
            trigger = root.Q<ObjectField>(name: "trigger");
            generateRigidbody = root.Q<Toggle>(name: "generate-rigidbody");
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                fingerSettingsContainer.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };

            updateListSchedule = detectedObjectsContainer.schedule.Execute(() => UpdateLiveData()).Every(250);

            UIUtility.InitializeField
            (
                trigger,
                () => component.Trigger,
                e =>
                {
                    Collider _trigger = (Collider)e.newValue;
                    component.Trigger = _trigger;
                    if (!_trigger.isTrigger)
                    {
                        trigger.SetValueWithoutNotify(null);
                    }
                },
                typeof(Collider)
            );

            UIUtility.InitializeField
            (
                generateRigidbody,
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
