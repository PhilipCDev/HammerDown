using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.GameObjects;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    [SelectionBase]
    public class Nail : GrabableObject, IHitable
    {
        public int timesToHit = 2;
        public Vector3 handOffset;
        public Vector3 nailStep;
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
            Debug.Log(Game.instance.board);            
        }


        public override void OnGrab(Hand hand)
        {
            _stateHolding = true;
            if (_nailState != NailStates.Loose)
            {
                Debug.Log("Nail can't be moved");
                return;
            }

            //Snap the nail to the hand
            gameObject.transform.parent = hand.transform;
            gameObject.transform.localEulerAngles = Vector3.zero;
            gameObject.transform.localPosition = handOffset;
            _nailState = NailStates.Holding;
            _gravity.Enabled = false;
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
                if (!Game.instance.board.IsOnBoard(transform))
                {
                    Debug.Log("Nail needs to be on board");
                    _nailState = NailStates.Destroyed;
                    Game.instance.boardStatus.RemoveNail(this);
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
                //Push Nail in Wall
                gameObject.transform.localPosition += nailStep;
                return;
            }
            
            if (_nailState == NailStates.ABitInWall && _stateHolding)
            {
                _hitCounter++;
                if (_hitCounter >= timesToHit)
                {
                    Debug.Log("Nail fixed in wall");
                    _nailState = NailStates.Fixed;
                    //Push Nail in Wall
                    gameObject.transform.localPosition += nailStep;
                    Game.instance.boardStatus.RemoveNail(this);
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
                Game.instance.boardStatus.RemoveNail(this);
            }

            if (_nailState == NailStates.Fixed)
            {
                Debug.Log("Already in wall!");
            }
        }
    }
}
