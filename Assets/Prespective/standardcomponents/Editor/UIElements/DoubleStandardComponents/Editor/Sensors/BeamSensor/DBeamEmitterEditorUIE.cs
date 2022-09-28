using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.math.doubles;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.sensors.beamsensor.editor
{
    [CustomEditor(typeof(DBeamEmitter))]
    public class DBeamEmitterEditorUIE : StandardComponentEditorUIE<DBeamEmitter>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField redirectionLimitReached;
        #endregion
        #region << Property Fields>>
        DoubleField reach;
        VisualElement originOffsetContainer;
        DVector3Field originOffset;
        VisualElement originDirectionContainer;
        DVector3Field originDirection;
        IntegerField maxNumberOfHits;
        ObjectField beamMaterial;
        DoubleField beamRadius;
        #endregion
        #region << Gizmo Properties >>
        Toggle useOriginGizmo;
        FloatField originGizmoSize;
        Toggle useBeamGizmo;
        ColorField beamColor;
        ColorField beamExcessColor;
        #endregion
        #region << Control Panel Properties >>
        TextField stateControlPanel;
        TextField reductionLimitReached;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Sensors/BeamSensor/DBeamEmitterLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            redirectionLimitReached.value = component.BeamCompleted.ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            redirectionLimitReached = root.Q<TextField>(name: "redirection-limit-reached");
            redirectionLimitReached.isReadOnly = true;
            #endregion
            #region << Properties >>
            reach = root.Q<DoubleField>(name: "reach");
            originOffsetContainer = root.Q<VisualElement>(name: "origin-offset");
            originOffset = new DVector3Field("Origin Offset", component.PositionalOffset);
            originOffsetContainer.Add(originOffset);
            originDirectionContainer = root.Q<VisualElement>(name: "origin-direction");
            originDirection = new DVector3Field("Origin Direction", component.OriginDirection);
            originDirectionContainer.Add(originDirection);
            maxNumberOfHits = root.Q<IntegerField>(name: "max-number-of-hits");
            beamMaterial = root.Q<ObjectField>(name: "beam-material");
            beamRadius = root.Q<DoubleField>(name: "beam-radius");

            UIUtility.InitializeField
            (
                reach,
                () => component.Reach,
                e =>
                {
                    component.Reach = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                originOffset,
                () => component.PositionalOffset,
                e =>
                {
                    component.PositionalOffset = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                originDirection,
                () => component.DirectionalOffset,
                e =>
                {
                    component.DirectionalOffset = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                maxNumberOfHits,
                () => component.MaxNumberOfHits,
                e =>
                {
                    component.MaxNumberOfHits = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                beamMaterial,
                () => component.BeamMaterial,
                e =>
                {
                    component.BeamMaterial = (Material)e.newValue;
                    SceneView.RepaintAll();
                },
                typeof(Material)
            );

            UIUtility.InitializeField
            (
                beamRadius,
                () => component.BeamRadius,
                e =>
                {
                    component.BeamRadius = e.newValue;
                    SceneView.RepaintAll();
                }
            );
            #endregion
            #region << Gizmo Settings >>
            useOriginGizmo = root.Q<Toggle>(name: "use-origin-gizmo");
            originGizmoSize = root.Q<FloatField>(name: "origin-gizmo-size");
            useBeamGizmo = root.Q<Toggle>(name: "use-beam-gizmo");
            beamColor = root.Q<ColorField>(name: "beam-color");
            beamExcessColor = root.Q<ColorField>(name: "beam-excess-color");

            UIUtility.InitializeField
            (
                useOriginGizmo,
                () => component.UseOriginGizmo,
                e =>
                {
                    component.UseOriginGizmo = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                originGizmoSize,
                () => component.OriginGizmoSize,
                e =>
                {
                    component.OriginGizmoSize = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                useBeamGizmo,
                () => component.UseBeamGizmo,
                e =>
                {
                    component.UseBeamGizmo = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                beamColor,
                () => component.BeamColor,
                e =>
                {
                    component.BeamColor = e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                beamExcessColor,
                () => component.BeamExcessColor,
                e =>
                {
                    component.BeamExcessColor = e.newValue;
                    SceneView.RepaintAll();
                }
            );
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.ToggleNoBoxAndReadOnly(stateControlPanel, true);

            reductionLimitReached = new TextField("Reduction Limit Reached");
            UIUtility.ToggleNoBoxAndReadOnly(reductionLimitReached, true);

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
            _container.Add(reductionLimitReached);
            _container.Add(enableDisableButton);
        }

        protected override void UpdateControlPanelData()
        {
            stateControlPanel.value = component.IsActive ? "Active" : "Inactive";
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            reductionLimitReached.value = component.BeamCompleted.ToString();
        }

        private void OnSceneGUI()
        {
            if (!Application.isPlaying)
            {
                if (component.UseOriginGizmo)
                {
                    Handles.color = component.BeamColor;
                    Handles.SphereHandleCap(0, component.OriginPosition.ToFloat(), Quaternion.identity, HandleUtility.GetHandleSize(component.transform.position) * component.OriginGizmoSize * 0.25f, EventType.Repaint);
                    Handles.ArrowHandleCap(0, component.OriginPosition.ToFloat(), DQuaternion.FromToRotation(DVector3.Forward, component.OriginDirection).ToFloat(), HandleUtility.GetHandleSize(component.transform.position) * (component.OriginGizmoSize > 0.9f && component.OriginGizmoSize < 1.1f ? 1.1f : component.OriginGizmoSize), EventType.Repaint);
                }
            }
        }
    }
}
