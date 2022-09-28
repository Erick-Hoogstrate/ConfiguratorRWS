using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using u040.prespective.core;
using u040.prespective.math;
using u040.prespective.utility;
using u040.prespective.prepair.kinematics;
using u040.prespective.prescissor;

namespace u040.prespective.standardcomponents.materialhandling.beltsystem
{
    /// <summary>
    /// @CLASS : BeltSystem
    /// 
    /// @ABOUT : Creates and controls a convayer belt using loftmesh
    /// 
    /// @AUTHOR: Pieter, Tymen, Mathijs (Unit040)
    /// 
    /// @VERSION: 08/07/2019 - V1.00 - Implemented alpha
    /// @VERSION: 08/07/2019 - V1.10 - Moved the loft mesh creations to its own class which this class inherits from and implemented basic apply belt speed through force
    /// @VERSION: 29/07/2019 - V1.15 - Implemented lookup table to find clossed points
    /// @VERSION: 30/07/2019 - V1.20 - Made fixed update calculations multi threaded
    /// @VERSION: 30/07/2019 - V1.21 - Made stored spline lookup table in local space of belt
    /// @VERSION: 16/09/2019 - V1.22 - Fixed sleeping rigidbodies bug
    /// @Version: 24/01/2020 - V1.30 - Belt surface points are now being buffered  and calculates closest points from the buffer. Belt is no longer a KinematicRelation. 
    /// </summary>
    [Obsolete(DeprecationMessages.DEPRECATION_BELTSYSTEM)]
    public class BeltSystem : MonoBehaviour
    {
#pragma warning disable 0618

        #region<public variables>
        public AFWheelJoint WheelJoint;
        public float Velocity { get; private set; } = 0f;
        public bool InvertAxis = false;
        public int BufferedCircumferenceSplinePoint = 1000;
        public int BufferedSurfaceSplinePoints = 100;
        public bool ManageRotation = false;
        public LoftMesh LoftMesh = new LoftMesh();

        public bool EnableVelocityGizmo = true;
        public bool ShowGizmoWhenNotSelected = true;
        public Color VelocityGizmoColor = Color.red;
        #endregion

        #region<class variables>
        private bool idle = true;
        private List<Rigidbody> idleRigidbodies = new List<Rigidbody>();
        private BeltPointsDescription beltPointsDescription;
        private List<BeltObjectContact> beltObjectContactList = new List<BeltObjectContact>();
        private float storedWheelJointPosition;
        private float surfacePlaneAngleMargin = 1f;
        private bool pointsProperlyBuffered = true;
        #endregion

        #region<collision calculations>
        internal void OnCollisionStay(Collision _collision)
        {
            int numberOfContactPoints = _collision.contactCount;

            //Calculate the average contact point and contact normal
            Vector3 contactPointsSum = Vector3.zero;
            Vector3 contactNormalSum = Vector3.zero;

            for (int i = 0; i < numberOfContactPoints; i++)
            {
                contactPointsSum += _collision.contacts[i].point;
                contactNormalSum += _collision.contacts[i].normal;
            }
            Vector3 averageContactPoint = contactPointsSum / numberOfContactPoints;
            Vector3 averageContactNormal = contactNormalSum / numberOfContactPoints;

            //Add the contact point to the list
            this.beltObjectContactList.Add(new BeltObjectContact() { ObjectRigidbody = _collision.rigidbody, ContactPoint = averageContactPoint, ContactNormal = averageContactNormal });
        }
        #endregion

        #region<start>
        internal void Awake()
        {
            bufferSurfacePoints();
            this.storedWheelJointPosition = this.WheelJoint ? this.WheelJoint.CurrentRevolutionPercentage : 0f;
            
            //Get the collider
            MeshCollider collider = this.GetComponent<MeshCollider>();

            //If a collider is found
            if (collider)
            {
                //Check if the physics material is the default
                if (collider.material.name == "")
                {
                    //Create a new 'icy' physics material and assign it
                    collider.material = new PhysicMaterial() { dynamicFriction = 0f, staticFriction = 0f, bounciness = 0f, frictionCombine = PhysicMaterialCombine.Minimum, bounceCombine = PhysicMaterialCombine.Average, name = "Generated Belt Physics Material" };
                }
                else
                {
                    //Log that we wont override a non-default physics material
                    Debug.LogWarning("Cannot generate a PhysicsMaterial on " + this.gameObject.name + "(" + this.GetType().Name + ") since a PhysicsMaterial has already been assigned");
                }   
            }

            //If no collider is found
            else
            {
                Debug.LogError(this.gameObject.name + "(" + this.GetType().Name + ") is unable to function since it has no collider");
            }
        }



