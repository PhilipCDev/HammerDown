using UnityEngine;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        public bool IsOnBoard(Vector2 position)
        {
            return true;
        }
        public Vector2 GetSize()
        {
            return Vector2.one;
        }
    }
}
