using System;
using UnityEngine;
using ID.Runtime.Audio;
using UnityEngine.UI;

namespace ID
{
    public class SoundConsumer : MonoBehaviour
    {
        public bool doCrossFade;

        public bool doPlay;

        public Button playButton;

        public Button stopButton;

        public Sound soundToPlay;

        public Sound soundToFinish;

        private void Awake()
        {
            playButton.onClick.AddListener(PlaySound);
            stopButton.onClick.AddListener(StopSound);
        }

        void PlaySound()
        {
            if (doCrossFade)
            {
                AudioManager.CrossFade(soundToPlay, soundToFinish, 5f, 5f);
            }

            if (doPlay)
            {
                AudioManager.Play(soundToFinish);
            }
        }

        void StopSound()
        {
            if (doCrossFade)
            {
                AudioManager.FadeOut(soundToPlay, 2f);
            }

            if (doPlay)
            {
                AudioManager.Stop(soundToFinish);
            }
        }
    }
}
