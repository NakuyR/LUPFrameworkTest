using Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
namespace Manager
{
    public class ShootingStage : BaseStage
    {
        protected override void Awake() 
        {
            base.Awake();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override IEnumerator OnStageEnter()
        {
            base.OnStageEnter();
            StageKind = StageKind.Shooting;
            //구현부


            return null;
        }
        public override IEnumerator OnStageStay()
        {
            base.OnStageStay();
            //일단 납두기
            return null;
        }
        public override IEnumerator OnStageExit()
        {
            base.OnStageExit();
            //구현부


            return null;
        }
        protected override void LoadResources()
        {
            //resource = ResourceManager.Instance.Load...
        }

        protected override void GetDatas()
        {
            //data = GetData...
        }
    }
}

