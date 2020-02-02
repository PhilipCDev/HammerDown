using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Map.Components;
using HammerDown.Player;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

namespace HammerDown.Animals
{
    public abstract class Animal : MonoBehaviour, IHitable
    {
        public enum Targets
        {
            Hand, 
            Hammer, 
            Hole
        }

        public Targets currentTarget;
        public float speed;
        public bool active;
        protected Transform _targetPosition;
        protected Animator _animator;

        private void Start()
        {
            SetUp();
        }

        protected Transform GetTargetPosition(Targets target)
        {
            switch (target)
            {
                case Targets.Hammer:
                    return Game.instance.hammer.transform;
                case Targets.Hand:
                    Debug.Log(Game.instance.hand);
                    return Game.instance.hand.transform;
                case Targets.Hole:
                    List<Hole> holes = Game.instance.board.holes;
                    var index = Random.Range(0, holes.Count - 1);
                    return holes[index].transform;
                    // TODO problem with already closed holes?
            }
            Debug.Log("There is no target for this animal!");
            return null;
        }

        protected virtual void GoToTarget()
        {
            throw new NotImplementedException();
        }

        protected virtual void SetUp()
        {
            
        }

        private void Update()
        {
            if(!active) return;
            GoToTarget();
        }

        public virtual void OnHit(Hammer hand)
        {
            throw new NotImplementedException();
        }

        public virtual void Die()
        {
            throw new NotImplementedException();
        }
    }
}
