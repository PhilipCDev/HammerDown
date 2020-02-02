using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HammerDown.Sound
{
    public enum SoundEffects {HAMMER, HURT, ANIMALDIE, NAILBROKE, SUCCESS};
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager current;
        public AudioClip[] soundEffects;
        public AudioClip[] hammerSounds;
        public AudioClip[] hurtSounds;
        public AudioClip[] animalDieSounds;
        public AudioClip[] nailBrokeSounds;
        public AudioClip[] successSounds;


        public AudioClip menuMusic;
        public AudioClip musicIngame;

        public AudioSource cameraEar;
        public float musicVolume;

        void Start()
        {
            current = this;
            cameraEar.clip = menuMusic;
            
        }

        public static void PlaySound(SoundEffects effect)
        {
            AudioSource.PlayClipAtPoint(current.soundEffects[(int)effect], Camera.main.transform.position);
            var index = 0;
            switch (effect)
            {
                case SoundEffects.HAMMER:
                    index = Random.Range(0, current.hammerSounds.Length - 1);
                    AudioSource.PlayClipAtPoint(current.hammerSounds[index], Camera.main.transform.position);
                    break;
                case SoundEffects.HURT:
                    index = Random.Range(0, current.hurtSounds.Length - 1);
                    AudioSource.PlayClipAtPoint(current.hurtSounds[index], Camera.main.transform.position);
                    break;
                case SoundEffects.ANIMALDIE:
                    index = Random.Range(0, current.animalDieSounds.Length - 1);
                    AudioSource.PlayClipAtPoint(current.animalDieSounds[index], Camera.main.transform.position);
                    break;
                case SoundEffects.NAILBROKE:
                    index = Random.Range(0, current.nailBrokeSounds.Length - 1);
                    AudioSource.PlayClipAtPoint(current.nailBrokeSounds[index], Camera.main.transform.position);
                    break;
                case SoundEffects.SUCCESS:
                    index = Random.Range(0, current.successSounds.Length - 1);
                    AudioSource.PlayClipAtPoint(current.successSounds[index], Camera.main.transform.position);
                    break;
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

        private IEnumerator Play()
        {
            yield return new WaitForSeconds(cameraEar.clip.length);
            cameraEar.clip = musicIngame;
        }
    }

}
