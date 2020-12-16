using ID.Runtime.Utilities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace ID.Runtime.Audio
{
    [HideMonoScript]
    public class  AudioManager : Singleton<AudioManager>
    {
        [TitleGroup("AudioManager", Alignment = TitleAlignments.Centered)]
        
        [TabGroup("AudioManager/Tab", "General")] [BoxGroup("AudioManager/Tab/General/Volume", CenterLabel = true)] [Range(0,1)] public float masterVolume = 1f;
        
        [TabGroup("AudioManager/Tab", "General")] [BoxGroup("AudioManager/Tab/General/Volume")] [Range(0,1)] public float musicVolume = 1f;
        [TabGroup("AudioManager/Tab", "General")] [BoxGroup("AudioManager/Tab/General/Volume")] [Range(0,1)] public float sfxVolume = 1f;
        [TabGroup("AudioManager/Tab", "General")] [BoxGroup("AudioManager/Tab/General/Volume")] [Range(0,1)] public float uiVolume = 1f;

        public static float MusicVolume => Instance.musicVolume;
        public static float MasterVolume => Instance.masterVolume;
        public static float SFXVolume => Instance.sfxVolume;
        public static float UIVolume => Instance.uiVolume;
        
        private const float VolumeThreshold = -80f;
        
        private static string MasterVol = "masterVol";
        private static string MusicVol = "musicVol";
        private static string SfxVol = "sfxVol";
        private static string UiVol = "uiVol";
        
        [TabGroup("AudioManager/Tab", "General")] [BoxGroup("AudioManager/Tab/General/AudioPool", LabelText = "Audio Pool", CenterLabel = true)] 
        [Range(5, 60f)] public int maxPoolInstances = 15;
        
        [TabGroup("AudioManager/Tab", "Audio Mixer")]
        [ShowInInspector]
        public AudioMixer masterMixer;

        private static AudioPool AudioPool;

        private static bool IsInit;
        private void Start()
        {
            if(IsInit == false) Init();
        }
        
        private static void Init()
        {
            AudioPool = new AudioPool(Instance.transform, Instance.maxPoolInstances);
            if (Instance.masterMixer == null)
            {
                Instance.masterMixer =
                    AssetDatabase.LoadAssetAtPath<AudioMixer>("Assets/ID/Core/Audio/MasterMixer.mixer");
            }
            IsInit = true;
        }

        public static bool IsPlaying(Sound currentSound)
        {
            if (IsInit == false) Init();
            var isPlaying = AudioPool.IsPlaying(currentSound);
            return isPlaying;
        }
        
        public static ExtendedAudioSource GetAudioSource()
        {
            if (IsInit == false) Init();
            return AudioPool.GetAudioSource();
        }
        

        public static void Play(Sound sound)
        {
            //si el audio se está reproduciendo, lo para y lo vuelve a empezar
            if (IsPlaying(sound))
            {
                Stop(sound);
            }
            
            var source = GetAudioSource();
            
            source.Setup(sound);
            
            source.Play();
        }
        
        public static void PlaySoundAtPoint(Sound sound, Vector3 soundPosition)
        {
            //si el audio se está reproduciendo, lo para y lo vuelve a empezar
            if (IsPlaying(sound))
            {
                Stop(sound);
            }
            
            var source = GetAudioSource();
            source.Setup(sound);
            source.PlayClipAtPoint(soundPosition);
        }

        public static void PlayOnce(Sound sound)
        {
            var  source = GetAudioSource();
            
            source.Setup(sound);
            source.PlayOnce();
        }

        public static void PlayDelayed(Sound sound, float delay)
        {
            var source = GetAudioSource();
            
            source.Setup(sound);
            source.PlayDelayed(delay);
        }

        public static void FadeIn(Sound sound, float duration = 1f)
        {           
            if (IsPlaying(sound))
            {
                Stop(sound);
            }
            var source = GetAudioSource();
            
            source.Setup(sound);
            source.FadeIn(duration);
        }

        public static void FadeOut(Sound sound, float duration = 1f)
        {
            ExtendedAudioSource source = AudioPool.IsPlaying(sound);
            if (source != null)
            {
                source.FadeOut(duration);
            }
        }


        public static void CrossFade(Sound soundToStart, Sound sountToFinish, float fadeInDuration,
            float fadeOutDuration)
        {
            FadeOut(sountToFinish, fadeOutDuration);
            FadeIn(soundToStart, fadeInDuration);
        }

        /// <summary>
        /// returns true if the audio is paused, false otherwise
        /// </summary>
        /// <param name="sound"></param>
        public static bool TogglePause(Sound sound)
        {

            if (IsPlaying(sound) == false) return false;
            var extendedAudioSource = AudioPool.IsPlaying(sound);
            if (!extendedAudioSource) return false;
            if (extendedAudioSource)
            {
                if (extendedAudioSource.isPaused)
                {
                    Resume(sound);
                    return false;
                }
                
                Pause(sound);
                return true;
            }
            Debug.LogError("No ha podido pausar el audio, no debería pasar.");
            return false;
        }

        public static void Pause(Sound sound)
        {
            var extendedAudioSource = AudioPool.IsPlaying(sound);
            if (extendedAudioSource)
            {
                var source = GetAudioSource();
                source.Pause();
            }
        }

        public static void Resume(Sound sound)
        {
            var extendedAudioSource = AudioPool.IsPlaying(sound);
            if (extendedAudioSource)
            {
                var source = GetAudioSource();
                source.Resume();
            }
        }

        public static void Stop(Sound currentSound)
        {
            var extendedAudioSource = AudioPool.IsPlaying(currentSound);
            if (extendedAudioSource)
            {
                
                var source = extendedAudioSource;
                
                source.Stop();
            }
        }

        public static void StopAll()
        {
            var extendedAudioSources = AudioPool.ActiveAudioSources;
            foreach (var extendedAudioSource in extendedAudioSources)
            {
                extendedAudioSource.Stop();
            }
        }

        public void SetMasterVolume(float newMasterVolume)
        {
                        
            if(IsInit == false) Init();
            masterVolume = newMasterVolume;
            if (masterVolume <= 0)
            {
                masterMixer.SetFloat(MasterVol, VolumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(masterVolume);
                masterMixer.SetFloat(MasterVol, value);
            }
        }

        public void SetMusicVolume(float newMusicVolume)
        {
            if(IsInit == false) Init();
            musicVolume = newMusicVolume;
            if (musicVolume <= 0)
            {
                masterMixer.SetFloat(MusicVol, VolumeThreshold);
            }
            else
            {
                float value = 20f * Mathf.Log10(musicVolume);
                masterMixer.SetFloat(MusicVol, value);
            }
        }

        public void SetSFXVolume(float newSfxVolume)
        {
            if(IsInit == false) Init();
            
            sfxVolume = newSfxVolume;
            if (sfxVolume <= 0)
            {
                masterMixer.SetFloat(SfxVol, VolumeThreshold);
            }
            else
            {
                float value = 20f * Mathf.Log10(sfxVolume);
                masterMixer.SetFloat(SfxVol, value);
            }
        }

        public void SetUIVolume(float newUiVolume)
        {
            if(IsInit == false) Init();
            uiVolume = newUiVolume;
            if (uiVolume <= 0)
            {
                masterMixer.SetFloat(UiVol, VolumeThreshold);
            }
            else
            {
                float value = 20f * Mathf.Log10(uiVolume);
                masterMixer.SetFloat(UiVol, value);
            }
        }


    }
}