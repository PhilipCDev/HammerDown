using System;
using System.Collections;
using System.Collections.Generic;
using HammerDown.Map.Components;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

namespace HammerDown.Animals
{
    public abstract class Animal : MonoBehaviour
    {
        public enum Targets
        {
            Hand, 
            Hammer, 
            Hole
        }

        public Targets currentTarget;
        private Transform _targetPosition;

        private void Start()
        {
            SetUp();
        }

        private Transform GetTargetPosition(Targets target)
        {
            switch (target)
            {
                case Targets.Hammer:
                    return Game.instance.hammer.transform;
                case Targets.Hand:
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

    }
}
