using System;
using UnityEngine;
using u040.prespective.utility;
using u040.prespective.math.doubles;
using u040.prespective.prescissor.parametricshapegeneration.loft;
using u040.prespective.prepair.kinematics.joints.basic;

namespace u040.prespective.standardcomponents.virtualhardware.systems.conveyor
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ConveyorBelt : MonoBehaviour
    {
        #region<public variables>
        /// <summary>
        /// wheel joint that drives the belt
        /// </summary>
        public AWheelJoint WheelJoint;
        /// <summary>
        /// belt velocity
        /// </summary>
        public double Velocity { get; private set; } = 0d;

        /// <summary>
        /// if velocity inverted
        /// </summary>
        public bool InvertAxis = false;
        /// <summary>
        /// loft mesh of belt if present
        /// </summary>
        public DLoftMesh LoftMesh = new DLoftMesh();

        /// <summary>
        /// show velocity gizmo
        /// </summary>
        public bool EnableVelocityGizmo = true;
        /// <summary>
        /// show gizmo when not selected
        /// </summary>
        public bool ShowGizmoWhenNotSelected = true;
        /// <summary>
        /// velocity gizmo color
        /// </summary>
        public Color VelocityGizmoColor = Color.red;
        #endregion

        #region<class variables>
        /// <summary>
        /// the internal last wheel joint position
        /// </summary>
        private double storedWheelJointPosition;
        /// <summary>
        /// local direction belt
        /// </summary>
        [SerializeField]
        private DVector3 localDirection = DVector3.Forward;
        /// <summary>
        /// rigid body of the belt
        /// </summary>
        protected Rigidbody rb;
        #endregion

        #region<getters setters>
        /// <summary>
        /// local direction belt moving in
        /// </summary>
        public DVector3 LocalDirection
        {
            get
            {
                return this.localDirection;
            }
            set
            {
                this.localDirection = value.Normalized;
            }
        }
        #endregion

        #region<start>
        /// <summary>
        /// Unity start function
        /// </summary>
        public void Start()
        {
            this.storedWheelJointPosition = this.WheelJoint ? this.WheelJoint.CurrentRevolutionPercentage : 0d;
            this.rb = this.GetComponent<Rigidbody>();
            if (this.rb != null)
            {
                this.rb.isKinematic = true;
                this.rb.useGravity = false;
            }
            else
            {
                Debug.LogError("must add rigid body to belt " + PreSpectiveUtility.GetTransformPathFromTransform(this.transform));
            }
        }
        #endregion

        #region<fixed update>
        /// <summary>
        /// Unity fixed update
        /// </summary>
        public void FixedUpdate()
        {
            this.onFixedUpdate();
        }

        public virtual void onFixedUpdate()
        {
            this.determineWheeljointVelocity();
            if (this.rb != null)
            {
                Vector3 pos = this.rb.position;
                this.rb.position -= (float)this.Velocity * this.transform.TransformDirection(this.localDirection.ToFloat()).normalized * Time.fixedDeltaTime;
                this.rb.MovePosition(pos);
            }
        }

        /// <summary>
        /// Determine the velocity of the belt by calculating how much the WheelJoint has rotated and what the equivalent arc length is
        /// </summary>
        protected void determineWheeljointVelocity()
        {
            //If there is a WheelJoint and if the WheelJoint has changed rotation
            if (this.WheelJoint != null && this.WheelJoint.CurrentRevolutionPercentage != this.storedWheelJointPosition)
            {
                //Calculated how much it has changed since last tick
                double differenceInPercentage = this.WheelJoint.CurrentRevolutionPercentage - this.storedWheelJointPosition;

                //Calculated the distance the belt would move over that rotation
                double arcLength = differenceInPercentage * (2d * Math.PI * this.WheelJoint.Radius);

                //Set the velocity to match the distance over the time of a tick
                this.Velocity = arcLength / Time.fixedDeltaTime;

                //Invert the velocity is axis is inverted
                if (InvertAxis) 
                { 
                    this.Velocity *= -1d; 
                }

                //Store the current rotation for next change calculation
                this.storedWheelJointPosition = this.WheelJoint.CurrentRevolutionPercentage;
            }
            //If there is no WheelJoint or it has not changed rotation
            else
            {
                this.Velocity = 0d;
            }
        }
        #endregion
    }
}