        /// <summary>
        /// Buffer all world points on the belt surface so they wont have to be calculated in runtime
        /// </summary>
        private void bufferSurfacePoints()
        {
            //Create an array for all surface splines
            BeltSurfacePointsDescription[] allSurfacePoints = new BeltSurfacePointsDescription[this.LoftMesh.SurfaceGuideSplines.Count];
            for (int i = 0; i < this.LoftMesh.SurfaceGuideSplines.Count; i++)
            {
                //Buffer Spline
                AFSpline surfaceSpline = this.LoftMesh.SurfaceGuideSplines[i];

                //Create an array for all points on a surface spline
                Vector3[] surfacePoints = new Vector3[this.BufferedSurfaceSplinePoints];
                for (int j = 0; j < this.BufferedSurfaceSplinePoints; j++)
                {
                    //Calculate the point on the spline at a certain percentage in world space and add it to the array
                    surfacePoints[j] = surfaceSpline.GetPointAtEquidistantPerc(j / (float)this.BufferedSurfaceSplinePoints);
                }

                //Add all points on the current surface spline to the array of all point on the belt
                allSurfacePoints[i] = new BeltSurfacePointsDescription() { Points = surfacePoints };
            }

            //Create an array for all circumference points
            Vector3[] circumferencePoints = new Vector3[this.LoftMesh.SurfaceGuideSplines.Count];
            for (int i = 0; i < this.LoftMesh.SurfaceGuideSplines.Count; i++)
            {
                //Add point at certain percentage of spline to array
                Vector3 circumferecePoint;
                float distance;
                Vector3 surfacePoint = allSurfacePoints[i].Points[0];
                this.LoftMesh.LoftCircumferenceSpline.GetClosestPercentageToWorldPos(surfacePoint, out distance, out circumferecePoint);
                circumferencePoints[i] = circumferecePoint;
            }

            //Create a new BeltPointDescription of the circumference points and all surface points
            this.beltPointsDescription = new BeltPointsDescription() { CircumferencePoints = circumferencePoints, SurfacePoints = allSurfacePoints };
            if (this.beltPointsDescription.CircumferencePoints.Length < 2 || this.beltPointsDescription.SurfacePoints.Length < 2)
            {
                pointsProperlyBuffered = false;
                Debug.LogError("BeltSystem on " + this.gameObject.name + " is unable to function since the spline points could not be buffered.");
            }
        }
        #endregion

        #region<fixed update>
        internal void FixedUpdate()
        {
            if (!this.pointsProperlyBuffered)
            {
                return;
            }

            //Determine the velocity on the belt by the angle difference on the wheeljoint
            determineWheeljointVelocity();

            //force wake up sleeping bodies when applying velocity after idle
            forceWakeupIdleBodies();

            //Only update objects if an actual velocity is set
            if (this.Velocity != 0f)
            {
                int numberOfBeltObjectContacts = this.beltObjectContactList.Count;
                int numberofsurfaceguides = this.LoftMesh.SurfaceGuideSplines.Count;

                BeltForceIntent[] forcesToBeApplied = new BeltForceIntent[numberOfBeltObjectContacts];
                ConcurrentDictionary<int, BeltObjectContact> contactsBag = this.beltObjectContactList.ToConcurrentDictonairy();

                Parallel.For(0, numberOfBeltObjectContacts, _i =>
                {
                    BeltObjectContact contact = contactsBag[_i];

                    //Write data to seperate memory location
                    BeltPointsDescription beltPointsDescription = this.beltPointsDescription;

                    //Find the index of the closest surface point to the contact point
                    KeyValuePair<Vector3, Vector3> closestPointIndexes = findBetweenWhichSurfacePoints(beltPointsDescription, contact);

                    //Calculate the force direction that needs to be applied on the rigidbody
                    Vector3 forceDirectionVector = (closestPointIndexes.Value - closestPointIndexes.Key).normalized;

                    forcesToBeApplied[_i] = new BeltForceIntent() { Rigidbody = contact.ObjectRigidbody, PointOfForceApplication = contact.ContactPoint, ForceDirection = forceDirectionVector };
                });


                //Add the calculated force to the rigidbodies
                for (int i = 0; i < forcesToBeApplied.Length; i++)
                {
                    //Buffer the intent
                    BeltForceIntent intent = forcesToBeApplied[i];

                    //Buffer rigidbody
                    Rigidbody rig = intent.Rigidbody;

                    //Make sure the rigidbody still exists
                    if (rig != null)
                    {
                        //Velocity in the belt direction
                        Vector3 currentDirectionalVelocity = Vector3.Project(rig.velocity, intent.ForceDirection);

                        //Target velocity
                        Vector3 targetDirectionalVelocity = intent.ForceDirection.normalized * this.Velocity;

                        //Difference in directional velocity
                        Vector3 differenceDirectionalVelocity = targetDirectionalVelocity - currentDirectionalVelocity;

                        //Force needed to accelerate to target velocity
                        Vector3 accelerationForce = rig.mass * (differenceDirectionalVelocity / Time.fixedDeltaTime);

                        //Apply force to rigidbody
                        rig.AddForce(accelerationForce);
                        //_rig.AddForceAtPosition(_accelerationForce, _intent.PointOfForceApplication, ForceMode.Force);

                        if (ManageRotation)
                        {
                            float angle = Vector3.SignedAngle(rig.velocity, intent.ForceDirection, Vector3.up);
                            Quaternion correction = Quaternion.AngleAxis(angle, Vector3.up);
                            rig.transform.rotation = correction * rig.transform.rotation;
                        }
                    }
                }
            }

            //Clear the list of contacts to recalculate each contact points next update
            this.beltObjectContactList.Clear();
        }

