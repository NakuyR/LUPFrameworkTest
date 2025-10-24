using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Manager
{
    public abstract class BaseStage : MonoBehaviour
    {
        public StageKind StageKind = StageKind.Main;

        protected virtual void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /*
        UnKnown = 0,    // 이상한 씬
        Debug = 1,      // 디버그 씬 (개발용)
        Main = 2,       // 메인 화면
        Intro = 3,      // 인트로
        Roguelike = 4,  // 로그라이크
        Shooting = 5,   // 슈팅
        ExtractionShooter = 6, // 익스트랙션 슈터
        Production = 7,  // 생산/건설/강화
        DeckStrategy = 8, // 덱 전략
         */
        public void LoadStage(int stage)
        {
            StageKind endStageKind = (StageKind)stage;
            StageManager.Instance.LoadStage(endStageKind);
        }
        protected abstract void LoadResources();

        protected abstract void GetDatas();

        public virtual IEnumerator OnStageEnter()
        {
            LoadResources();
            GetDatas();
            yield return null;
        }
        public virtual IEnumerator OnStageStay()
        {
            yield return null;
        }

        public virtual IEnumerator OnStageExit()
        {
            SaveDatas();
            yield return null;
        }

        private void SaveDatas()
        {
            //foreach(RuntimeData data in datas)
            //{
            //    DataManager.Instance.SaveData(data);
            //}
        }

        protected BaseStaticData GetStaticData(BaseStage stage)
        {
            BaseStaticData data = null;

            data = Manager.DataManager.Instance.GetStaticData(stage);

            return data;
        }

        protected BaseStaticData GetRuntimeData(BaseStage stage)
        {
            // 추후 구현

            return null;
        }
    }
}

