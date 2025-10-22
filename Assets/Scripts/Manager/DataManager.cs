using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Manager
{
    public class DataManager : Singleton<DataManager>
    {
        [SerializeField]
        BaseStaticData data;

        private BaseStaticData GetStaticData(BaseStage stage)
        {
            BaseStaticData data = null;

            switch (stage.StageKind)
            {
                case Manager.StageKind.Shooting:
                    data = Resources.Load<BaseStaticData>("Data/ShootingStaticData");
                    break;
                case Manager.StageKind.DeckStrategy:
                    data = Resources.Load<BaseStaticData>("Data/SLGStaticData");
                    break;
                case Manager.StageKind.ExtractionShooter:
                    data = Resources.Load<BaseStaticData>("Data/ExtractionShooterStaticData");
                    break;
                case Manager.StageKind.Roguelike:
                    data = Resources.Load<BaseStaticData>("Data/RoguelikeStaticData");
                    break;
                case Manager.StageKind.Main:
                    data = Resources.Load<BaseStaticData>("Data/BuildStaticData");
                    break;
            }

            if (!data)
            {
                Debug.LogError($"Failed to load static data");
                return null;
            }

            return data;
        }

        public ScriptablePlayerData GetRuntimeData()
        {
            return null;
        }

        public override void Awake()
        {
            base.Awake();

            // Manager.ResourceManager.Instance.Load
        }

        public void SaveRuntimeData(BaseRuntimeData runtimeData)
        {
            
        }

        public BaseStaticData GetDatas(BaseStage stage)
        {
            BaseStaticData staticdata = GetStaticData(stage);
            GetRuntimeData();

            return staticdata;
        }
    }
}
