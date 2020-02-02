using HammerDown.Map.Components;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        //Hannah: Move to Game
        public BoardStatus boardStatus;
        
        //List of all holes, just use and not change entries! Currently holes are not implemented
        public List<Hole> holes;

        #region public Functions
        //Bool function. Give a point and returns if it is on a board. Check for Nail (Nail can only be nailed in if OnBoard is true)
        //Hannah: Change structure 2D -> 3D
        public bool IsOnBoard(Transform obj)
        {
            RaycastHit[] hits = Physics.RaycastAll(obj.transform.position - obj.transform.forward * 0.5f, obj.transform.transform.forward, 1, 1 << 12 | 1 << 14);
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
