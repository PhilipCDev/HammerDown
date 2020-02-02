using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HammerDown.Interfaces;
using HammerDown.Sound;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Movement))]
    public class Hammer : MonoBehaviour
    {
        Movement movement;
        Animator animator;
        public GameObject trigger;

        Coroutine hammerUpCoroutine;

        bool init;

        public CameraEffects cameraEffects;


        private void Awake()
        {
            if (init)
                return;

            Game.instance.RegisterHammer(this);
            Game.instance.GameStart += delegate { gameObject.SetActive(true); };
            Game.instance.GameOver += delegate { gameObject.SetActive(false); };
            gameObject.SetActive(false);
            init = true;
        }

        private void Start()
        {
            movement = GetComponent<Movement>();
            animator = GetComponentInChildren<Animator>();
            trigger.SetActive(false);
        }

        public void Trigger(InputAction.CallbackContext context)
        {
            if (context.performed)
                HammerDown();
        }

        private void OnTriggerEnter(Collider other)
        {
            IHitable[] hits = other.GetComponentsInChildren<IHitable>();
            if (hits.Length == 0)
                return;

            foreach  (IHitable hit in hits)
            {
                hit.OnHit(this);
            }
        }

        void HammerDown()
        {
            movement.MoveDown();
            if (hammerUpCoroutine != null) StopCoroutine(MoveHammerUp());
            hammerUpCoroutine = StartCoroutine(MoveHammerUp());
            animator.Play("Down");
            trigger.SetActive(true);
            SoundManager.PlaySound(SoundEffects.HAMMER);
            if (cameraEffects != null)
            {
                cameraEffects.Shake();
            }
        }

        IEnumerator MoveHammerUp(float delay = 0.4f)
        {
            yield return new WaitForSeconds(delay);
            movement.MoveUp();
            animator.Play("Up");
            trigger.SetActive(false);
        }
    }
}
