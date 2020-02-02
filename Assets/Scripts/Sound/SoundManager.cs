using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Sound
{
    public enum SoundEffects {HAMMER, HURT, ANIMALDIE, NAILBROKE, SUCCESS};
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager current;
        public AudioClip[] soundEffects;

        public AudioClip menuMusic;
        public AudioClip musicIngame;

        public AudioSource cameraEar;

        void Start()
        {
            current = this;
        }

        public static void PlaySound(SoundEffects effect)
        {
            AudioSource.PlayClipAtPoint(current.soundEffects[(int)effect], Camera.main.transform.position);
        }

        public static void SetMusic(bool menu)
        {
//            current.cameraEar.
        }
    }

}
