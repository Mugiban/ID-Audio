using System.Collections.Generic;
using ID.Runtime.Utilities.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace ID.Runtime.Audio
{
    [CreateAssetMenu(menuName = "ID Audio/Sound", fileName = "Sound")]
    [HideMonoScript]
    public class Sound : ScriptableObject
    {
        [PropertySpace(10)]
        [TitleGroup("Sound", GroupName = "$name", Alignment = TitleAlignments.Centered)]
        [BoxGroup("Sound/Clips", CenterLabel = true)]
        [ListDrawerSettings(DraggableItems = true, Expanded = true)]
        [LabelText("AudioClips")]
        public List<AudioClip> clips;
        
        
        
        [PropertySpace(10)]
        [BoxGroup("Sound/Box", LabelText = "Settings", CenterLabel = true)]
        [PropertyRange(0f, 1f)] public float volume = .5f;
        
        [PropertySpace(1)]
        [ToggleLeft]
        [BoxGroup("Sound/Box")]
        public bool randomVolume = true;
        
        [PropertySpace(1)]
        [ShowIf("randomVolume")]
        [BoxGroup("Sound/Box")]
        [MinMaxSlider(0f,1f, true)] 
        public Vector2 randomVolumeMinMax = new Vector2(.4f, .6f);
        
        
        [PropertySpace(10)]
        [BoxGroup("Sound/Box")]
        [SerializeField] [PropertyRange(0, 2f)] public float pitch = 1f;
        
        [PropertySpace(1)]
        [ToggleLeft]
        [BoxGroup("Sound/Box")]
        public bool randomPitch = true;
        
        [PropertySpace(1)]
        [BoxGroup("Sound/Box")]
        [ShowIf("randomPitch")]
        [MinMaxSlider(.5f,1.5f, true)]
        public Vector2 randomPitchMinMax = new Vector2(.9f, 1.1f);
        
        
        [FormerlySerializedAs("audioMixer")]
        [PropertySpace(10)]
        [BoxGroup("Sound/Box")]
        [LabelText("Mixer Group")]
        public AudioMixerGroup audioMixerGroup;
        
        [PropertySpace(1)]
        [BoxGroup("Sound/Box")]
        [PropertyRange(0, 1f)] public float spatialBlend;
        
        [ToggleLeft]
        [PropertySpace(1)]
        [BoxGroup("Sound/Box")]
        public bool loop;

        
        
#if UNITY_EDITOR
        private AudioSource _previewAudioSource;
        [HorizontalGroup("Sound/Box/Preview")]
        [Button("Play")]
        public void PreviewSound()
        {
            
            if (_previewAudioSource && _previewAudioSource.isPlaying) return;
            _previewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("AudioSource", 
                HideFlags.HideAndDontSave, typeof(AudioSource)).AddComponent<AudioSource>();
            _previewAudioSource.clip = clips.Random();
            if (randomVolume)
            {
                _previewAudioSource.volume = Random.Range(randomVolumeMinMax.x, randomVolumeMinMax.y);
            }
            else
            {
                _previewAudioSource.volume = volume;
            }

            if (randomPitch)
            {
                _previewAudioSource.pitch = Random.Range(randomPitchMinMax.x, randomPitchMinMax.y);
            }
            else
            {
                _previewAudioSource.pitch = pitch;
            }
            _previewAudioSource.outputAudioMixerGroup = audioMixerGroup;
            _previewAudioSource.spatialBlend = spatialBlend;
            _previewAudioSource.loop = loop;
            _previewAudioSource.Play();
        }
        
        [HorizontalGroup("Sound/Box/Preview")]
        [Button("Stop")]
        public void StopPreview()
        {
            if (_previewAudioSource != null)
            {
                _previewAudioSource.Stop();
            }
        }
#endif
    }
}