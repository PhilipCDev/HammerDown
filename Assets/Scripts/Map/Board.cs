using HammerDown.Map.Components;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {

        // !!!! Added this here, maybe wrong but needed it somewhere
        public BoardStatus boardStatus;
        
        //List of all holes, just use and not change entries! Currently holes are not implemented
        public List<Hole> holes;

        //Bool function. Give a point and returns if it is on a board. Check for Nail (Nail can only be nailed in if OnBoard is true)
        public bool IsOnBoard(Vector2 position)
        {
            return true;
        }

        //Call Whenever a nail is nailed completly in. Returns true only if the Plank is fully fixed. Needs position of the board and position of all full nailed in and not broken nails.
        public bool IsPlankFixed(RectanglePos position, List<Vector2> nailPositions)
        {
            // TODO hab ich gecallt, soll ich was mit dem return machen?
            return false;
        }

        //returns size of the board
        public Vector2 GetSize()
        {
            return Vector2.one;
        }

        //used for evaluating points, you do not need this
        public float CalcHoleCoverage()
        {
            return 0;
        }
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
