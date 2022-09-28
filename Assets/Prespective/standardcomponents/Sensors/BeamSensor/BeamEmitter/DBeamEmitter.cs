using System.Collections.Generic;
using System.Reflection;
using u040.prespective.utility;
using UnityEngine;
using u040.prespective.prepair.components.sensors;
using System;

using u040.prespective.math.doubles;
using u040.prespective.math;

namespace u040.prespective.standardcomponents.sensors.beamsensor
{
    public class DBeamEmitter : MonoBehaviour, IDBeamEmitter, ISensor
    {
        #region<<NOTES>>
        /*
         FIXME: 
         If a reflector would ever change its reflecting properties (way of reflection, or reflecting at all), but the position 
         of the rayCast hit would not change, validatePath() would not detect this change and thus would not update the ray even 
         though the ray would need to be reflecting in a different way.
         */
        #endregion

#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        /*Settings*/
        [SerializeField] private bool isActive = true;
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        [SerializeField] private double reach = 5d;
        public double Reach {
            get 
            { 
                return reach; 
            } 
            set 
            { 
                reach = Math.Abs(value); 
            } 
        }



        [SerializeField] private DVector3 positionalOffset = DVector3.Zero;
        public DVector3 PositionalOffset
        {
            get { return positionalOffset; }
            set
            {
                positionalOffset = value;
            }
        }
        public DVector3 OriginPosition
        {
            get { return transform.TransformPoint(positionalOffset.ToFloat()).ToDouble(); }
        }

        [SerializeField] private DVector3 directionalOffset = DVector3.Forward;
        public DVector3 DirectionalOffset
        {
            get { return directionalOffset; }
            set
            {
                if (directionalOffset != value)
                {
                    directionalOffset = value;
                    if (directionalOffset.Magnitude == 0d)
                    {
                        Debug.LogWarning("Origin has currently no direction. No laser will be projected.");
                    }
                }
            }
        }
        public DVector3 OriginDirection
        {
            get { return transform.TransformDirection(DirectionalOffset.Normalized.ToFloat()).ToDouble(); }
        }


        [SerializeField] private int maxNumberOfHits = 25; //FIXME: If value is changed to lower value during play mode (which u shouldn't do anyway), path doesnt get recalculated until the targets within the new path ask for an update.
        public int MaxNumberOfHits { get { return maxNumberOfHits; } set { maxNumberOfHits = Mathf.Max(1, value); } }
        /*********/

        /*Data*/
        [SerializeField] private bool beamCompleted = true;
        public bool BeamCompleted { get { return beamCompleted; } set { beamCompleted = value; } }
        [SerializeField] private DBeamPath path = null;
        public DBeamPath Path { get { return path; } set { path = value; } }
        /******/

        /*Debugging*/
        [SerializeField] private bool useBeamGizmo = true;
        public bool UseBeamGizmo { get { return useBeamGizmo; } set { useBeamGizmo = value; } }
        [SerializeField] private bool useOriginGizmo = true;
        public bool UseOriginGizmo { get { return useOriginGizmo; } set { useOriginGizmo = value; } }
        [SerializeField] private float originGizmoSize = 1f;
        public float OriginGizmoSize { get { return originGizmoSize; } set { originGizmoSize = value; } }
        [SerializeField] private Color beamColor = Color.red;
        public Color BeamColor { get { return beamColor; } set { beamColor = value; } }
        [SerializeField] private Color beamRestColor = Color.blue;
        public Color BeamExcessColor { get { return beamRestColor; } set { beamRestColor = value; } }

        [SerializeField] private Material beamMaterial;
        public Material BeamMaterial { get { return beamMaterial; } set { beamMaterial = value; } }
        [SerializeField] private double beamRadius = 0.001d;
        public double BeamRadius
        {
            get { return beamRadius; }
            set
            {
                //checks if lower then allowed radius
                if (value >= 0.00000001d) beamRadius = value;
                else beamRadius = 0.00000001d;
            }
        }

        
        /*******/
        [HideInInspector]
        [SerializeField] private GameObject beamMeshGO = null;


        private void FixedUpdate()
        {
            if (IsActive)
            {
                if (Path != null)
                {
                    //Validate the path
                    validatePath();
                }
                if (Path == null) //If there is no path, No else if because needs to be able to reset first, then create right after
                {
                    //Create empty path object
                    Path = new DBeamPath();

                    //Calculate the path
                    buildPath();
                }
                if (useBeamGizmo) drawPath();
            }
            else if (!IsActive && Path != null)
            {
                resetPath();
            }
        }

        private void OnDisable()
        {
            resetPath();
        }

        public void buildPath()
        {
            BeamCompleted = recurseRayCast(Path, Reach);

            this.generateBeamMesh();
        }

        public void resetPath(int fromPoint = 0)
        {
            for (int i = 0; i < Path.Count; i++)
            {
                if (Path[i].getRedirectionObject() != null) Path[i].getRedirectionObject().lostHit();
            }
            Path = null;
            if (this.beamMeshGO != null) { this.beamMeshGO.GetComponent<MeshFilter>().mesh = null; }
        }

