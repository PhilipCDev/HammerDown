using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Interfaces;
using HammerDown.Player;

namespace HammerDown.GameObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class Gravity : MonoBehaviour, IGrabable
    {
        public float TimeUntilWarning = 4f;
        public float TimeFromWarningToDrop = 2f;
        public Animation WarningAnimation;
        private bool _enabled = true;
        private int _grabbed = 0;
        private int Grabbed
        {
            get
            {
                return _grabbed;
            }
            set
            {
                if (_grabbed > 0 && value < 1 && Enabled)
                {
                    StartWarningTimer();
                }
                _grabbed = value;
            }
        }

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
                    WarningAnimation.Stop();
                }
                else
                {
                    if (Grabbed < 1)
                    {
                        StartWarningTimer();
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

        private void StartWarningTimer()
        {
            if (warningTimer != null)
            {
                StopCoroutine(warningTimer);
            }
            warningTimer = StartCoroutine(Countdown(TimeUntilWarning, Warning));
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


        private void Drop()
        {
            rigidBody.useGravity = true;
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

        public void OnGrab(Hand hand)
        {
            Grabbed++;
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
        }

        public void OnRelease(Hand hand)
        {
            Grabbed--;
        }
    }
}
