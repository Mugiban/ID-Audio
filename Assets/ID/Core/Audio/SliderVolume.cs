using UnityEngine;
using UnityEngine.UI;

namespace ID.Runtime.Audio
{
    public class SliderVolume : MonoBehaviour
    {
        public string type = "master";

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnVolumeChange);
        }
        private void Start()
        {
            SetVolume();
        }
        
        private void SetVolume()
        {
            switch (type)
            {
                case "master":
                    _slider.value = AudioManager.MasterVolume;
                    break;
                case "music":
                    _slider.value = AudioManager.MusicVolume;
                    break;
                case "sfx":
                    _slider.value = AudioManager.SFXVolume;
                    break;
                case "ui":
                    _slider.value = AudioManager.UIVolume;
                    break;
            }
            
        }

        void OnVolumeChange(float value)
        {
            switch (type)
            {
                case "master":
                    AudioManager.Instance.SetMasterVolume(value);
                    break;
                case "music":
                    AudioManager.Instance.SetMusicVolume(value);
                    break;
                case "sfx":
                    AudioManager.Instance.SetSFXVolume(value);
                    break;
                case "ui":
                    AudioManager.Instance.SetUIVolume(value);
                    break;
            }
        }
    }
}
