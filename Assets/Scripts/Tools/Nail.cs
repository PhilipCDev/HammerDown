using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.GameObjects;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Nail : GrabableObject, IHitable
    {
        public int timesToHit = 2;
        public LayerMask movementMask;
        public LayerMask hitMask;
        
        private int _hitCounter = 0;
        private bool _stateHolding = false;
        private NailStates _nailState;

        public enum NailStates
        {
            Loose, 
            Holding, 
            ABitInWall, // touches wall, was hit more than once but not enough
            Fixed, // hitcounter == times to hit
            Destroyed // hit, but not on board 
        }

        protected override void SetUp()
        {
            _nailState = NailStates.Loose;
            
        }


        public override void OnGrab(Hand hand)
        {
            _stateHolding = true;
            if (_nailState != NailStates.Loose)
            {
                Debug.Log("Nail can't be moved");
                return;
            }

            // TODO maybe align the nail, such that his head faces the camera
            
            gameObject.transform.parent = hand.transform;
            _nailState = NailStates.Holding;
        }

        public override void OnRelease(Hand hand)
        {
            _stateHolding = false;
            gameObject.transform.parent = null;
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
            if (_nailState == NailStates.Holding && _stateHolding)
            {
                // TODO Check if on board
                if (!Game.instance.board.IsOnBoard(new Vector2(transform.position.x, transform.position.y )))
                {
                    Debug.Log("Nail needs to be on board");
                    _nailState = NailStates.Destroyed;
                    return;
                }
                
                if (Physics.Raycast(rigid.position, - transform.up, 
                    out RaycastHit hit, 1000.0f, movementMask))
                {
                    hit.transform.gameObject.GetComponent<Plank>().AddNail(this);
                    
                }
                
                Debug.Log("First Hit on Nail");
                _hitCounter++;
                _nailState = NailStates.ABitInWall;
                return;
            }
            
            if (_nailState == NailStates.ABitInWall && _stateHolding)
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

            if (_nailState == NailStates.Fixed)
            {
                Debug.Log("Already in wall!");
            }
        }
    }
}
