﻿using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Animals;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Movement))]
    public class Hand : MonoBehaviour, IHitable
    {
        Movement movement;
        Animator animator;

        public float fearPushStrength = 800;
        public float onHitPushStrength = 500;

        List<GameObject> grabbedObjectsInRange = new List<GameObject>();

        GameObject activeGrabbedObject;

        bool init;

        private void Awake()
        {
            if (init)
                return;

            Game.instance.RegisterHand(this);
            Game.instance.GameStart += delegate { gameObject.SetActive(true); };
            Game.instance.GameOver += delegate { gameObject.SetActive(false); };
            gameObject.SetActive(false);
            init = true;
        }

        private void Start()
        {
            movement = GetComponent<Movement>();
            animator = GetComponentInChildren<Animator>();         
        }

        private void Update()
        {
            //Hand should not collide with hammer when not on wall
            Physics.IgnoreLayerCollision(8, 9, !movement.OnWall);
        }

        public void Trigger(InputAction.CallbackContext context)
        {
            if (context.performed)
                GrabBegin();

            if (context.canceled)
                GrabEnd();
        }

        public void Feared(Animal fearer)
        {
            Debug.Log("Feared");
            Vector3 dir = (transform.position - fearer.transform.position).normalized;
            movement.PushAway(dir, fearPushStrength);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<ITouchable>() != null)
            {
                ITouchable[] touches = other.gameObject.GetComponentsInChildren<ITouchable>();
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
                ITouchable[] touches = other.gameObject.GetComponentsInChildren<ITouchable>();
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

                //Grabbed Nail
                if(activeGrabbedObject.layer == 10)
                {
                    animator.CrossFade("StartNail", 0.2f);
                }
                else
                {
                    animator.CrossFade("StartPlate", 0.2f);
                }

                animator.SetBool("released", false);
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
                animator.SetBool("released", true);
            }
        }

        public void OnHit(Hammer hand)
        {
            Vector3 dir = - transform.right  + transform.up * 0.4f;
            movement.PushAway(dir, onHitPushStrength);
            GrabEnd();
        }
    }
}

