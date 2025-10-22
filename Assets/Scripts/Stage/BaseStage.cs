<<<<<<< Updated upstream
﻿using UnityEngine;
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
>>>>>>> Stashed changes
using UnityEngine.SceneManagement;

namespace Manager
{
    public abstract class BaseStage : MonoBehaviour
    {
        public StageKind StageKind = StageKind.Main;
<<<<<<< Updated upstream
        // Start is called once before the first execution of Update after the MonoBehaviour is created
=======

        public BaseStaticData staticData;
       
>>>>>>> Stashed changes
        protected virtual void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //@TODO 인자값 int로 수정
        public virtual void LoadStage(string stage)
        {
            StageKind endStageKind = StageKind;
            switch (stage)
            {
                case "Debug":
                    endStageKind = StageKind.Debug;
                    break;
                case "Main":
                    endStageKind = StageKind.Main;
                    break;
                case "Intro":
                    endStageKind = StageKind.Intro;
                    break;
                case "Roguelike":
                    endStageKind = StageKind.Roguelike;
                    break;
                case "Shooting":
                    endStageKind = StageKind.Shooting;
                    break;
                case "ExtractionShooter":
                    endStageKind = StageKind.ExtractionShooter;
                    break;
                case "Build":
                    endStageKind = StageKind.Production;
                    break;
                case "SLG":
                    endStageKind = StageKind.DeckStrategy;
                    break;
            }
            StageManager.Instance.LoadStage(endStageKind);
        }

<<<<<<< Updated upstream
        public virtual void OnStageEnter()
        {
=======
        protected BaseStaticData GetDatas(BaseStage stage)
        {
            BaseStaticData staticData = DataManager.Instance.GetDatas(stage);

            return staticData;
        }

        public virtual IEnumerator OnStageEnter()
        {
            LoadResources();
            staticData = GetDatas(this);
            yield return null;
>>>>>>> Stashed changes
        }
        public virtual void OnStageStay()
        {
        }

        public virtual void OnStageExit()
        {
        }
    }
}

