using HammerDown.Map.Components;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        private RectanglePos boardPos;
        private float planeOffset;

        //Hannah: Move to Game
        public BoardStatus boardStatus;
        
        //List of all holes, just use and not change entries! Currently holes are not implemented
        public List<Hole> holes;

        #region private Functions
        private void Start()
        {
            CalculateBoardPos();
        }
        private void CalculateBoardPos()
        {
            //Bounds bounds = new Bounds(transform.position, Vector3.zero);
            List<Bounds> bounds = gameObject.GetComponentsInChildren<Transform>()
                .Where(t => t.gameObject.layer == 12)
                .Select<Transform, Bounds>(t => t.gameObject.GetComponent<Collider>().bounds)
                .ToList();

            Bounds b = bounds[0];
            for (int i=1; i<bounds.Count; i++)
            {
                b.Encapsulate(bounds[i].min);
            }

            if(b.extents.x < b.extents.z)
            {
                boardPos = new RectanglePos(new Vector2(b.center.z-b.extents.z,b.center.y-b.extents.y), new Vector2(b.center.z + b.extents.z, b.center.y + b.extents.y));
            }
            else
            {
                boardPos = new RectanglePos(new Vector2(b.center.x - b.extents.x, b.center.y - b.extents.y), new Vector2(b.center.x + b.extents.x, b.center.y + b.extents.y));
            }
            planeOffset = Vector3.Dot(b.center, transform.forward);
            Debug.Log(planeOffset);
        }
        #endregion

        #region public Functions
        //Bool function. Give a point and returns if it is on a board. Check for Nail (Nail can only be nailed in if OnBoard is true)
        public bool IsOnBoard(Vector2 position)
        {
            //TODO: think of problem with 45 deg plane
            //Hannah: Speak about forward direction, meaning of x
            Vector3 startPos = transform.up * position.y + transform.right * position.x + transform.forward * (planeOffset + 1);
            RaycastHit[] hits = Physics.RaycastAll(startPos, -transform.forward, 1, 1 << 12 | 1 << 14);
            if (hits.Length > 0 && !hits.Any(h => h.collider.gameObject.layer == 14))
            {
                return true;
            }
            return false;
        }

        //Call Whenever a nail is nailed completly in. Returns true only if the Plank is fully fixed. Needs position of the board and position of all full nailed in and not broken nails.
        public bool IsPlankFixed(RectanglePos position, List<Vector2> nailPositions)
        {
            // Hannah: Yes, use for call BoardStatus.AddFixedPlanks
            return false;
        }

        //Hannah: Changed, returns RectanglePos of the board
        public RectanglePos GetBoardPos()
        {
            return boardPos;
        }

        //used for evaluating points, you do not need this
        public float CalcHoleCoverage()
        {
            return 0;
        }
        #endregion
    }

    public struct RectanglePos{
        public Vector2 leftBottom;
        public Vector2 rightTop;

        public RectanglePos(Vector2 _leftBottom, Vector2 _rightTop)
        {
            leftBottom = _leftBottom;
            rightTop = _rightTop;
        }
    }
}
