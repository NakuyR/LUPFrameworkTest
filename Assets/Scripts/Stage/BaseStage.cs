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
        //private List<RuntimeData> datas;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //@TODO 인자값 int로 수정
        public void LoadStage(int stage)
        {
            StageKind endStageKind = StageKind;
            switch (stage)
            {
                case 1:
                    endStageKind = StageKind.Debug;
                    break;
                case 2:
                    endStageKind = StageKind.Main;
                    break;
                case 3:
                    endStageKind = StageKind.Intro;
                    break;
                case 4:
                    endStageKind = StageKind.Roguelike;
                    break;
                case 5:
                    endStageKind = StageKind.Shooting;
                    break;
                case 6:
                    endStageKind = StageKind.ExtractionShooter;
                    break;
                case 7:
                    endStageKind = StageKind.Production;
                    break;
                case 8:
                    endStageKind = StageKind.DeckStrategy;
                    break;
            }
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

        //RuntimeData GetData(RuntimeDataKind data)
        protected virtual void GetData()
        {
            //RuntimeData data = DataManager.Instance.GetRuntimeData();
            //datas.Add(data);
            //return data;
        }

        private void SaveDatas()
        {
            //foreach(RuntimeData data in datas)
            //{
            //    DataManager.Instance.SaveData(data);
            //}
        }
    }
}

