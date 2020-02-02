using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HammerDown.Interfaces;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Movement))]
    public class Hammer : MonoBehaviour
    {
        Movement movement;
        Animator animator;

        Coroutine hammerUpCoroutine;


        private void Awake()
        {
            Game.instance.RegisterHammer(this);
        }

        private void Start()
        {
            movement = GetComponent<Movement>();
            animator = GetComponentInChildren<Animator>();
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
        }

        IEnumerator MoveHammerUp(float delay = 0.4f)
        {
            yield return new WaitForSeconds(delay);
            movement.MoveUp();
            animator.Play("Up");
        }
    }
}
