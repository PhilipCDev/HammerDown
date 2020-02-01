﻿using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.GameObjects;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Nail : MonoBehaviour, IGrabable, IHitable
    {
        public int timesToHit = 2;
        
        private int _hitCounter = 0;
        private bool _stateHolding = false;
        private bool _stateFixed = false;
        private Gravity _gravity;
        private NailStates _nailState;

        public enum NailStates
        {
            Loose, 
            Holding, 
            ABitInWall, // touches wall, was hit more than once but not enough
            Fixed, // hitcounter == times to hit
            Destroyed // hit, but not on board 
        }
        
        private void Start()
        {
            _nailState = NailStates.Loose;
            _gravity = gameObject.GetComponent<Gravity>();
        }
        
        public void OnGrab(Hand hand)
        {
            if (_nailState != NailStates.Loose)
            {
                Debug.Log("Nail can't be moved");
                return;
            }

            // TODO maybe align the nail, such that his head faces the camera
            
            gameObject.transform.parent = hand.transform;
            _nailState = NailStates.Holding;
        }

        public void OnRelease(Hand hand)
        {
            if (_nailState == NailStates.Holding)
            {
                gameObject.transform.parent = null;
                _gravity.Enabled = true;
                _nailState = NailStates.Loose;
                return;
            }

            _gravity.Enabled = false;
        }

        public void OnHit(Hammer hammer)
        {
            if (_nailState == NailStates.Holding)
            {
                // TODO Check if on board
                
                Debug.Log("First Hit on Nail");
                _hitCounter++;
                _nailState = NailStates.ABitInWall;
                return;
            }
            
            if (_nailState == NailStates.ABitInWall)
            {
                _hitCounter++;
                if (_hitCounter >= timesToHit)
                {
                    Debug.Log("Nail fixed in wall");
                    _nailState = NailStates.Fixed;
                }
                else
                {
                    Debug.Log("Nail got punshed more into wall");
                }
                return;
            }

            if (_nailState == NailStates.Destroyed)
            {
                Debug.Log("Can't hit a destroyed nail");
            }
        }
    }
}
