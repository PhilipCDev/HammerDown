using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Interfaces;
using HammerDown.Tools;

namespace HammerDown.GameObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class Gravity : MonoBehaviour, ITouchable
    {
        public float TimeUntilWarning = 4f;
        public float TimeFromWarningToDrop = 2f;
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
            OnTouchExit(null);
        }

        // Update is called once per frame
        void Update()
        {

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

        public void OnTouchEnter(Hand hand)
        {
            if (_enabled == false)
            {
                return;
            }
            rigidBody.useGravity = false;
            if (warningTimer != null)
            {
                StopCoroutine(warningTimer);
            }
            if (dropTimer != null)
            {
                StopCoroutine(dropTimer);
            }
        }

        public void OnTouchStay(Hand hand)
        {

        }

        public void OnTouchExit(Hand hand)
        {
            if (_enabled == false)
            {
                return;
            }
            if (warningTimer != null)
            {
                StopCoroutine(warningTimer);
            }
            warningTimer = StartCoroutine(Countdown(TimeUntilWarning,Warning));
        }

        private void Drop()
        {
            rigidBody.useGravity = true;
        }

        private void Warning()
        {
            Debug.Log("Warning " + gameObject.name);
            if (dropTimer != null)
            {
                StopCoroutine(dropTimer);
            }
            dropTimer = StartCoroutine(Countdown(TimeFromWarningToDrop, Drop));
        }

    }
}
