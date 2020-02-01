using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.GameObjects;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{

    public abstract class GrabableObject : MonoBehaviour, IGrabable, ITouchable
    {

        protected Gravity _gravity;

        private void Start()
        {
            _gravity = gameObject.GetComponent<Gravity>();
        }

        public virtual void OnGrab(Hand hand)
        {
            gameObject.transform.parent = hand.transform;
        }

        public virtual void OnRelease(Hand hand)
        {
            gameObject.transform.parent = null;
        }

        public virtual void OnTouchEnter(Hand hand)
        {
            Debug.Log("Hand touch start: " + gameObject.name);
        }

        public virtual void OnTouchStay(Hand hand)
        {
            Debug.Log("Hand touch: " + gameObject.name);
        }

        public virtual void OnTouchExit(Hand hand)
        {
            Debug.Log("Hand touch exit: " + gameObject.name);
        }
    }
    
}