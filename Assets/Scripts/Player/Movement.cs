using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammerDown.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public float speed = 2.0f;
        public bool confinedToWall;
        public float distanceToWall = 1.2f;
        public float hitDistance = 0.2f;

        public LayerMask movementMask;
        public LayerMask hitMask;
        Vector2 moveAxis;
        Vector3 moveOffset;

        bool downMoving;
        bool upMoving;
        public Rigidbody rigid { private set; get; }

        private void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            CheckForForwardObject();
            Move();
        }


        void Move()
        {
            Vector3 move = transform.right * moveAxis.x + transform.forward * moveAxis.y;

            if(downMoving)
            {
                move += -transform.up;
            }
            else
            if(upMoving)
            {
                move += transform.up;
                CheckDistanceToWall();
            }
   
            rigid.MovePosition(rigid.position + move.normalized * Time.fixedDeltaTime * speed);
        }

        public void UpdateMovement(InputAction.CallbackContext context)
        {
            moveAxis = context.ReadValue<Vector2>();
            moveAxis *= -1.0f;
        }

        public void MoveDown()
        {
            downMoving = true;
            upMoving = false;
        }

        public void MoveUp()
        {
            downMoving = false;
            upMoving = true;
        }

        void CheckDistanceToWall()
        {
            if (Physics.Raycast(rigid.position, - transform.up, 
                out RaycastHit hit, 1000.0f, movementMask))
            {
                Debug.Log(hit.distance);
                //Stop moving up when enough distance from wall
                if (hit.distance >= distanceToWall)
                    upMoving = false;
            }
        }

        void CheckForForwardObject()
        {
            if (Physics.Raycast(rigid.position, -transform.up, hitDistance, hitMask))
            {
                //Stop moving object down when colliding
                downMoving = false;
            }
        }
    }
}
