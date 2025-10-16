using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Manager
{
    public enum VideoResourceType
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
            switch (type) { 
            case VideoResourceType.Sample:
                    videoClip = LoadResource<VideoClip>("VideoClip/SampleVideo");
                    break;
            }
            return videoClip;
        }
    }
}

