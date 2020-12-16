using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ID.Runtime.Audio
{
    public class AudioPool
    {
        public static List<ExtendedAudioSource> AudioSources { get; private set; }
        public static List<ExtendedAudioSource> ActiveAudioSources { get; private set; }
        private static Transform Trans;
        
        public AudioPool(Transform parent, int maxPoolInstances)
        {
            InitializePool(parent, maxPoolInstances);
        }

        private void InitializePool(Transform parent, int maxPoolInstances)
        {
            Trans = parent;
            AudioSources = new List<ExtendedAudioSource>();
            ActiveAudioSources = new List<ExtendedAudioSource>();
            for (int i = 1; i < maxPoolInstances+1; i++)
            {
                CreateAudioInstance(i);
            }
        }
        private ExtendedAudioSource CreateAudioInstance(int index = 1)
        {
            var go = new GameObject("AudioInstance " + index, typeof(ExtendedAudioSource));
            var source = go.GetComponent<ExtendedAudioSource>();
            go.transform.SetParent(Trans);
            AudioSources.Add(source);
            go.SetActive(false);
            return source;
        }

        public ExtendedAudioSource GetAudioSource()
        {
            foreach (var source in AudioSources.Where(source => source.isPlaying == false))
            {
                source.gameObject.SetActive(true);
                ActiveAudioSources.Add(source);
                return source;
            }
            var newSource = CreateAudioInstance();
            newSource.gameObject.SetActive(true);
            return newSource;
        }

        public ExtendedAudioSource IsPlaying(Sound currentSound)
        {
            foreach (var extendedAudioSource in ActiveAudioSources)
            {
                if (extendedAudioSource._currentSound == currentSound)
                {
                    if (extendedAudioSource.isPlaying)
                    {
                        return extendedAudioSource;
                    }
                }
            }
            return null;
        }

        public static void Return(ExtendedAudioSource extendedAudioSource)
        {
            ActiveAudioSources.Remove(extendedAudioSource);
            extendedAudioSource.gameObject.SetActive(false);
        }
    }
}