using System.Collections;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Plank : GrabableObject
    {
        [SerializeField]
        private int nails = 0;
        private List<Nail> allNails;

        protected override void SetUp()
        {
            allNails = new List<Nail>();
        }

        public override void OnRelease(Hand hand)
        {
            if (nails >= 2)
            {
                _gravity.Enabled = false;
                Debug.Log("Deactivated Gravity of " + gameObject.name + ", because plank has more than 2 nails");
            }
            gameObject.transform.parent = null;

        }

        public void AddNail(Nail nail)
        {
            if (!allNails.Contains(nail))
            {
                Debug.Log("Added nail to plank");
                nails++;
            }
            else
            {
                Debug.Log("Nail Already part of plank");
            }
        }

    }
    
}
