using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Movement))]
    public class Hammer : MonoBehaviour
    {
        Movement movement;

        Coroutine hammerUpCoroutine;

        private void Start()
        {
            movement = GetComponent<Movement>();
        }

        public void Trigger(InputAction.CallbackContext context)
        {
            if (context.performed)
                HammerDown();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        void HammerDown()
        {
            movement.MoveDown();
            if (hammerUpCoroutine != null) StopCoroutine(MoveHammerUp());
            hammerUpCoroutine = StartCoroutine(MoveHammerUp());
        }

        IEnumerator MoveHammerUp(float delay = 0.4f)
        {
            yield return new WaitForSeconds(delay);
            movement.MoveUp();
        }
    }
}
