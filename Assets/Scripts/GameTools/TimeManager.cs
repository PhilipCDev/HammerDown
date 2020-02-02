using HammerDown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.GameTools {
    public class TimeManager : MonoBehaviour
    {
        public float time;
        private bool isPaused;

        void Start()
        {
            Game.instance.GameStart += () => { time = Game.instance.board.maxTime; };
            Game.instance.Pause += () => { isPaused = true; };
            Game.instance.Continue += () => { isPaused = false; };
        }

        private void Update()
        {
            if(time != 0)
            {
                time = Mathf.Clamp(time - Time.deltaTime, 0, int.MaxValue);
                if(time == 0)
                {
                    Game.instance.GameOver();
                }
            }
        }
    }
}