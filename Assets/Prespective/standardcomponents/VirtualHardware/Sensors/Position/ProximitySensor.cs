using System.Collections.Generic;
using UnityEngine;
using u040.prespective.utility.collectionmanagement;
using System.Reflection;
using u040.prespective.prepair.virtualhardware.sensors;
using u040.prespective.core.events;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.position
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class ProximitySensor : QuantitativeSensor, ISensor
    {
        #region<properties>
        [SerializeField]  private List<Collider> colliderList = new List<Collider>();
        [SerializeField]  private List<Collider> storedTriggerList = new List<Collider>();

        /// <summary>
        /// A list of all the Game Objects that hold Trigger Colliders and form the Detection Area for the Proximity Sensor
        /// </summary>
        public List<Collider> TriggerList
        {
            get 
            { 
                return this.storedTriggerList; 
            }
            private set 
            { 
                this.storedTriggerList = value; 
            }
        }

        /// <summary>
        /// Whether to automatically generate a Rigid body on each Trigger Game Object upon entering Play mode
        /// </summary>
        public bool GenerateTriggerRigidbodies = true;
        #endregion

        #region<unity function>
        /// <summary>
        /// Unity awake
        /// </summary>
        public void Awake()
        {
            //Create trigger linkage with each collider
            for (int i = TriggerList.Count - 1; i >= 0; i--)
            {
                if (TriggerList[i] != null && TriggerList[i].isTrigger)
                {
                    if (this.GenerateTriggerRigidbodies)
                    {
                        Rigidbody rigidBody = TriggerList[i].gameObject.GetComponent<Rigidbody>();
                        if (rigidBody == null)
                        {
                            rigidBody = TriggerList[i].gameObject.AddComponent<Rigidbody>();
                        }
                        rigidBody.useGravity = false;
                        rigidBody.isKinematic = true;
                    }

                    ALocalEventLink link = ALocalEventLink.Create(TriggerList[i].gameObject, this);
                    link.TriggerEnter = addCollider;
                    link.TriggerExit = removeCollider;
                }
                else
                { 
                    TriggerList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Unity fixed update
        /// </summary>
        public void FixedUpdate()
        {
            if (colliderList.RemoveAll(_entry => _entry == null) > 0)
            {
                updateFlag();
            }
        }
        #endregion

        #region<collider>
        private void addCollider(Collider _collider)
        {
            List<Transform> transformList = TypedList.GetAllTypedComponents<Transform>(this.transform);
            if (!transformList.Contains(_collider.transform))
            {
                this.colliderList.Add(_collider);
            }
            updateFlag();
        }

        private void removeCollider(Collider _collider)
        {
            this.colliderList.Remove(_collider);
            updateFlag();
        }

        private void updateFlag()
        {
            flagged = colliderList.Count > 0;
        }

        /// <summary>
        /// Remove a Trigger Collider from the Proximity Sensor
        /// </summary>
        /// <param name="_collider"></param>
        /// <returns>Whether the trigger was successfully removed</returns>
        public bool RemoveTrigger(Collider _collider)
        {
            return TriggerList.Remove(_collider);
        }

        /// <summary>
        /// Add a Trigger Collider to the Proximity Sensor. Collider must be a Trigger unique to the list.
        /// </summary>
        /// <param name="_collider"></param>
        /// <returns>Whether the trigger was successfully added</returns>
        public bool AddTrigger(Collider _collider)
        {
            if (!_collider.isTrigger)
            {
                Debug.LogWarning("Cannot add " + _collider.name + " because it is not a trigger");
                return false;
            }
            else if (TriggerList.Contains(_collider))
            {
                Debug.LogWarning("Cannot add " + _collider.name + " because it has already been assigned as a trigger");
                return false;
            }
            else
            {
                TriggerList.Add(_collider);
                return true;
            }
        }
        #endregion
    }
}
