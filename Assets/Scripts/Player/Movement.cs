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

        public LayerMask wallMask;
        public LayerMask floorMask;

        public bool OnWall { private set; get; }

        Vector2 moveAxis;

        bool downMoving;
        bool upMoving;
        public Rigidbody rigid { private set; get; }

        private void Start()
        {
            rigid = GetComponent<Rigidbody>();
            OnWall = true;
        }

        private void FixedUpdate()
        {
            if (!confinedToWall)
                SnapToObject();
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
        
        void SnapToObject()
        {
            //Check Down to see if hitting ground
            if (Physics.Raycast(rigid.position, transform.forward,
                out RaycastHit hitFloor, 0.5f, floorMask))
            {
                //Snap to Wall
                transform.up = hitFloor.normal;
                OnWall = false;
                return;
            }

            if (Physics.Raycast(rigid.position, - transform.forward,
                out RaycastHit hitWall, 0.3f, wallMask))
            {
                //Snap to Wall
                transform.up = hitWall.normal;
                OnWall = true;
                return;
            }
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
