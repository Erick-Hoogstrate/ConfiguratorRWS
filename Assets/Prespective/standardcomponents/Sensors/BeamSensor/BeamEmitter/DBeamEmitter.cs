using System.Collections.Generic;
using UnityEngine;
using System;
using u040.prespective.prescissor.parametricshapegeneration.linerevolve;
using System.Reflection;
using u040.prespective.prepair.virtualhardware.actuators;
using u040.prespective.prepair.virtualhardware.systems.beam;

namespace u040.prespective.standardcomponents.virtualhardware.systems.beam
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class DBeamEmitter : MonoBehaviour, IDBeamEmitter, IActuator
    {
        #region<constants>
        private const string DEFAULT_BEAM_MATERIAL_PATH = "BeamEmitter/DefaultBeamEmitterMaterial";
        private const float MIN_REACH = 0.05f;
        private const int MAX_HITS = 25;
        #endregion

        #region<properties>
        [SerializeField]  private GameObject beamMeshGameObject = null;
        private DBeamPath path = new DBeamPath();

        /// <summary>
        /// Set the Emitter to be Active/Inactive
        /// </summary>
        public bool IsActive = true;

        /// <summary>
        /// Draw the gizmo in the Scene View
        /// </summary>
        public bool ShowGizmo = true;

        /// <summary>
        /// The color for the gizmo drawn in the Scene View
        /// </summary>
        public Color GizmoColor = Color.white;

        [SerializeField] private float reach = 5f;
        /// <summary>
        /// The reach of the beam cast by the Beam Emitter
        /// </summary>
        public float Reach {
            get 
            { 
                return reach; 
            } 
            set 
            { 
                reach = Math.Max(value, MIN_REACH); 
            } 
        }

        /// <summary>
        /// A positional offset for the origin of the beam
        /// </summary>
        public Vector3 PositionalOffset = Vector3.zero;
        
        /// <summary>
        /// The direction of the beam from the Beam Emitter
        /// </summary>
        public Vector3 BeamDirection
        {
            get 
            { 
                return transform.TransformDirection(DirectionalOffset.normalized); 
            }
        }

        /// <summary>
        /// A rotational offset for the origin of the beam
        /// </summary>
        public Vector3 DirectionalOffset = Vector3.forward;

        /// <summary>
        /// The global point of origin of the beam
        /// </summary>
        public Vector3 BeamOrigin
        {
            get
            {
                return transform.TransformPoint(PositionalOffset);
            }
        }

        [SerializeField]  private bool showBeam = true;
        /// <summary>
        /// Draw the beam (mesh) in the Scene View
        /// </summary>
        public bool ShowBeam
        {
            get { return this.showBeam; }
            set
            {
                if (showBeam != value)
                {
                    showBeam = value;
                    if (showBeam && BeamMaterial == null)
                    {
                        loadDefaultBeamMaterial();
                    }

                    generateBeamMesh();
                }
            }
        }

        [SerializeField] private Material storedBeammaterial;
        /// <summary>
        /// The material used to draw the beam mesh in the Scene View
        /// </summary>
        public Material BeamMaterial
        {
            get
            {
                return this.storedBeammaterial;
            }
            set
            {
                if (storedBeammaterial != value)
                {
                    if (storedBeammaterial == null && !ShowBeam)
                    {
                        ShowBeam = true;
                    }

                    storedBeammaterial = value;

                    if (storedBeammaterial == null && ShowBeam)
                    {
                        ShowBeam = false;
                    }
                }
            }
        }

        [SerializeField]  private float beamRadius = 0.0025f;
        /// <summary>
        /// The thickness of the beam mesh in the Scene View
        /// </summary>
        public float BeamRadius
        {
            get { return beamRadius; }
            set
            {
                if (BeamRadius != value)
                {
                    beamRadius = Mathf.Max(0.0001f, value);
                    generateBeamMesh();
                }
            }
        }
        #endregion

        #region<unity functions>
        /// <summary>
        /// Unity reset
        /// </summary>
        public void Reset()
        {
            loadDefaultBeamMaterial();
        }

        /// <summary>
        /// Unity on disable
        /// </summary>
        public void OnDisable()
        {
            resetPath();
        }

        /// <summary>
        /// Unity fixed update
        /// </summary>
        public void FixedUpdate()
        {
            if (!IsActive)
            {
                resetPath();
                return;
            }

            //Make sure colliders are synchronized with transforms
            Physics.SyncTransforms();

            //If there is a change in the beam path
            if (!validatePath())
            {
                //Create the new beam
                updatePath();
            }
        }
        #endregion

        #region<path>
        private void updatePath()
        {
            //Get a list of all targets that were hit last update
            List<IDBeamTarget> previousHitTargets = path.ConvertAll(_point => _point.RedirectionObject);

            //Create a new path
            path = new DBeamPath();
            if (BeamDirection == Vector3.zero)
            {
                Debug.LogError("Unable to cast beam. Direction is 0.");
            }

            //Add emitter as a point
            path.Add(new DBeamPathRedirectionPoint(null, this.BeamOrigin, this.BeamOrigin, this.BeamDirection));

            //Calculate the new path
            path = recurseRayCast(path, Reach);

            //Get a list of all targets that are currently being hit
            List<IDBeamTarget> newTargets = path.ConvertAll(_point => _point.RedirectionObject);
            
            //Process all lost hits for targets no longer being hit
            handleLostHits(previousHitTargets, newTargets);

            //Generate the beam mesh
            this.generateBeamMesh();
        }

        private bool validatePath()
        {
            float remainingDistance = Reach;
            int pathCount = path.Count;

            if (path.Count == 0)
            {
                return false;
            }

            //If emitter moved/rotated
            if (path[0].OutPoint != this.BeamOrigin || path[0].OutDirection != this.BeamDirection)
            {
                return false;
            }

            //Recalculate path
            //Cast ray from point, does hit point match? If not, origin point has changed
            for (int i = 0; i < pathCount; i++)
            {
                bool isLastPathSection = i == pathCount - 1;

                RaycastHit hit;
                bool raycastHit = Physics.Raycast(path[i].OutPoint, path[i].OutDirection, out hit, remainingDistance);

                //If raycast hit something
                if (raycastHit)
                {
                    //If did not hit end or next as expected
                    if ((isLastPathSection && path.EndPoint != hit.point) || (!isLastPathSection && hit.point != path[i + 1].InPoint))
                    {
                        return false;
                    }

                    //If hit is as expected
                    else
                    {
                        remainingDistance -= hit.distance;
                    }
                }

                //If raycast did not hit something
                else
                {
                    //If end is not as expected
                    if (!isLastPathSection || path[i].OutPoint + path[i].OutDirection.normalized * remainingDistance != path.EndPoint)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected virtual DBeamPath recurseRayCast(DBeamPath _path, float _remainingDistance, int _recursionProtection = 0)
        {
            //Get last redirection point
            DBeamPathRedirectionPoint lastPoint = _path[_path.Count - 1];

            //Prevent from exceeding max hit count
            if (_recursionProtection >= MAX_HITS)
            {
                _path.EndPoint = lastPoint.OutPoint + (lastPoint.OutDirection.normalized * _remainingDistance); //Hit nothing
                Debug.LogWarning(this.GetType().Name + " on " + this.gameObject.name + " has exceeded its maximum allowed number of hits of " + MAX_HITS + ".");
                return path;
            }

            RaycastHit hit;
            //If the raycast hits something
            if (Physics.Raycast(lastPoint.OutPoint, lastPoint.OutDirection, out hit, (float)_remainingDistance))
            {
                IDBeamTarget beamTarget = hit.collider.transform.GetComponent<IDBeamTarget>(); //Get IBeamTarget interface component

                //A compatible beam target was hit
                if (beamTarget != null)
                {
                    //Get the data on the hit
                    DBeamPathRedirectionPoint newHit = beamTarget.ResolveHit(lastPoint.OutDirection.normalized, hit, this);

                    //Add the hitpoint to the list
                    _path.Add(newHit);

                    //Cast the remainig ray from the new hit point
                    return recurseRayCast(_path, _remainingDistance - hit.distance, ++_recursionProtection);
                }

                //Some other non beam system collider was hit
                else
                {
                    //Mark this as the endpoint for the beam path
                    _path.EndPoint = hit.point;
                }
            }

            //If raycast does not hit
            else
            {
                _path.EndPoint = lastPoint.OutPoint + (lastPoint.OutDirection.normalized * _remainingDistance); //Hit nothing
            }

            return path;
        }

        private void resetPath()
        {
            //Register hit lost for each target
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].RedirectionObject != null)
                {
                    path[i].RedirectionObject.LostHit(this);
                }
            }

            //Clear path
            path = new DBeamPath();

            //Clear beam mesh
            if (this.beamMeshGameObject != null)
            {
                this.beamMeshGameObject.GetComponent<MeshFilter>().mesh = null;
            }
        }
        #endregion

        #region<lost hit>
        private void handleLostHits(List<IDBeamTarget> _previousHits, List<IDBeamTarget> _newHits)
        {
            //For each target that was previously hit
            foreach(IDBeamTarget currentHit in _previousHits)
            {
                //Skip if null
                if (currentHit == null)
                {
                    continue; 
                }

                //If it is no longer being hit
                if (!_newHits.Contains(currentHit))
                {
                    //Register lost hit
                    currentHit.LostHit(this);
                }
            }
        }
        #endregion

        #region<beam mesh>
        private void generateBeamMesh()
        {
            if (ShowBeam && this.BeamMaterial != null && path.Count != 0)
            {
                //creates the beam gameobject if neccessary
                if (this.beamMeshGameObject == null)
                {
                    this.beamMeshGameObject = new GameObject("BeamMeshObject");
                    this.beamMeshGameObject.transform.SetParent(this.transform);
                    this.beamMeshGameObject.transform.localPosition = Vector3.zero;
                    this.beamMeshGameObject.transform.localRotation = Quaternion.identity;
                    this.beamMeshGameObject.transform.localScale = Vector3.one;
                    this.beamMeshGameObject.hideFlags = HideFlags.HideAndDontSave;

                    this.beamMeshGameObject.AddComponent<MeshFilter>();
                    this.beamMeshGameObject.AddComponent<MeshRenderer>();
                }

                List<Vector3> points = new List<Vector3>();
                points.Add(path[0].OutPoint);
                //Draw the path lines
                for (int i = 0; i < path.Count - 1; i++)
                {
                    points.Add(path[i + 1].InPoint);
                }
                //only add point if endpoint is not equal to last found point since cannot make mesh over zero length
                if (path.EndPoint != path[path.Count - 1].InPoint)
                {
                    points.Add(path.EndPoint);
                }

                this.beamMeshGameObject.GetComponent<MeshFilter>().mesh = LineMeshUtility.GenerateSmoothLineMesh(this.transform, points, (float)this.beamRadius);
                this.beamMeshGameObject.GetComponent<Renderer>().material = this.BeamMaterial;
            }
            else
            {
                if (this.beamMeshGameObject != null)
                {
                    Destroy(this.beamMeshGameObject);
                }
            }
        }

        private void loadDefaultBeamMaterial()
        {
            BeamMaterial = Resources.Load<Material>(DEFAULT_BEAM_MATERIAL_PATH);
        }
        #endregion
    }
}
