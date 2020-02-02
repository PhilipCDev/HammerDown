using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Map;

namespace HammerDown
{
    public class TestLevelLoader : MonoBehaviour
    {
        public Board board;
        public BoardStatus status;

        private void Awake()
        {
            Game.instance.RegisterNewBoard(board, status);
        }
    }
}
