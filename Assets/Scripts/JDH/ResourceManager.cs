using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Manager
{
    public enum VideoResourceType
    {
        Sample,
    }
    public enum SoundBGMResourceType
    {
        Sample,
    }
    public enum SoundSFXResourceType
    {
        Sample,
    }
    public class ResourceManager : Singleton<ResourceManager>
    {
        private static Dictionary<string, Object> _cache = new();
        private static T LoadResource<T>(string path) where T : Object
        {
            if (_cache.ContainsKey(path)) return _cache[path] as T;
            var obj = Resources.Load<T>(path);
            if (obj != null) _cache[path] = obj;
            return obj;
        }

        public VideoClip LoadVideoClip(VideoResourceType type)
        {
            VideoClip videoClip = null;
            //string path = "VideoClip/";
            switch (type) { 
            case VideoResourceType.Sample:
                    videoClip = LoadResource<VideoClip>("VideoClip/SampleVideo");
                    break;
            }
            return videoClip;
        }

        public AudioSource LoadAudioBGM(SoundBGMResourceType type)
        {
            AudioSource audioSource = null;
            //string path = "VideoClip/";
            switch (type)
            {
                case SoundBGMResourceType.Sample:
                    audioSource = LoadResource<AudioSource>("BGM/SampleBGM");
                    break;
            }
            return audioSource;
        }

        public AudioSource LoadAudioSFX(SoundSFXResourceType type)
        {
            AudioSource audioSource = null;
            //string path = "VideoClip/";
            switch (type)
            {
                case SoundSFXResourceType.Sample:
                    audioSource = LoadResource<AudioSource>("SFX/SampleBGM");
                    break;
            }
            return audioSource;
        }
    }
}

