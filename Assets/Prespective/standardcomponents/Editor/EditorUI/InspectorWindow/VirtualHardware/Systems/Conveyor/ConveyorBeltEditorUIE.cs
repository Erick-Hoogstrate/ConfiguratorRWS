using u040.prespective.core.spline;
using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.systems.conveyor;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.customuifields;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.systems.conveyor
{
    [CustomEditor(typeof(ConveyorBelt))]
    public class ConveyorBeltEditorUIE : StandardComponentEditorUIE<ConveyorBelt>
    {
        #region << FIELDS >>
        //Belt System Property Fields
        private ObjectField wheelJointField;
        private TextField velocityField;
        private Toggle invertAxisToggle;
        
        //Loft Mesh Property Fields
        private VisualElement loftMeshSettings;
        private ObjectField circumferenceSplineField;
        private ObjectField surfaceSplineField;
        private DoubleField circumferenceMaxLengthDeviationField;
        private DoubleField circumferenceMaxAngleDeviationField;
        private Toggle invertSurfaceNormalsToggle;
        private Toggle showAsRenderedMeshToggle;
        private Button optimalOrientationButton;
        private Button surfaceButton;
        private Button clearSurfaceButton;
        private Button loftmeshButton;
        
        //Gizmo Settings Fields
        private Toggle enableVelocityGizmoToggle;
        private Toggle showWhenNotSelectedToggle;
        private ColorField velocityColorField;
        
        //Values
        private const float DIRECTION_GIZMO_ARC_ANGLE = 60f;
        private const float DIRECTION_GIZMO_ARC_RADIUS = 1.5f;
        private const float DIRECTIO_GIZMO_CONE_SIZE = 0.25f;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "ConveyorBeltEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Belt System Properties >>
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint");
            velocityField = root.Q<TextField>(name: "velocity");
            velocityField.isReadOnly = true;
            invertAxisToggle = root.Q<Toggle>(name: "invert-axis");

            UIUtility.InitializeField
            (
                wheelJointField,
                component,
                () => component.WheelJoint,
                _e =>
                {
                    component.WheelJoint = (AWheelJoint)_e.newValue;
                },
                typeof(AWheelJoint)
            );

            velocityField.value = component.Velocity.ToString();

            UIUtility.InitializeField
            (
                invertAxisToggle,
                component,
                () => component.InvertAxis,
                _e =>
                {
                    component.InvertAxis = _e.newValue;
                }
            );

            DVector3Field manualVectorField = new DVector3Field("Local Direction", component.LocalDirection);
            root.Q<VisualElement>(name: "local-direction").Add(manualVectorField);

            manualVectorField.Query<DoubleField>().ForEach(_doubleField =>
            {
                _doubleField.Q<Label>().style.minWidth = 0;
            });

            UIUtility.InitializeField
            (
                manualVectorField,
                component,
                () => component.LocalDirection,
                _e =>
                {
                    component.LocalDirection = _e.newValue;
                }
            );

            EditorApplication.playModeStateChanged += _state =>
            {
                invertAxisToggle.SetEnabled(!(_state == PlayModeStateChange.EnteredPlayMode));
            };
            #endregion
            #region << Loft Mesh Properties >>
            loftMeshSettings = root.Q<VisualElement>(name: "loft-mesh-settings");
            circumferenceSplineField = root.Q<ObjectField>(name: "circumference-spline");
            optimalOrientationButton = root.Q<Button>(name: "optimal-orientation-button");
            surfaceSplineField = root.Q<ObjectField>(name: "surface-spline");
            circumferenceMaxLengthDeviationField = root.Q<DoubleField>(name: "circumference-max-length-deviation");
            circumferenceMaxAngleDeviationField = root.Q<DoubleField>(name: "circumference-max-angle-deviation");
            invertSurfaceNormalsToggle = root.Q<Toggle>(name: "invert-surface-normals");
            showAsRenderedMeshToggle = root.Q<Toggle>(name: "show-as-rendered-mesh");
            surfaceButton = root.Q<Button>(name: "surface-button");
            clearSurfaceButton = root.Q<Button>(name: "clear-surface-button");
            loftmeshButton = root.Q<Button>(name: "loftmesh-button");

            UIUtility.InitializeField
            (
                circumferenceSplineField,
                component,
                () => component.LoftMesh.LoftCircumferenceSpline,
                _e =>
                {
                    component.LoftMesh.LoftCircumferenceSpline = (ADSpline)_e.newValue;
                },
                typeof(ADSpline)
            );

            UIUtility.InitializeField
            (
                surfaceSplineField,
                component,
                () => component.LoftMesh.LoftSurfaceSpline,
                _e =>
                {
                    component.LoftMesh.LoftSurfaceSpline = (ADSpline)_e.newValue;
                },
                typeof(ADSpline)
            );

            this.optimalOrientationButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                int index = Undo.GetCurrentGroup();
                if (this.component.LoftMesh.LoftCircumferenceSpline != null)
                {
                    Object[] undoObjects = System.Array.ConvertAll<ADSplineControlPoint, Transform>(this.component.LoftMesh.LoftCircumferenceSpline.SplineControlPoints.ToArray(), (ADSplineControlPoint _p) => { return _p.transform; });
                    Undo.RecordObjects(undoObjects, "optimal orientation conveyor");
                }
                if (this.component.LoftMesh.LoftSurfaceSpline != null)
                {
                    Object[] undoObjects = System.Array.ConvertAll<ADSplineControlPoint, Transform>(this.component.LoftMesh.LoftSurfaceSpline.SplineControlPoints.ToArray(), (ADSplineControlPoint _p) => { return _p.transform; });
                    Undo.RecordObjects(undoObjects, "optimal orientation conveyor");
                }
                component.LoftMesh.SetOptimalSplineOrientation();
                Undo.CollapseUndoOperations(index);
            });

            UIUtility.InitializeField
            (
                circumferenceMaxLengthDeviationField,
                component,
                () => component.LoftMesh.CircumferenceMaxLengthDeviation,
                _e =>
                {
                    component.LoftMesh.CircumferenceMaxLengthDeviation = (float)_e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                circumferenceMaxAngleDeviationField,
                component,
                () => component.LoftMesh.CircumferenceMaxAngleDeviation,
                _e =>
                {
                    component.LoftMesh.CircumferenceMaxAngleDeviation = (float)_e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                invertSurfaceNormalsToggle,
                component,
                () => component.LoftMesh.FlipNormals,
                _e =>
                {
                    component.LoftMesh.FlipNormals = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                showAsRenderedMeshToggle,
                component,
                () => component.LoftMesh.ShowAsRenderedMesh,
                _e =>
                {
                    component.LoftMesh.ShowAsRenderedMesh = _e.newValue;
                }
            );

            surfaceButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.LoftMesh.GenerateLoftMeshSurface(component.gameObject);
                updateLoftMeshButtons();
            });

            clearSurfaceButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.LoftMesh.ClearLoftMeshSurface();
                updateLoftMeshButtons();
            });


            loftmeshButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.LoftMesh.GenerateLoftMesh(component.gameObject);
                updateLoftMeshButtons();
            });

            updateLoftMeshButtons();
            #endregion

            #region << Gizmo Settings Properties >>
            enableVelocityGizmoToggle = root.Q<Toggle>(name: "enable-velocity-gizmo");
            showWhenNotSelectedToggle = root.Q<Toggle>(name: "show-when-not-selected");
            velocityColorField = root.Q<ColorField>(name: "velocity-color");

            UIUtility.InitializeField
            (
                enableVelocityGizmoToggle,
                component,
                () => component.EnableVelocityGizmo,
                _e =>
                {
                    component.EnableVelocityGizmo = _e.newValue;
                    updateGizmoElementsDisplay();
                }
            );

            UIUtility.InitializeField
            (
                showWhenNotSelectedToggle,
                component,
                () => component.ShowGizmoWhenNotSelected,
                _e =>
                {
                    component.ShowGizmoWhenNotSelected = _e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                velocityColorField,
                component,
                () => component.VelocityGizmoColor,
                _e =>
                {
                    component.VelocityGizmoColor = _e.newValue;
                }
            );

            updateGizmoElementsDisplay();
            #endregion
        }

        protected void updateGizmoElementsDisplay()
        {
            UIUtility.SetDisplay(showWhenNotSelectedToggle, component.EnableVelocityGizmo);
            UIUtility.SetDisplay(velocityColorField, component.EnableVelocityGizmo);
        }

        protected void updateLoftMeshButtons()
        {
            clearSurfaceButton.SetEnabled(component.LoftMesh.SurfaceGuideSplines.Count > 0);
            loftmeshButton.SetEnabled(component.LoftMesh.SurfaceGuideSplines.Count > 0);
        }

        protected override void updateLiveData()
        {
            velocityField.value = component.Velocity.ToString();
        }

        /// <summary>
        /// Called every tick in the editor; draws the wheel joint in the editor - this part manages specific parts when the joint is not selected (only active when 'showInSceneViewWhenNotSelected' is set)
        /// </summary>
        /// <param name="_belt">the wheel joint this scene view gizmo is called on</param>
        /// <param name="_gizmoType"></param>
        [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        private static void RenderCustomGizmo(ConveyorBelt _belt, GizmoType _gizmoType)
        {
            if (_belt.EnableVelocityGizmo && (_belt.ShowGizmoWhenNotSelected || Selection.Contains(_belt.gameObject)))
            {
                if (_belt.WheelJoint)
                {
                    Handles.color = _belt.VelocityGizmoColor;

                    //Draw arc
                    Vector3 center = _belt.WheelJoint.transform.position;
                    Vector3 normal = _belt.WheelJoint.GlobalAxisDirection.ToFloat();
                    
                    Quaternion arcRotation = Quaternion.AngleAxis((360f * (float)_belt.WheelJoint.CurrentRevolutionPercentage) + DIRECTION_GIZMO_ARC_ANGLE * -0.5f, normal);
                    Vector3 from = arcRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized;
                    float angle = DIRECTION_GIZMO_ARC_ANGLE;
                    float radius = (float)_belt.WheelJoint.Radius * DIRECTION_GIZMO_ARC_RADIUS;
                    Handles.DrawWireArc(center, normal, from, angle, radius);

                    //Draw handle
                    float size = (float)_belt.WheelJoint.Radius * DIRECTIO_GIZMO_CONE_SIZE;
                    float positionAngle = (360f * (float)_belt.WheelJoint.CurrentRevolutionPercentage);
                    if (_belt.Velocity == 0f)
                    {
                        Quaternion handleRotation = Quaternion.AngleAxis(positionAngle, normal);
                        Vector3 position = _belt.WheelJoint.transform.position + handleRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized * (float)_belt.WheelJoint.Radius * DIRECTION_GIZMO_ARC_RADIUS;
                        Handles.SphereHandleCap(0, position, Quaternion.identity, size, EventType.Repaint);
                    }
                    else
                    {
                        //Determine rotation direction from velocity
                        bool _directionCW = _belt.Velocity >= 0f ? true : false;

                        //If axis is inverted, invert gizmo
                        if (_belt.InvertAxis) 
                        { 
                            _directionCW = !_directionCW; 
                        }

                        Vector3 directionAxis = normal * (_directionCW ? 1f : -1f);

                        float directionModifier = _directionCW ? 1f : -1f;
                        positionAngle += DIRECTION_GIZMO_ARC_ANGLE * 0.5f * directionModifier;
                        Quaternion coneRotation = Quaternion.AngleAxis(positionAngle, normal);
                        Vector3 position = _belt.WheelJoint.transform.position + coneRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized * (float)_belt.WheelJoint.Radius * DIRECTION_GIZMO_ARC_RADIUS;
                        Quaternion rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90f, directionAxis) * (coneRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized), directionAxis);
                        Handles.ConeHandleCap(0, position, rotation, size, EventType.Repaint);
                    }
                }
            }
        }

        /// <summary>
        /// show control panel properties
        /// </summary>
        /// <param name="_container"></param>
        public override void ShowControlPanelProperties(VisualElement _container)
        {
            Label notImplemented = new Label("No properties implemented yet.");
            notImplemented.AddToClassList("font-italic");

            _container.Add(notImplemented);
        }
    }
}
