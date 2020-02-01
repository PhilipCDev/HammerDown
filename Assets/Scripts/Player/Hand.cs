using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using HammerDown.Interfaces;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Movement))]
    public class Hand : MonoBehaviour
    {
        Movement movement;

        List<GameObject> grabbedObjectsInRange = new List<GameObject>();
        List<GameObject> touchedObjects = new List<GameObject>();

        GameObject activeGrabbedObject;

        private void Start()
        {
            movement = GetComponent<Movement>();
        }

        public void Trigger(InputAction.CallbackContext context)
        {
            if (context.performed)
                GrabBegin();

            if (context.canceled)
                GrabEnd();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<ITouchable>() != null)
            {
                ITouchable[] touches = activeGrabbedObject.GetComponentsInChildren<ITouchable>();
                foreach (ITouchable t in touches)
                {
                    t.OnTouchEnter(this);
                }
            }

            if (other.gameObject.GetComponent<IGrabable>() != null)
            {
                if (!grabbedObjectsInRange.Contains(other.gameObject))
                    grabbedObjectsInRange.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.GetComponent<ITouchable>() != null)
            {
                ITouchable[] touches = activeGrabbedObject.GetComponentsInChildren<ITouchable>();
                foreach (ITouchable t in touches)
                {
                    t.OnTouchExit(this);
                }
            }

            if (other.gameObject.GetComponent<IGrabable>() != null)
            {
                if (grabbedObjectsInRange.Contains(other.gameObject))
                    grabbedObjectsInRange.Remove(other.gameObject);
            }
        }

        void GrabBegin()
        {
            if (grabbedObjectsInRange.Count == 0)
            {
                Debug.Log("No grabbable object in range!");
                return;
            }

            float minDistance = float.MaxValue;
            GameObject nearObject = null;
            foreach (GameObject obj in grabbedObjectsInRange)
            {
                float dis = Vector3.Distance(obj.transform.position, transform.position);
                if(dis < minDistance)
                {
                    minDistance = dis;
                    nearObject = obj;
                }
            }

            activeGrabbedObject = nearObject;
            if(activeGrabbedObject != null)
            {
                IGrabable[] grabs = activeGrabbedObject.GetComponentsInChildren<IGrabable>();
                foreach (IGrabable g in grabs)
                {
                    g.OnGrab(this);
                }
            }
        }

        void GrabEnd()
        {
            if (activeGrabbedObject != null)
            {
                IGrabable[] grabs = activeGrabbedObject.GetComponentsInChildren<IGrabable>();
                foreach (IGrabable g in grabs)
                {
                    g.OnRelease(this);
                }
            }
        }
    }
}

