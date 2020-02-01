using System.Collections;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Plank : GrabableObject
    {
        private int nails = 0;
        

        public override void OnRelease(Hand hand)
        {
            if (nails >= 2)
            {
                _gravity.Enabled = false;
                Debug.Log("Deactivated Gravity of " + gameObject.name + ", because plank has more than 2 nails");
            }
            gameObject.transform.parent = null;

        }

    }
    
}
