using Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Data;
namespace Manager
{
    public class MainStage : BaseStage
    {

        public AudioSource SFX;
        public AudioSource BGM;
        public float soundVolume= 0;
        public Slider slider;
    
        protected override void Awake() 
        {
            base.Awake();
            StageKind = StageKind.Main;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            slider.onValueChanged.AddListener(SetAudioVolume);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override IEnumerator OnStageEnter()
        {
            yield return base.OnStageEnter();
            
            //구현부


            yield return null;
        }
        public override IEnumerator OnStageStay()
        {
            yield return base.OnStageStay();
            //일단 납두기
            yield return null;
        }
        public override IEnumerator OnStageExit()
        {
            yield return base.OnStageExit();
            //구현부


            yield return null;
        }
        protected override void LoadResources()
        {
            //resource = ResourceManager.Instance.Load...
        }

        protected override void GetDatas()
        {
            
        }

        void SetAudioVolume(float value)
        {
            Debug.LogFormat("VideoVolume : {0}", value);
            SFX.volume = slider.value;
            BGM.volume = slider.value;
        }
    }
}

