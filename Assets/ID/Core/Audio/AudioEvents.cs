using System;

namespace ID.Runtime.Audio
{
    public static class AudioEvents
    {
        public static event Action<Sound> OnAudioEndPlaying;
        public static event Action<ExtendedAudioSource> OnPauseAudio;
        public static event Action<ExtendedAudioSource> OnResumeAudio;
        
        public static void AudioEndPlaying(Sound sound)
        {
            OnAudioEndPlaying?.Invoke(sound);
        }
        
        public static void PauseAudio(ExtendedAudioSource source)
        {
            OnPauseAudio?.Invoke(source);
        }

        public static void ResumeAudio(ExtendedAudioSource source)
        {
            OnResumeAudio?.Invoke(source);
        }
    }
}
