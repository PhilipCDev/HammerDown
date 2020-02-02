using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Interfaces;
using HammerDown.Player;

namespace HammerDown.GameObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class Gravity : MonoBehaviour
    {
        public float TimeUntilWarning = 4f;
        public float TimeFromWarningToDrop = 2f;
        public bool changeKinematicState;
        public Animation WarningAnimation;
        private bool cannotFall;
        private bool _enabled = true;

        [SerializeField]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if (_enabled == false)
                {
                    if (warningTimer != null)
                    {
                        StopCoroutine(warningTimer);
                    }
                    if (dropTimer != null)
                    {
                        StopCoroutine(dropTimer);
                    }
                }
            }
        }


        private delegate void TimerDegelate();
        private Coroutine warningTimer;
        private Coroutine dropTimer;
        private Rigidbody rigidBody;


        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            //OnTouchExit(null);
        }

 
        private IEnumerator Countdown(float time, TimerDegelate func)
        {
            float normalizedTime = 0;
            while (normalizedTime <= time)
            {
                normalizedTime += Time.deltaTime;
                yield return null;
            }
            func();
        }

        public void Stabilized()
        {
            if (_enabled == false || rigidBody == null)
            {
                return;
            }

            rigidBody.useGravity = false;
            if(changeKinematicState)
                rigidBody.isKinematic = true;

            if (warningTimer != null)
            {
                StopCoroutine(warningTimer);
            }
            if (dropTimer != null)
            {
                StopCoroutine(dropTimer);
            }
        }

        public void DeStabilized()
        {
            if (_enabled == false || rigidBody == null || cannotFall)
            {
                return;
            }
            if (warningTimer != null)
            {
                StopCoroutine(warningTimer);
            }
            warningTimer = StartCoroutine(Countdown(TimeUntilWarning,Warning));
        }

        public void CanNoLongerFall()
        {
            cannotFall = true;
        }

        private void Drop()
        {
            rigidBody.useGravity = true;
            if(changeKinematicState)
                rigidBody.isKinematic = false;
        }

        private void Shake()
        {
            if (WarningAnimation == null)
            {
                Debug.LogError("Animation missing");
                return;
            }
            WarningAnimation.Play();
        }

        private void Warning()
        {
            Shake();
            if (dropTimer != null)
            {
                StopCoroutine(dropTimer);
            }
            dropTimer = StartCoroutine(Countdown(TimeFromWarningToDrop, Drop));
        }

    }
}
