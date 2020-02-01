using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Map;

namespace HammerDown
{
    public class Game 
    {
        public static Game instance = new Game();

        public Board board;
        public int score;

        public delegate void GameEvent();
        public GameEvent GameStart { private set; get; }

        public void RegisterNewBoard(Board newBoard)
        {
            //Old Board remove
            board = newBoard;
        }
    }
}
