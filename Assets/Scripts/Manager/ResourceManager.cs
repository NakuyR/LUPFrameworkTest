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
                    //videoClip = LoadResource<VideoClip>(path+"/SampleVideo");
                    videoClip = LoadResource<VideoClip>("VideoClip/SampleVideo");
                    break;
            }
            return videoClip;
        }

        public AudioClip LoadAudioBGM(SoundBGMResourceType type)
        {
            AudioClip audioClip = null;
            //string path = "VideoClip/";
            switch (type)
            {
                case SoundBGMResourceType.Sample:
                    audioClip = LoadResource<AudioClip>("BGM/SampleBGM");
                    break;
            }
            return audioClip;
        }

        public AudioClip LoadAudioSFX(SoundSFXResourceType type)
        {
            AudioClip audioClip = null;
            //string path = "VideoClip/";
            switch (type)
            {
                case SoundSFXResourceType.Sample:
                    audioClip = LoadResource<AudioClip>("SFX/SampleSFX");
                    break;
            }
            return audioClip;
        }

        public BaseStaticData LoadStaticData(StageKind type, int stagetype)
        {
            BaseStaticData data = null;
            switch (type)
            {
                case Manager.StageKind.Shooting:
                    data = Resources.Load<BaseStaticData>("Data/ShootingStaticData");
                    break;
                case Manager.StageKind.DeckStrategy:
                    data = Resources.Load<BaseStaticData>("Data/DeckStrategyStaticData");
                    break;
                case Manager.StageKind.ExtractionShooter:
                    data = Resources.Load<BaseStaticData>("Data/ExtractionShooterStaticData");
                    break;
                case Manager.StageKind.Roguelike:
                    data = Resources.Load<BaseStaticData>("Data/RoguelikeStaticData");
                    break;
                case Manager.StageKind.Main:
                    data = Resources.Load<BaseStaticData>("Data/ProductionStaticData");
                    break;
            }
            return data;
        }
    }
}

