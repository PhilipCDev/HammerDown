using HammerDown.Map;
using HammerDown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HammerDown.GameTools
{
    public class LevelManager : MonoBehaviour
    {
        public List<Board> boards;
        public List<BoardStatus> statuses;
        public int nextLevel;

        public void Play()
        {
            Game.instance.RegisterNewBoard(boards[nextLevel], statuses[nextLevel]);

            Game.instance.hand.transform.position = Game.instance.board.HandPos.position;
            Game.instance.hand.transform.rotation = Game.instance.board.HandPos.rotation;
            Game.instance.hammer.transform.position = Game.instance.board.HammerPos.position;
            Game.instance.hammer.transform.position = Game.instance.board.HammerPos.position;

            Game.instance.boardStatus.StartChecking();
            Game.instance.GameStart();
        }
    }
}