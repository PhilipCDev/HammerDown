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
        public Transform cameraMainPos;
        private float timer;
        // Start is called before the first frame update
        void Start()
        {
            Game.instance.GameStart += delegate { gameObject.SetActive(false); };
            Game.instance.GameOver += delegate { gameObject.SetActive(true);
                Camera.main.transform.position = cameraMainPos.position;
                Camera.main.transform.rotation = cameraMainPos.rotation;
            
            };
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
            Menu.SetActive(false);
            Debug.Log("Miep");
            StartCoroutine(goToHTP());
            anim.SetBool("goToHTP", true);
        }
        public void backToMenu()
        {
            HTP.SetActive(false);
            StartCoroutine(goToMenu());
            anim.SetBool("goToHTP", false);
        }

        public IEnumerator goToMenu()
        {
            yield return new WaitForSeconds(1f);
            Menu.SetActive(true);
        }

        public IEnumerator goToHTP()
        {
            yield return new WaitForSeconds(1f);
            HTP.SetActive(true);
        }

    }

    
}
