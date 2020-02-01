using System.Collections;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Tools;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Board : MonoBehaviour, IGrabable
    {
        public float timeToFallDown;
        private bool _isOnWall = false;
        private int nails = 0;

        public void OnGrab(Hand hand)
        {
            gameObject.transform.parent = hand.transform;
        }

        public void OnRelease(Hand hand)
        {
            gameObject.transform.parent = null;
            // TODO Check if wall is free
            _isOnWall = true;
            StartCoroutine(nameof(FallDown));
        }

        private IEnumerator FallDown()
        {
            yield return new WaitForSeconds(timeToFallDown);
            if (nails <= 3)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }


        }
        
    }
    
}
