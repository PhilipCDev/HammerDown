using HammerDown.GameTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HammerDown.UI {
    public class Clock : MonoBehaviour
    {
        public Text display;
        public TimeManager manager;

        public void Start()
        {
            Game.instance.GameStart += () => { gameObject.SetActive(true); };
            Game.instance.GameOver += () => { gameObject.SetActive(false); };
        }

        public void Update()
        {
            display.text = "" + ((int)manager.time);
        }
    }
}