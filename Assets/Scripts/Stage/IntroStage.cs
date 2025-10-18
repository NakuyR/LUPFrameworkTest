﻿using Manager;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
        public AudioSource audioSource;
        void SetVideoClip(VideoClip clip)
        {
            videoplayer.clip = clip;
            videoplayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

            // 첫 번째 오디오 트랙을 활성화 (보통 0번이 기본 트랙)
            videoplayer.EnableAudioTrack(0, true);
            videoplayer.SetDirectAudioMute(0, false);

            // AudioSource 연결
            videoplayer.SetTargetAudioSource(0, audioSource);

            // 초기 볼륨 적용
            audioSource.volume = videoVolume.value;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            videoVolume.onValueChanged.AddListener(SetVideoVolume);
        }

        private void Update()
        {
            
        }

        public override IEnumerator OnStageEnter()
        {
            base.OnStageEnter();
            StageKind = StageKind.Intro;

            SetVideoClip(clip);
            videoplayer.Play();
            return null;
        }


        void SetVideoVolume(float value)
        {
            Debug.LogFormat("VideoVolume : {0}", value);
            videoplayer.SetDirectAudioVolume(0,value);
            audioSource.volume = videoVolume.value;
        }

        protected override void LoadResources()
        {
            clip = ResourceManager.Instance.LoadVideoClip(VideoResourceType.Sample);
            //resource = ResourceManager.Instance.Load...
        }

        protected override void GetDatas()
        {
            //data = DataManager.Instance.GetData...
        }
    }
}

