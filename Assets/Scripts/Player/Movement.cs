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
        public float moveRampSpeed = 3;
        public bool confinedToWall;
        public float distanceToWall = 1.2f;
        public float hitDistance = 0.2f;

        float moveRamp;

        public LayerMask movementMask;
        public LayerMask hitMask;

        public LayerMask wallMask;
        public LayerMask floorMask;

        public bool OnWall { private set; get; }

        Vector2 moveAxis;

        bool downMoving;
        bool upMoving;

        float pushAwayTime;
        bool pushAway;
        Vector3 pushDir;

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
            PushAwayUpdate();

            CheckForForwardObject();
            Move();
        }

        void PushAwayUpdate()
        {
            if (pushAway)
            {
                if (!CheckInBounds(Vector3.zero) || Time.time - pushAwayTime > 0.4f)
                {
                    rigid.velocity = Vector3.zero;
                    rigid.MovePosition(rigid.position - pushDir * 0.1f);
                    pushAway = false;
                }      
            }
        }

        public void PushAway(Vector3 dir, float strength)
        {
            pushAway = true;
            rigid.AddForce(dir * strength);
            pushAwayTime = Time.time;
            pushDir = dir;
        }

        void Move()
        {
            if (pushAway)
                return;

            if (moveAxis.magnitude < 0.1f)
            {
                moveRamp = 0;
            }

            moveRamp = Mathf.Lerp(moveRamp, 1, Time.deltaTime * moveRampSpeed);

            Vector3 move = transform.right * moveAxis.x + transform.forward * moveAxis.y;
            move.Normalize();
            move *= moveRamp;

            if (downMoving)
            {
                move = move * 0.4f - transform.up;
            }
            else
            if (upMoving)
            {
                move = move * 0.4f + transform.up;
                CheckDistanceToWall();
            }

            Vector3 movement = move * Time.fixedDeltaTime * speed;

            if (!CheckInBounds(movement))
                return;

            rigid.MovePosition(rigid.position + movement);
        }


        bool CheckInBounds(Vector3 moveDir)
        {
            if (Physics.Raycast(rigid.position + moveDir, -transform.up,
                out RaycastHit hit, 1000.0f, movementMask))
            {
                return true;
            }

            return false;
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

            if (Physics.Raycast(rigid.position, -transform.forward,
                out RaycastHit hitWall, 0.3f, wallMask))
            {
                //Snap to Wall
                transform.up = hitWall.normal;
                OnWall = true;
                return;
            }

            upMoving = false;
            downMoving = false;
            //Fixed distance to object
            if (Physics.Raycast(rigid.position, -transform.up,
               out RaycastHit hit, 1000.0f, movementMask))
            {
                //Stop moving up when enough distance from wall
                if (hit.distance < distanceToWall * 0.9f)
                {
                    upMoving = true;
                }
                else if (hit.distance > distanceToWall * 1.1f)
                {
                    downMoving = true;
                }

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
            if (Physics.Raycast(rigid.position, -transform.up,
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
