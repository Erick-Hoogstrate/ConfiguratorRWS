using System.Reflection;
using u040.prespective.core;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.materialhandling.beltsystem.editor
{
    [CustomEditor(typeof(DBeltSystem))]
    public class DBeltSystemEditorUIE : StandardComponentEditorUIE<DBeltSystem>
    {
        #region << Belt System Property Fields>>
        ObjectField wheelJointField;
        TextField velocity;
        Toggle invertAxis;
        IntegerField circumferencePoints;
        IntegerField surfacePoints;
        #endregion
        #region << Loft Mesh Property Fields>>
        VisualElement loftMeshSettings;
        ObjectField circumferenceSpline;
        ObjectField surfaceSpline;
        DoubleField circumferenceMaxLengthDeviation;
        DoubleField circumferenceMaxAngleDeviation;
        Toggle invertSurfaceNormals;
        Toggle showAsRenderedMesh;
        Button generateSurface;
        Button clearSurface;
        Button generateLoftmesh;
        #endregion
        #region << Gizmo Settings Fields >>
        Toggle enableVelocityGizmo;
        Toggle showWhenNotSelected;
        ColorField velocityColor;
        #endregion
        #region << Values >>
        private const float directionGizmoArcAngle = 60f;
        private const float directionGizmoArcRadius = 1.5f;
        private const float directioGizmoConeSize = 0.25f;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("MaterialHandling/DBeltSystemLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Belt System Properties >>
            wheelJointField = root.Q<ObjectField>(name: "wheel-joint");
            velocity = root.Q<TextField>(name: "velocity");
            velocity.isReadOnly = true;
            invertAxis = root.Q<Toggle>(name: "invert-axis");
            circumferencePoints = root.Q<IntegerField>(name: "circumference-points");
            surfacePoints = root.Q<IntegerField>(name: "surface-points");

            UIUtility.InitializeField
            (
                wheelJointField,
                () => component.WheelJoint,
                e =>
                {
                    component.WheelJoint = (AWheelJoint)e.newValue;
                },
                typeof(AWheelJoint)
            );

            velocity.value = component.Velocity.ToString();

            UIUtility.InitializeField
            (
                invertAxis,
                () => component.InvertAxis,
                e =>
                {
                    component.InvertAxis = e.newValue;
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
                () => component.LocalDirection,
                _e =>
                {
                    component.LocalDirection = _e.newValue;
                }
            );

            EditorApplication.playModeStateChanged += state =>
            {
                invertAxis.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };

            //UIUtility.InitializeField
            //(
            //    circumferencePoints,
            //    () => component.BufferedCircumferenceSplinePoint,
            //    e =>
            //    {
            //        component.BufferedCircumferenceSplinePoint = e.newValue;
            //    }
            //);

            //UIUtility.InitializeField
            //(
            //    surfacePoints,
            //    () => component.BufferedSurfaceSplinePoints,
            //    e =>
            //    {
            //        component.BufferedSurfaceSplinePoints = e.newValue;
            //    }
            //);
            #endregion
            #region << Loft Mesh Properties >>
            loftMeshSettings = root.Q<VisualElement>(name: "loft-mesh-settings");
            circumferenceSpline = root.Q<ObjectField>(name: "circumference-spline");
            surfaceSpline = root.Q<ObjectField>(name: "surface-spline");
            circumferenceMaxLengthDeviation = root.Q<DoubleField>(name: "circumference-max-length-deviation");
            circumferenceMaxAngleDeviation = root.Q<DoubleField>(name: "circumference-max-angle-deviation");
            invertSurfaceNormals = root.Q<Toggle>(name: "invert-surface-normals");
            showAsRenderedMesh = root.Q<Toggle>(name: "show-as-rendered-mesh");
            generateSurface = root.Q<Button>(name: "generate-surface");
            clearSurface = root.Q<Button>(name: "clear-surface");
            generateLoftmesh = root.Q<Button>(name: "generate-loft-mesh");

            UIUtility.InitializeField
            (
                circumferenceSpline,
                () => component.LoftMesh.LoftCircumferenceSpline,
                e =>
                {
                    component.LoftMesh.LoftCircumferenceSpline = (ADSpline)e.newValue;
                },
                typeof(ADSpline)
            );

            UIUtility.InitializeField
            (
                surfaceSpline,
                () => component.LoftMesh.LoftSurfaceSpline,
                e =>
                {
                    component.LoftMesh.LoftSurfaceSpline = (ADSpline)e.newValue;
                },
                typeof(ADSpline)
            );

            UIUtility.InitializeField
            (
                circumferenceMaxLengthDeviation,
                () => component.LoftMesh.CircumferenceMaxLengthDeviation,
                e =>
                {
                    component.LoftMesh.CircumferenceMaxLengthDeviation = (float)e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                circumferenceMaxAngleDeviation,
                () => component.LoftMesh.CircumferenceMaxAngleDeviation,
                e =>
                {
                    component.LoftMesh.CircumferenceMaxAngleDeviation = (float)e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                invertSurfaceNormals,
                () => component.LoftMesh.FlipNormals,
                e =>
                {
                    component.LoftMesh.FlipNormals = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                showAsRenderedMesh,
                () => component.LoftMesh.ShowAsRenderedMesh,
                e =>
                {
                    component.LoftMesh.ShowAsRenderedMesh = e.newValue;
                }
            );

            generateSurface.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.LoftMesh.GenerateLoftMeshSurface(component.gameObject);
                UpdateMeshButtonsDisplay();
            });

            clearSurface.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.LoftMesh.ClearLoftMeshSurface();
                UpdateMeshButtonsDisplay();
            });

            generateLoftmesh.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                component.LoftMesh.GenerateLoftMesh(component.gameObject);
                UpdateMeshButtonsDisplay();
            });

            UpdateMeshButtonsDisplay();
            #endregion

            #region << Gizmo Settings Properties >>
            enableVelocityGizmo = root.Q<Toggle>(name: "enable-velocity-gizmo");
            showWhenNotSelected = root.Q<Toggle>(name: "show-when-not-selected");
            velocityColor = root.Q<ColorField>(name: "velocity-color");

            UIUtility.InitializeField
            (
                enableVelocityGizmo,
                () => component.EnableVelocityGizmo,
                e =>
                {
                    component.EnableVelocityGizmo = e.newValue;
                    UpdateGizmoElementsDisplay();
                }
            );

            UIUtility.InitializeField
            (
                showWhenNotSelected,
                () => component.ShowGizmoWhenNotSelected,
                e =>
                {
                    component.ShowGizmoWhenNotSelected = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                velocityColor,
                () => component.VelocityGizmoColor,
                e =>
                {
                    component.VelocityGizmoColor = e.newValue;
                }
            );

            UpdateGizmoElementsDisplay();
            #endregion
        }

        protected void UpdateGizmoElementsDisplay()
        {
            UIUtility.SetDisplay(showWhenNotSelected, component.EnableVelocityGizmo);
            UIUtility.SetDisplay(velocityColor, component.EnableVelocityGizmo);
        }

        protected void UpdateMeshButtonsDisplay()
        {
            UIUtility.SetDisplay(generateSurface, component.LoftMesh.SurfaceGuideSplines.Count == 0);
            UIUtility.SetDisplay(clearSurface, component.LoftMesh.SurfaceGuideSplines.Count > 0);
            UIUtility.SetDisplay(generateLoftmesh, component.LoftMesh.SurfaceGuideSplines.Count > 0);
        }

        protected override void UpdateLiveData()
        {
            velocity.value = component.Velocity.ToString();
        }

        /// <summary>
        /// Called every tick in the editor; draws the wheeljoint in the editor - this part manages specific parts when the joint is not selected (only active when 'showInSceneViewWhenNotSelected' is set)
        /// </summary>
        /// <param name="_belt">the wheeljoint this sceneview gizmo is called on</param>
        /// <param name="gizmoType"></param>
        [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        private static void RenderCustomGizmo(DBeltSystem _belt, GizmoType gizmoType)
        {
            Handles.BeginGUI();
            Handles.Label(Vector3.zero, "");

            if (_belt.EnableVelocityGizmo && (_belt.ShowGizmoWhenNotSelected || Selection.Contains(_belt.gameObject)))
            {
                if (_belt.WheelJoint)
                {
                    Handles.color = _belt.VelocityGizmoColor;

                    //Draw arc
                    Vector3 _center = _belt.WheelJoint.DTransform.Position.ToFloat();
                    Vector3 _normal = _belt.WheelJoint.GlobalAxisDirection.ToFloat();
                    Quaternion _arcRotation = Quaternion.AngleAxis((360f * (float)_belt.WheelJoint.CurrentRevolutionPercentage) + directionGizmoArcAngle * -0.5f, _belt.WheelJoint.GlobalAxisDirection.ToFloat());
                    Vector3 _from = _arcRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized;
                    float _angle = directionGizmoArcAngle;
                    float _radius = (float)_belt.WheelJoint.Radius * directionGizmoArcRadius;
                    Handles.DrawWireArc(_center, _normal, _from, _angle, _radius);

                    //Draw handle
                    float _size = (float)_belt.WheelJoint.Radius * directioGizmoConeSize;
                    float _positionAngle = (360f * (float)_belt.WheelJoint.CurrentRevolutionPercentage);
                    if (_belt.Velocity == 0f)
                    {
                        Quaternion _handleRotation = Quaternion.AngleAxis(_positionAngle, _belt.WheelJoint.GlobalAxisDirection.ToFloat());
                        Vector3 _position = _belt.WheelJoint.transform.position + _handleRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized * (float)_belt.WheelJoint.Radius * directionGizmoArcRadius;
                        Handles.SphereHandleCap(0, _position, Quaternion.identity, _size, EventType.Repaint);
                    }
                    else
                    {
                        //Determine rotation direction from velocity
                        bool _directionCW = _belt.Velocity >= 0f ? true : false;

                        //If axis is inverted, intert gizmo
                        if (_belt.InvertAxis) { _directionCW = !_directionCW; }

                        Vector3 _directionAxis = _belt.WheelJoint.GlobalAxisDirection.ToFloat() * (_directionCW ? 1f : -1f);

                        float _directionModifier = _directionCW ? 1f : -1f;
                        _positionAngle += directionGizmoArcAngle * 0.5f * _directionModifier;
                        Quaternion _coneRotation = Quaternion.AngleAxis(_positionAngle, _belt.WheelJoint.GlobalAxisDirection.ToFloat());
                        Vector3 _position = _belt.WheelJoint.transform.position + _coneRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized * (float)_belt.WheelJoint.Radius * directionGizmoArcRadius;
                        Quaternion _rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90f, _directionAxis) * (_coneRotation * _belt.WheelJoint.GlobalForwardDirection.ToFloat().normalized), _directionAxis);
                        Handles.ConeHandleCap(0, _position, _rotation, _size, EventType.Repaint);
                    }
                }
            }
            Handles.EndGUI();
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            Label notImplemented = new Label("No properties implemented yet.");
            notImplemented.AddToClassList("font-italic");

            _container.Add(notImplemented);
        }
    }
}
