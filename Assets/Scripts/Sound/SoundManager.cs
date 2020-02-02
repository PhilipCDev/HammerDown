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
        public float musicVolume;

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

        public void SwitchMusic(bool menu)
        {
            StopAllCoroutines();
            if (menu)
            {
                StartCoroutine(SwitchMusic(menuMusic, cameraEar));
            }
            else
            {
                StartCoroutine(SwitchMusic(musicIngame, cameraEar));
            }
        }
        public IEnumerator SwitchMusic(AudioClip music, AudioSource source)
        {
            for(float f = 0; f<1; f+= Time.deltaTime)
            {
                yield return new WaitForEndOfFrame();
                source.volume = (1-f) * musicVolume;
            }
            source.volume = 0;
            source.clip = music;
            for (float f = 0; f < 1; f += Time.deltaTime)
            {
                yield return new WaitForEndOfFrame();
                source.volume = f * musicVolume;
            }
        }
    }

}
