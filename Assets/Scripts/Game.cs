using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HammerDown.Map;

namespace HammerDown
{
    public class Game : MonoBehaviour
    {
        public static Game instance;

        public Board board;

        private int _score;
        public int Score { get { return _score; } set { _score = value; } }

        public delegate void GameEvent();
        public GameEvent GameStart { private set; get; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameStart?.Invoke();
        }

    }
}
