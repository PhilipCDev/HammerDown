using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Map;
using HammerDown.Player;

namespace HammerDown
{
    public class Game 
    {
        public static Game instance = new Game();

        public Hammer hammer { private set; get; }
        public Hand hand { private set; get; }
        public Board board { private set; get; }
        public BoardStatus boardStatus { private set; get; }

        public int score;

        public delegate void GameEvent();
        public GameEvent GameStart = () => { };
        public GameEvent GameOver = () => { };
        public GameEvent GetPoints = () => { };
        public GameEvent Pause = () => { };
        public GameEvent Continue = () => { };

        public void RegisterHammer(Hammer hammer)
        {
            if (this.hammer != null)
                Debug.Log("New Hammer registered!");

            this.hammer = hammer;
        }

        public void RegisterHand(Hand hand)
        {
            if (this.hand != null)
                Debug.Log("New Hand registered!");

            this.hand = hand;
        }

        public void RegisterNewBoard(Board newBoard, BoardStatus newStatus)
        {
            //Old Board remove
            board = newBoard;
            boardStatus = newStatus;
        }
    }
}
