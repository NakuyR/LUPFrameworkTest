using Manager;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Manager
{
    public class IntroStage : BaseStage
    {

        public VideoPlayer videoplayer;
        public VideoClip clip;
        public Slider videoVolume;
        void SetVideoClip(VideoClip clip)
        {
            videoplayer.clip = clip;
        }

        private void Start()
        {
            onStageEnter();
        }

        private void Update()
        {
            videoVolume.onValueChanged.AddListener(SetVideoVolume);
        }

        public override void onStageEnter()
        {
            base.onStageEnter();

            LoadResource();
        }
        void LoadResource()
        {
            clip = ResourceManager.Instance.LoadVideoClip(VideoResourceType.Sample);

            OnResourceLoaded();
        }

        void OnResourceLoaded()
        {
            SetVideoClip(clip);
            videoplayer.Play();
        }

        void SetVideoVolume(float value)
        {
            Debug.LogFormat("VideoVolume : {0}", value);
            videoplayer.SetDirectAudioVolume(0,value);
        }
    }
}

