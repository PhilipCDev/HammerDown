using UnityEngine;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        private void Start()
        {
            //TODO register when level is changed
            Game.instance.RegisterNewBoard(this);
        }

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
