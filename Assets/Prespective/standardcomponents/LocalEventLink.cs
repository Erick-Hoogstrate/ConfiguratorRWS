using System;
using UnityEngine;

namespace u040.prespective.standardcomponents
{
    /// <summary>
    /// This component invokes relavant actions when local events fire.
    /// </summary>
    public class LocalEventLink : MonoBehaviour
    {
        public Component Listener = null;
        public Action<Collision> CollisionEnter;
        public Action<Collision> CollisionStay;
        public Action<Collision> CollisionExit;
        public Action<Collider> TriggerEnter;
        public Action<Collider> TriggerStay;
        public Action<Collider> TriggerExit;
        public Action PreRender;
        public Action PostRender;

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionEnter != null)
            {
                CollisionEnter.Invoke(collision);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (CollisionStay != null)
            {
                CollisionStay.Invoke(collision);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (CollisionExit != null)
            {
                CollisionExit.Invoke(collision);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (TriggerEnter != null)
            {
                TriggerEnter.Invoke(other);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (TriggerStay != null)
            {
                TriggerStay.Invoke(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (TriggerExit != null)
            {
                TriggerExit.Invoke(other);
            }
        }
        private void OnPreRender()
        {
            if (PreRender != null)
            {
                PreRender.Invoke();
            }
        }
        private void OnPostRender()
        {
            if (PostRender != null)
            {
                PostRender.Invoke();
            }
        }

        public static LocalEventLink Create(GameObject _gameObject, Component _listener)
        {
            LocalEventLink _newLink = _gameObject.AddComponent<LocalEventLink>();
            _newLink.Listener = _listener;
            return _newLink;
        }

    }
}
