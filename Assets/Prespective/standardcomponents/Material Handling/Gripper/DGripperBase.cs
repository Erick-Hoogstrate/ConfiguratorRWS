using System.Collections.Generic;
using UnityEngine;
using System;
using u040.prespective.math;
using u040.prespective.standardcomponents.virtualhardware.systems.gripper.fingers;

namespace u040.prespective.standardcomponents.virtualhardware.systems.gripper
{
    public class DGripperBase : MonoBehaviour
    {
        /// <summary>
        /// A list of all Gripper Fingers assigned to this GripperBase
        /// </summary>
        public List<DFingerSetting> GripperFingers = new List<DFingerSetting>();

        /// <summary>
        /// Add a unique Gripper Finger
        /// </summary>
        /// <param name="_finger"></param>
        /// <returns></returns>
        public bool AddFinger(DGripperFinger _finger, bool _inverted = false)
        {
            //If the list does not already contain the finger
            if (GripperFingers.Find((_entry) => _entry.Finger == _finger) == null)
            {
                //Add the finger
                DFingerSetting setting = new DFingerSetting();
                setting.Finger = _finger;
                setting.Inverted = _inverted;
                GripperFingers.Add(setting);
                return true;
            }

            //If the list does already contain the finger
            else
            {
                //Log warning, do not add finger
                Debug.LogWarning("Unable to add " + _finger.name + " since it has already been assigned as a finger");
                return false;
            }
        }
        public bool RemoveFinger(DGripperFinger _finger)
        {
            //Find the FingerSetting for this finger
            DFingerSetting setting = GripperFingers.Find((_entry) => _entry.Finger == _finger);

            //Remove it from the list
            return GripperFingers.Remove(setting);
        }

        internal void Awake()
        {
            //Remove all null fingers from the list
            GripperFingers.RemoveAll((_entry) =>
            {
                return _entry == null;
            });

            //Force all fingers to the same set position
            moveFingersToPosition(currentClosePercentage);

            //Store the Gripper base's current transform position and rotation
            storeTransformValues();
        }

        internal void FixedUpdate()
        {
            //Move objects along with the base if necessary
            processGrippedObjects();

            //Detect which object is currently being gripped
            detectGrippedObjects();

            //Make sure to open/close fingers if necessary
            positionFingers();

            /*
             * It is important that we move the finger into position last. This is necessary for a 
             * proper grip detection. If we move the fingers first, than detect a grip, the new 
             * actual grip will not be detected since the trigger events only fire after the loop
             * for the gripper base has been completed. This results in the gripper finger lagging 
             * a frame behind and the fingers to clip through gripped objects.
             */
        }

        #region<<Processing Gripper Objects>>

        private Vector3 storedTransformPosition;
        private Quaternion storedTransformRotation;

        private void processGrippedObjects()
        {
            /*
             * Each gripped object has to do 3 things:
             * - Cancel out gravity
             * - Inherit positional changes
             * - Inherit rotational changes
             */

            //First: Put drag on the rigidbodies
            //For each gripped object
            for (int i = 0; i < grippedObjects.Count; i++)
            {
                //Try to get the Rigidbody
                Rigidbody rigidbody = grippedObjects[i].Object.GetComponent<Rigidbody>();

                //If it has a Rigidbody and Gravity is used
                if (rigidbody)
                {
                    rigidbody.velocity *= (0.5f * Time.fixedDeltaTime);
                    rigidbody.angularVelocity *= (0.5f * Time.fixedDeltaTime);
                }
            }

            //Second: Inherit positional changes
            //Get current position
            Vector3 currentTransformPosition = this.transform.position;

            //Check if there has been a change in position
            if (currentTransformPosition != storedTransformPosition)
            {
                //Calculate the difference in position
                Vector3 deltaTransformPosition = currentTransformPosition - storedTransformPosition;

                //For each gripped object
                for (int i = 0; i < grippedObjects.Count; i++)
                {
                    //Buffer the GameObject
                    GrippedObject grippedObject = grippedObjects[i];

                    //Apply the same change in position to the gripped object
                    grippedObject.Object.transform.position += deltaTransformPosition;
                }
            }

            //Third: Inherit rotational changes
            //Get current rotation
            Quaternion currentTransformRotation = this.transform.rotation;

            //Check if there has been a change in rotation
            if (currentTransformRotation != storedTransformRotation)
            {
                //Calculate the difference in rotation
                Quaternion deltaTransformRotation = currentTransformRotation * Quaternion.Inverse(storedTransformRotation);

                //For each gripped object
                for (int i = 0; i < grippedObjects.Count; i++)
                {
                    //Buffer the GameObject
                    GrippedObject grippedObject = grippedObjects[i];

                    //Apply the same change in rotation to the gripped object's rotation
                    grippedObject.Object.transform.rotation = deltaTransformRotation * grippedObject.Object.transform.rotation;

                    //Calculate the new relative position by applying the difference in rotation to the relative vector
                    Vector3 rotatedRelativePosition = deltaTransformRotation * grippedObject.RelativePosition;

                    //Calculate the difference in relative position
                    Vector3 deltaRelativePosition = rotatedRelativePosition - grippedObject.RelativePosition;

                    //Apply difference in relative position to the gripped object
                    grippedObject.Object.transform.position += deltaRelativePosition;
                }
            }

            //Store the current transform values for next frame
            storeTransformValues();
        }