        /// <summary>
        /// Determine the velocity of the belt by calculating how much the WheelJoint has rotated and what the equivalent arclength is
        /// </summary>
        private void determineWheeljointVelocity()
        {
            //If there is a WheelJoint and if the WheelJoint has changed rotation
            if (this.WheelJoint && this.WheelJoint.CurrentRevolutionPercentage != this.storedWheelJointPosition)
            {
                //Calculated how much it has changed since last tick
                float differenceInPercentage = this.WheelJoint.CurrentRevolutionPercentage - this.storedWheelJointPosition;

                //Calculated the distance the belt would move over that rotation
                float arcLength = differenceInPercentage * (2f * Mathf.PI * this.WheelJoint.Radius);

                //Set the velocity to match the distance over the time of a tick
                this.Velocity = arcLength / Time.fixedDeltaTime;

                //Invert the velocity is axis is inverted
                if (InvertAxis) { this.Velocity *= -1f; }

                //Store the current rotation for next change calculation
                this.storedWheelJointPosition = this.WheelJoint.CurrentRevolutionPercentage;
            }

            //If there is no WheelJoint or it has not changed rotation
            else
            {
                this.Velocity = 0f;
            }
        }

        /// <summary>
        /// Makes sure that the rigidbodies of objects on the belt stay awake
        /// </summary>
        private void forceWakeupIdleBodies()
        {
            //finds connected rigidbodies when no velocity
            if (this.Velocity == 0f)
            {
                if (!this.idle) { this.idle = true; }

                //this.IdleRigidbodies.AddRange(this.BeltObjectContactList);
                for (int i = 0; i < this.beltObjectContactList.Count; i++)
                {
                    if (!this.idleRigidbodies.ContainsCheck<Rigidbody>(this.beltObjectContactList[i].ObjectRigidbody))
                    {
                        this.idleRigidbodies.Add(this.beltObjectContactList[i].ObjectRigidbody);
                    }
                }
            }

            //force wake up sleeping bodies when applying velocity after idle
            else if (this.idle)
            {
                for (int i = 0; i < this.idleRigidbodies.Count; i++)
                {
                    if (this.idleRigidbodies[i] != null)
                    {
                        this.idleRigidbodies[i].WakeUp();
                    }
                }
                this.idle = false;
                this.idleRigidbodies.Clear();
            }
        }

