using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace HammerDown.UI
{

    public class ManuManager : MonoBehaviour
    {
        public Animator anim;
        public GameObject HTP;
        public GameObject Menu;
        // Start is called before the first frame update
        void Start()
        {
            HTP.SetActive(false);
            Menu.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Quit()
        {
            Application.Quit();
        }

        public void howToPlay()
        {
            HTP.SetActive(false);
            Menu.SetActive(false);
            anim.SetBool("goToHTP", true);
        }
        public void backToMenu()
        {
            HTP.SetActive(false);
            Menu.SetActive(true);
            anim.SetBool("goToHTP", false);
        }
    }

}