        private void storeTransformValues()
        {
            storedTransformPosition = this.transform.position;
            storedTransformRotation = this.transform.rotation;
        }
        #endregion

        #region<< Gripping Objects >>
        /// <summary>
        /// A list of all gripped objects and their relative positions
        /// </summary>
        private List<GrippedObject> grippedObjects = new List<GrippedObject>();

        /// <summary>
        /// A list containing all gripped objects
        /// </summary>
        public List<GameObject> GrippedGameObjects
        {
            get
            {
                return this.grippedObjects.ConvertAll<GameObject>((_entry) => { return _entry.Object; });
            }
        }

        /// <summary>
        /// A list of all objects detected by fingers and the number of fingers that detect them
        /// </summary>
        [SerializeField] private Dictionary<GameObject, int> detectedObjects = new Dictionary<GameObject, int>();

        protected virtual void detectGrippedObjects()
        {
            //Clear the entire lists each frame for the new update
            clearGrippedObjectsList();
            detectedObjects.Clear();
            fingersCollide = false;

            //For each gripper finger
            for (int i = 0; i < GripperFingers.Count; i++)
            {
                //Get all the objects the finger detects
                List<GameObject> gameObjects = GripperFingers[i].Finger.DetectedObjects;

                //For each detected object
                for (int j = 0; j < gameObjects.Count; j++)
                {
                    GameObject gameObject = gameObjects[j];
                    int count = 0;

                    //Check whether the detected object is another finger
                    if (GripperFingers.Find((_entry) => _entry.Finger.Trigger.gameObject == gameObject) != null)
                    {
                        fingersCollide = true;
                        continue;
                    }

                    //Check whether this object was already detected by other fingers
                    if (detectedObjects.TryGetValue(gameObject, out count))
                    {
                        //Add 1 to its current value
                        detectedObjects[gameObject] = ++count;
                    }

                    //If not already detected
                    else
                    {
                        //Add it as a detected object
                        detectedObjects.Add(gameObject, 1);
                        count = 1;
                    }

                    //If the objects is detected by all fingers
                    if (count == GripperFingers.Count)
                    {
                        //Check whether the object's rigidbody uses gravity
                        bool usedGravity = false;
                        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
                        if (rigidbody)
                        {
                            usedGravity = rigidbody.useGravity;
                            rigidbody.useGravity = false;
                        }

                        //Create the new datastruct
                        GrippedObject grippedObject = new GrippedObject()
                        {
                            Object = gameObject,
                            RelativePosition = gameObject.transform.position - this.transform.position,
                            UsedGravity = usedGravity
                        };

                        //Add it as a gripped object
                        grippedObjects.Add(grippedObject);
                    }
                }
            }

            /*
             * If all grippers need a detection per definition, change system to set al detected object from finger 1 in dictionary, 
             * then for every other finger compare which dectected objects are in the dictionary, ignore the others an clear the 
             * dictionary entries that do not match. In the end only all matching objects remain or nothing is detected by every 
             * finger and the method can be stopped early.
             */
        }

