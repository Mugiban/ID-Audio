using DG.Tweening;
using UnityEngine;

namespace ID.Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ExtendedAudioSource : MonoBehaviour
    {
        private AudioSource _source;
        public Sound _currentSound;

        public bool isPlaying;

        public bool isPaused;
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }
        public void Update()
        {
            if (isPaused) return;
            
            if (_source.isPlaying == false)
            {
                //notifico del audio que se va a acabar
                AudioEvents.AudioEndPlaying(_currentSound);
                //reseteo el extended audio source para que no haya problemas en el siguiente pool
                Reset();
                //lo devuelvo al pool
                AudioPool.Return(this);
            }
        }

        private void Reset()
        {
            _currentSound = null;
            isPlaying = false;
            isPaused = false;
            transform.position = Vector3.zero;
        }

        public void Setup(Sound newSound)
        {
            if (newSound.clips.Count == 0) return;
            _currentSound = newSound;
            var currentClip = newSound.clips[Random.Range(0, newSound.clips.Count)];
            _source.clip = currentClip;
            if (newSound.randomVolume)
            {
                _source.volume = Random.Range(newSound.randomVolumeMinMax.x, newSound.randomVolumeMinMax.y);
            }
            else
            {
                _source.volume = newSound.volume;
            }

            if (newSound.randomPitch)
            {
                _source.pitch = Random.Range(newSound.randomPitchMinMax.x, newSound.randomPitchMinMax.y);
            }
            else
            {
                _source.pitch = newSound.pitch;
            }
            _source.outputAudioMixerGroup = newSound.audioMixerGroup;
            _source.spatialBlend = newSound.spatialBlend;
            _source.loop = newSound.loop;
            isPlaying = true;
        }

        public void Play()
        {
            _source.Play();
        }

        public void PlayClipAtPoint(Vector3 position)
        {
            AudioSource.PlayClipAtPoint(_source.clip, position);
        }

        public void PlayOnce()
        {
            _source.PlayOneShot(_source.clip);
        }
        public void PlayDelayed(float delay)
        {
            _source.PlayDelayed(delay);
        }
        
        public void Pause()
        {
            if (isPaused) return;
            isPaused = true;
            _source.Pause();
        }

        public void Resume()
        {
            if (isPaused == false) return;
            isPaused = false;
            _source.UnPause();
        }

        public void Stop()
        {
            _source.Stop();
            Reset();
        }

        public void FadeIn(float duration)
        {
            _source.Play();
            _source.volume = 0;
            _source.DOFade( _currentSound.volume, duration).SetEase(Ease.Linear);
        }
        
        private void StopFade()
        {
            Stop();
            _source.volume = 0.0001f;
        }
        public void FadeOut(float duration)
        {
            _source.DOFade(0, duration).OnComplete(StopFade).SetEase(Ease.Linear);
        }
        
    }
}