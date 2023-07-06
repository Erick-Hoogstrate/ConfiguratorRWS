using System.Collections.Generic;
using UnityEngine;
using System;
using u040.prespective.prepair.kinematics;
using u040.prespective.utility.modelmanagement;
using u040.prespective.core.events;

namespace u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers
{
    public abstract class DGripperFinger : MonoBehaviour
    {
        public abstract KinematicBody KinematicBody { get; set; }

        protected List<GameObject> detectedObjects = new List<GameObject>();
        public List<GameObject> DetectedObjects
        {
            get
            {
                //Remove all null values from the list, if any
                detectedObjects.RemoveAll((_entry) => _entry == null);

                //Return the list 
                return detectedObjects;
            }
        }

        [SerializeField] private Collider trigger;
        public Collider Trigger
        {
            get { return this.trigger; }
            set
            {
                if (value != this.trigger)
                {
                    if (!value || value.isTrigger)
                    {
                        this.trigger = value;
                    }
                    else
                    {
                        Debug.LogWarning("Cannot assign " + value.name + " as trigger since it is not setup as a trigger.");
                    }
                }
            }
        }

        /// <summary>
        /// Add a Rigidbody to Trigger object upon starting playmode
        /// </summary>
        public bool GenerateRigidbody = false;

        internal void Awake()
        {
            //Make sure colliders are setup properly
            Trigger.isTrigger = true;

            //Create a local event link to pass on the OntriggerStay if the trigger is not on the same gameobject
            ALocalEventLink link = ALocalEventLink.Create(Trigger.gameObject, this);
            link.TriggerEnter = onObjectDetected;
            link.TriggerExit = onObjectLost;

            //Generate a rigidbody on the trigger object if necessary
            if (GenerateRigidbody)
            {
                Rigidbody rigidbody = Trigger.RequireComponent<Rigidbody>(false);
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }
        }

        protected virtual void onObjectDetected(Collider _collider)
        {
            //If the list does not already contain the GameObject
            if (!detectedObjects.Contains(_collider.gameObject))
            {
                //Add it to the list
                detectedObjects.Add(_collider.gameObject);
            }
        }

        protected virtual void onObjectLost(Collider _collider)
        {
            detectedObjects.Remove(_collider.gameObject);
        }

        protected void fingerJammedCallback(double _percentage)
        {
            //To prevent FPE, callback percentage is allowed to divert a tiny bit from 1f
            if (Math.Abs(_percentage - 1d) > 0.001d)
            {
                Debug.LogError(this.name + " cannot move to intended position. Moved " + (_percentage * 100d) + "% of intended move.");
            }
        }

        public abstract void SetPosition(double _percentage);
        
    }
}