        private void clearGrippedObjectsList()
        {
            for (int i = 0; i < grippedObjects.Count; i++)
            {
                GrippedObject grippedObject = grippedObjects[i];
                Rigidbody rigidbody = grippedObject.Object.GetComponent<Rigidbody>();
                if (rigidbody)
                {
                    rigidbody.useGravity = grippedObject.UsedGravity;
                }
            }
            grippedObjects.Clear();
        }

        /// <summary>
        /// A data struct to save information on a gripped object
        /// </summary>
        private struct GrippedObject
        {
            public GameObject Object;
            public Vector3 RelativePosition;
            public bool UsedGravity;
        }
        #endregion

        #region << Moving Fingers>>
        /// <summary>
        /// A bool to track whether fingers collider with each other
        /// </summary>
        private bool fingersCollide = false;

        [SerializeField] private double targetClosePercentage = 0d;
        /// <summary>
        /// The close percentage the gripper fingers need to move to
        /// </summary>
        public double TargetClosePercentage
        {
            get { return this.targetClosePercentage; }
            set
            {
                double clampedValue = PreSpectiveMath.Clamp(value, 0d, 1d);

                if (this.targetClosePercentage != clampedValue)
                {
                    this.targetClosePercentage = clampedValue;
                }
            }
        }

        [SerializeField] private double currentClosePercentage = 0d;
        /// <summary>
        /// The current close percentage of the fingers
        /// </summary>
        public double ClosePercentage
        {
            get { return this.currentClosePercentage; }
            private set
            {
                this.currentClosePercentage = value;
            }
        }

        /// <summary>
        /// The time the gripper fingers need to go from fully open to fully closed
        /// </summary>
        public double CloseTime = 1f;

        /// <summary>
        /// Close the fingers at a pace according to the set speed
        /// </summary>
        private void positionFingers()
        {
            //If we havent closed enough
            if (currentClosePercentage != TargetClosePercentage)
            {
                //Determine how many percent the gripper still needs to close
                double percentageDifference = TargetClosePercentage - currentClosePercentage;

                //If we have an object detected or fingers collider with each other
                if (grippedObjects.Count > 0 || fingersCollide)
                {
                    //Only allow moves that open te gripper
                    percentageDifference = Math.Min(percentageDifference, 0d);

                    //If not move should occur, return
                    if (percentageDifference == 0d) { return; }
                }

                //Clamp the percentage so that we never close more than the set speed allows over time
                double maxAllowedMove = Time.fixedDeltaTime / this.CloseTime;
                double clampedPercentageOverSpeed = PreSpectiveMath.Clamp(percentageDifference, -maxAllowedMove, maxAllowedMove);

                //Determine the position the fingers would need to move to
                currentClosePercentage = PreSpectiveMath.Clamp(currentClosePercentage + clampedPercentageOverSpeed, 0d, 1d);

                //Set all fingers to set percentage
                moveFingersToPosition(currentClosePercentage);
            }
        }

        /// <summary>
        /// Move all fingers to the same position
        /// </summary>
        /// <param name="_percentage"></param>
        private void moveFingersToPosition(double _percentage)
        {
            //Set all fingers to set percentage
            for (int i = 0; i < GripperFingers.Count; i++)
            {
                double positionPercentage = _percentage;

                //if percentage needs to be inverted
                if (GripperFingers[i].Inverted)
                {
                    positionPercentage = 1d - _percentage;
                }

                //Set position of individual finger
                GripperFingers[i].Finger.SetPosition(positionPercentage);
            }
        }

        public enum GripperState { Open = 0, Closed = 1, Opening = 2, Closing = 3 };

        public GripperState State
        {
            get
            {
                if (GrippedGameObjects.Count > 0 || ClosePercentage == 1d || fingersCollide)
                {
                    return GripperState.Closed;
                }
                else if (ClosePercentage == TargetClosePercentage)
                {
                    return GripperState.Open;
                }
                else
                {
                    if (ClosePercentage > TargetClosePercentage)
                    {
                        return GripperState.Opening;
                    }
                    else
                    {
                        return GripperState.Closing;
                    }
                }
            }
        }
        #endregion

        [Serializable]
        public class DFingerSetting
        {
            public DGripperFinger Finger;
            public bool Inverted = false;
        }
    }
}
