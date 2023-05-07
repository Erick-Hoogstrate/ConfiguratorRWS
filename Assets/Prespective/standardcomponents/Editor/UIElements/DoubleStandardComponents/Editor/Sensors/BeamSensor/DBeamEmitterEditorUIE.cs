using u040.prespective.standardcomponents.virtualhardware.systems.beam;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.scenegui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.beam
{
    [CustomEditor(typeof(DBeamEmitter))]
    public class DBeamEmitterEditorUIE : StandardComponentEditorUIE<DBeamEmitter>
    {
        #region << Live Data Fields >>
        private TextField state;
        #endregion
        #region << Property Fields>>
        private FloatField reachField;
        private Vector3Field originOffset;
        private Vector3Field originDirection;
        private ObjectField beamMaterialField;
        private FloatField beamRadiusField;
        #endregion
        #region << Gizmo Properties >>
        private Toggle showGizmoToggle;
        private Toggle showBeamToggle;
        private ColorField gizmoColorField;
        #endregion
        #region << Control Panel Properties >>
        private TextField stateControlPanel;

        protected override string visualTreeFile => "DBeamEmitterEditorLayout";
        #endregion

        protected override void updateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;
            #endregion
            #region << Properties >>
            reachField = root.Q<FloatField>(name: "reach");
            originOffset = root.Q<Vector3Field>(name: "origin-offset");
            originDirection = root.Q<Vector3Field>(name: "origin-direction");
            beamMaterialField = root.Q<ObjectField>(name: "beam-material");
            beamRadiusField = root.Q<FloatField>(name: "beam-radius");

            UIUtility.InitializeField
            (
                reachField,
                component,
                () => component.Reach,
                _e =>
                {
                    component.Reach = _e.newValue;
                    reachField.SetValueWithoutNotify(component.Reach);
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                originOffset,
                component,
                () => component.PositionalOffset,
                _e =>
                {
                    component.PositionalOffset = _e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                originDirection,
                component,
                () => component.DirectionalOffset,
                _e =>
                {
                    component.DirectionalOffset = _e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                beamMaterialField,
                component,
                () => component.BeamMaterial,
                _e =>
                {
                    component.BeamMaterial = (Material)_e.newValue;
                    showBeamToggle.SetValueWithoutNotify(component.ShowBeam);
                    SceneView.RepaintAll();
                },
                typeof(Material)
            );

            beamRadiusField.isDelayed = true;
            UIUtility.InitializeField
            (
                beamRadiusField,
                component,
                () => component.BeamRadius,
                _e =>
                {
                    component.BeamRadius = _e.newValue;
                    beamRadiusField.SetValueWithoutNotify(component.BeamRadius);
                    SceneView.RepaintAll();
                }
            );
            #endregion
            #region << Gizmo Settings >>
            showGizmoToggle = root.Q<Toggle>(name: "show-gizmo");
            showBeamToggle = root.Q<Toggle>(name: "show-beam");
            gizmoColorField = root.Q<ColorField>(name: "gizmo-color");

            UIUtility.InitializeField
            (
                showGizmoToggle,
                component,
                () => component.ShowGizmo,
                _e =>
                {
                    component.ShowGizmo = _e.newValue;
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                showBeamToggle,
                component,
                () => component.ShowBeam,
                _e =>
                {
                    component.ShowBeam = _e.newValue;
                    beamMaterialField.SetValueWithoutNotify(component.BeamMaterial);
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                gizmoColorField,
                component,
                () => component.GizmoColor,
                _e =>
                {
                    component.GizmoColor = _e.newValue;
                    SceneView.RepaintAll();
                }
            );
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            stateControlPanel = new TextField("State");
            UIUtility.SetReadOnlyState(stateControlPanel, true);

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
            _container.Add(enableDisableButton);
        }

        protected override void updateControlPanelData()
        {
            stateControlPanel.value = component.IsActive ? "Active" : "Inactive";
            stateControlPanel.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
        }

        private void OnSceneGUI()
        {
            if (!Application.isPlaying)
            {
                if (component.ShowGizmo && component.BeamDirection != Vector3.zero)
                {
                    Handles.color = component.GizmoColor;
                    float handleSize = HandleUtility.GetHandleSize(component.transform.position);
                    Handles.SphereHandleCap(0, component.BeamOrigin, Quaternion.identity, handleSize * 0.25f, EventType.Repaint);
                    Handles.ArrowHandleCap(0, component.BeamOrigin, Quaternion.FromToRotation(Vector3.forward, component.BeamDirection), handleSize * 1.5f, EventType.Repaint);
                    PrespectiveHandles2D.ThickLine(component.BeamOrigin, component.BeamOrigin + component.BeamDirection.normalized * handleSize * 1.35f, _width: 7);
                }
            }
        }
    }
}