        /// <summary>
        /// Returns the two surface points between which the contact point is
        /// </summary>
        /// <param name="_beltPointsDescription"></param>
        /// <param name="_objectContactPoint"></param>
        /// <returns></returns>
        private KeyValuePair<Vector3, Vector3> findBetweenWhichSurfacePoints(BeltPointsDescription _beltPointsDescription, BeltObjectContact _objectContact)
        {
            /*
             *  First calculate which point on the circumference is closest to the contact point
             */

            //Points on the circumference spline to check
            Vector3[] circumferencePoints = _beltPointsDescription.CircumferencePoints;

            //Stored values on closest circumference point
            float leastDifferenceInDistance = -1f;
            int closestCircumferencePointIndex = 0;
            Vector3 closestCircumferencePoint = Vector3.zero;
            //Check for each circumference point the distance to the contact point
            for (int i = 0; i < circumferencePoints.Length; i++)
            {
                int currentIndex = i;
                Vector3 currentCircumferencePoint = circumferencePoints[i];

                //Calculate distance to circumference point
                float distanceFromContactToCurrentCircumferencePoint = Vector3.Distance(_objectContact.ContactPoint, currentCircumferencePoint);
                float angle = Vector3.Angle(_objectContact.ContactNormal, currentCircumferencePoint - _objectContact.ContactPoint);

                //if the calculated distance is less then previously found and lies in the plane of the collision surface, set the closest distance to the new calculated distance and store index
                if ((leastDifferenceInDistance > distanceFromContactToCurrentCircumferencePoint || leastDifferenceInDistance == -1f) && (Mathf.Abs(angle - 90f) < this.surfacePlaneAngleMargin))
                {
                    leastDifferenceInDistance = distanceFromContactToCurrentCircumferencePoint;

                    closestCircumferencePointIndex = currentIndex;
                    closestCircumferencePoint = currentCircumferencePoint;
                }
            }
                       
            /*
             *  Next, calculate which surface point in the spline belonging to the closest circumference point, is closest to the contact point.
             */

            //Take the points for the spline that belongs to the closest point on the circumference
            Vector3[] surfacePoints = beltPointsDescription.SurfacePoints[closestCircumferencePointIndex].Points;

            //Stored values on closest surface point
            int closestSurfaceIndex = 0;
            float closestDistanceFromSurfacePoint = -1f;

            //Check for each surface point the distance to the contact point
            for (int i = 0; i < surfacePoints.Length; i++)
            {
                float distanceFromContactToCurrentSurfacePoint = Vector3.Distance(_objectContact.ContactPoint, surfacePoints[i]);

                //if the calculated distance is less then previously found, set the closest distance to the new calculated distance and store index
                if (closestDistanceFromSurfacePoint > distanceFromContactToCurrentSurfacePoint || closestDistanceFromSurfacePoint == -1f)
                {
                    closestSurfaceIndex = i;
                    closestDistanceFromSurfacePoint = distanceFromContactToCurrentSurfacePoint;
                }
            }


            /*
             *  Calculate if the closest point is in front or behind the contact point
             */

            //Calculate the previous circumference point
            Vector3 previousCircumferencePoint = _beltPointsDescription.CircumferencePoints[PreSpectiveMath.LimitMinMax(closestCircumferencePointIndex - 1, 0, _beltPointsDescription.CircumferencePoints.Length - 1)];

            //If the distance from previous surface point to contact is less that previous surface point to closest point, closest point is in front of contact point. Otherwise behind contact point. 
            int indexModifier = Vector3.Distance(previousCircumferencePoint, _objectContact.ContactPoint) < Vector3.Distance(previousCircumferencePoint, closestCircumferencePoint) ? -1 : 1;

            //Take the lower index value to check the point behind the contact point
            int index = Mathf.Min(closestCircumferencePointIndex, closestCircumferencePointIndex + indexModifier);
            Vector3 surfacePointInBack = _beltPointsDescription.SurfacePoints[PreSpectiveMath.LimitMinMax(index, 0, _beltPointsDescription.CircumferencePoints.Length - 1)].Points[closestSurfaceIndex];

            //Take the higher index value to check the point in front of the contact point
            index = Mathf.Max(closestCircumferencePointIndex, closestCircumferencePointIndex + indexModifier);
            Vector3 surfacePointInFront = _beltPointsDescription.SurfacePoints[PreSpectiveMath.LimitMinMax(index, 0, _beltPointsDescription.CircumferencePoints.Length - 1)].Points[closestSurfaceIndex];

            //Return the closest surface point behind and in front of the contact point
            return new KeyValuePair<Vector3, Vector3>(surfacePointInBack, surfacePointInFront);
        }
        #endregion

        /// <summary>
        /// A buffer for all circumference and surface points in the belt
        /// </summary>
        private struct BeltPointsDescription
        {
            public Vector3[] CircumferencePoints;
            public BeltSurfacePointsDescription[] SurfacePoints;            
        }

        /// <summary>
        /// A buffer for all points on a surface spline
        /// </summary>
        private struct BeltSurfacePointsDescription
        {
            public Vector3[] Points;
        }

        /// <summary>
        /// Describes an object contact on the belt
        /// </summary>
        private struct BeltObjectContact
        {
            public Rigidbody ObjectRigidbody;
            public Vector3 ContactPoint;
            public Vector3 ContactNormal;
        }

        /// <summary>
        /// Describes an intended force to be applied to a specific rigidbody
        /// </summary>
        private struct BeltForceIntent
        {
            public Rigidbody Rigidbody;
            public Vector3 ForceDirection;
            public Vector3 PointOfForceApplication;
        }
#pragma warning restore 0618
    }
}
