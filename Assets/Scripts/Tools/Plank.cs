using System.Collections;
using System.Collections.Generic;
using HammerDown.Interfaces;
using HammerDown.Map;
using HammerDown.Player;
using UnityEngine;

namespace HammerDown.Tools
{
    public class Plank : GrabableObject
    {
        [SerializeField]
        private int nails = 0;
        private List<Nail> allNails;
        private List<Vector3> allNailPos;
        private RectanglePos _rectanglePos;
        private Collider _collider;

        protected override void SetUp()
        {
            allNails = new List<Nail>();
            allNailPos = new List<Vector3>();
            _collider = gameObject.GetComponent<Collider>();
            _rectanglePos.leftBottomFront = _collider.bounds.min;
            _rectanglePos.rightTopBack = _collider.bounds.max;
            Game.instance.boardStatus.AddPlank(this);
        }

        public override void OnGrab(Hand hand)
        {
            if (allNails.Count > 0)
            {
                return;
            }
            gameObject.transform.parent = hand.transform;
            Vector3 angles = gameObject.transform.localEulerAngles;
            angles.x = -90.0f;
            angles.z = 0;
            gameObject.transform.localEulerAngles = angles;
        }

        public override void OnRelease(Hand hand)
        {
            if (nails >= 2)
            {
                _gravity.Enabled = false;
                Debug.Log("Deactivated Gravity of " + gameObject.name + ", because plank has more than 2 nails");
            }
            gameObject.transform.parent = null;
        }

        public void AddNail(Nail nail)
        {
            if (!allNails.Contains(nail))
            {
                Game.instance.boardStatus.RemovePlank(this);
                Debug.Log("Added nail to plank");
                allNails.Add(nail);
                allNailPos.Add(new Vector2(nail.transform.position.x, nail.transform.position.y));
                if (Game.instance.board.IsPlankFixed(_rectanglePos, allNailPos))
                {

                    Game.instance.boardStatus.AddFixedPlanks(this);
                }
                nails++;
            }
            else
            {
                Debug.Log("Nail Already part of plank");
            }
        }
    }
    
}