        public void validatePath()
        {
            double _remainingDistance = Reach;
            int pathCount = Path.Count;

            //Recalculate path
            //Cast ray from point, does hit point match? If not, origin point has changed
            for (int i = 0; i < pathCount; i++)
            {
                if (i == 0 && (Path[0].getOutPoint() != this.OriginPosition || Path[0].getOutDirection() != this.OriginDirection))
                {
                    resetPath(i); //TODO: recalculate from path[i]
                    break;
                }
                else if (i < MaxNumberOfHits) //prevent it from detecting hits in the 'rest'ray when maxNumberOfReflections is reached
                {
                    RaycastHit hit;
                    bool raycastHit = Physics.Raycast(Path[i].getOutPoint().ToFloat(), Path[i].getOutDirection().ToFloat(), out hit, (float)_remainingDistance);

                    if (raycastHit)
                    {
                        if ((i == pathCount - 1 && Path.endPoint.ToFloat() != hit.point) || (i != pathCount - 1 && hit.point != Path[i + 1].getInPoint().ToFloat()))
                        {
                            resetPath(i); //TODO: recalculate from path[i]
                            break;
                        }
                        else
                        {
                            _remainingDistance -= hit.distance; //if all is good, retract hit.distance from remaining distance and cast next ray
                        }
                    }
                    else
                    {
                        if (i != pathCount - 1 || Path[i].getOutPoint() + Path[i].getOutDirection().Normalized * _remainingDistance != Path.endPoint)
                        {
                            resetPath(i); //TODO: recalculate from path[i]
                            break;
                        }
                    }
                }
            }
        }

        public void drawPath()
        {
            //only draws a path if no mesh beam is made
            if (this.beamMaterial == null)
            {
                //Draw the path lines
                for (int i = 0; i < Path.Count - 1; i++)
                {
                    Debug.DrawLine(Path[i].getOutPoint().ToFloat(), Path[i + 1].getInPoint().ToFloat(), BeamColor);
                }

                Debug.DrawLine(Path[Path.Count - 1].getOutPoint().ToFloat(), Path.endPoint.ToFloat(), BeamCompleted ? BeamColor : BeamExcessColor);
            }
        }

        private void generateBeamMesh()
        {
            if (this.useBeamGizmo && this.beamMaterial != null && Path.Count != 0)
            {
                //creates the beam gameobject if neccessary
                if (this.beamMeshGO == null)
                {
                    this.beamMeshGO = new GameObject("BeamMeshObject");
                    this.beamMeshGO.transform.SetParent(this.transform);
                    this.beamMeshGO.transform.localPosition = Vector3.zero;
                    this.beamMeshGO.transform.localRotation = Quaternion.identity;
                    this.beamMeshGO.transform.localScale = Vector3.one;

                    this.beamMeshGO.AddComponent<MeshFilter>();
                    this.beamMeshGO.AddComponent<MeshRenderer>();
                }

                List<Vector3> points = new List<Vector3>();
                points.Add(Path[0].getOutPoint().ToFloat());
                //Draw the path lines
                for (int i = 0; i < Path.Count - 1; i++)
                {
                    points.Add(Path[i + 1].getInPoint().ToFloat());
                }
                //only add point if endpoint is not equal to last found point since cannot make mesh over zero length
                if (Path.endPoint != Path[Path.Count - 1].getInPoint()) points.Add(Path.endPoint.ToFloat());

                this.beamMeshGO.GetComponent<MeshFilter>().mesh = LineMeshUtility.GenerateSmoothLineMesh(this.transform, points, (float)this.beamRadius);
                this.beamMeshGO.GetComponent<Renderer>().material = this.BeamMaterial;
            }
        }


        protected virtual bool recurseRayCast(DBeamPath _path, double _remainingDistance, int _recursionProtection = 0)
        {
            if (_path.Count == 0)
            {
                Path.Add(new DBeamPathRedirectionPoint(null, this.OriginPosition, this.OriginPosition, this.OriginDirection));
            }

            DBeamPathRedirectionPoint _lastPoint = _path[_path.Count - 1];

            if (_recursionProtection >= MaxNumberOfHits)
            {
                _path.endPoint = _lastPoint.getOutPoint() + (_lastPoint.getOutDirection().Normalized * _remainingDistance); //Hit nothing
                return false;
            }


            RaycastHit hit;
            if (Physics.Raycast(_lastPoint.getOutPoint().ToFloat(), _lastPoint.getOutDirection().ToFloat(), out hit, (float)_remainingDistance))
            {
                IDBeamTarget target = hit.collider.transform.GetComponent<IDBeamTarget>(); //Get IBeamTarget interface component
                if (target != null) //Hit target
                {
                    DBeamPathRedirectionPoint _newHit = target.resolveHit(_lastPoint.getOutDirection().Normalized, hit);
                    _path.Add(_newHit);
                    return recurseRayCast(_path, _remainingDistance - hit.distance, ++_recursionProtection);
                }
                else _path.endPoint = hit.point.ToDouble(); //Hit non-target
            }
            else _path.endPoint = _lastPoint.getOutPoint() + (_lastPoint.getOutDirection().Normalized * _remainingDistance); //Hit nothing

            return true;
        }
    }
}
