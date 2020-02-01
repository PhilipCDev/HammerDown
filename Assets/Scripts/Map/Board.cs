using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        public bool IsOnBoard(Vector2 position)
        {
            return true;
        }

        public bool IsCovered(RectanglePos position)
        {
            return true;
        }

        public bool IsPlankFixed(RectanglePos position, List<Vector2> nailPositions)
        {
            return false;
        }

        public Vector2 GetSize()
        {
            return Vector2.one;
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
