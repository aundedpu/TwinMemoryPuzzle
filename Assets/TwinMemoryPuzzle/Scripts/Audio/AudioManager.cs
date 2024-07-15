using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioSource bgSound;
        public AudioClip[] fxSounds;
        private AudioSource fxSource;
    
        void Awake()
        { 
            instance = this;
            fxSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayFxSound(int soundIndex)
        {
            if (soundIndex < fxSounds.Length)
            {
                AudioSource tempSource = gameObject.AddComponent<AudioSource>();
                tempSource.clip = fxSounds[soundIndex];
                
                tempSource.Play();
                
                Destroy(tempSource, fxSounds[soundIndex].length);
            }
        }

        public void PlayBgSound()
        {
            // play the background sound
            bgSound.Play();
        }

        public void StopBgSound()
        {
            // stop the background sound
            bgSound.Stop();
        }
    }
}