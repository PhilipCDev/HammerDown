using UnityEngine;
using UnityEngine.InputSystem;

namespace HammerDown.Tools
{
    [RequireComponent(typeof(Movement))]
    public class Hand : MonoBehaviour
    {
        Movement movement;

        private void Start()
        {
            movement = GetComponent<Movement>();
        }

        public void Trigger(InputAction.CallbackContext context)
        {
            if (context.performed)
                GrabBegin();

            if (context.canceled)
                GrabEnd();
        }

        void GrabBegin()
        {
            movement.MoveDown();
        }

        void GrabEnd()
        {
            movement.MoveUp();
        }
    }
}

