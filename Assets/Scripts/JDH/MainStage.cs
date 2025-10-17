using Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;
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

        public override void LoadStage(string stage)
        {
            base.LoadStage(stage);
        }

        void SetAudioVolume(float value)
        {
            Debug.LogFormat("VideoVolume : {0}", value);
            SFX.volume = slider.value;
            BGM.volume = slider.value;
        }
    }
}

