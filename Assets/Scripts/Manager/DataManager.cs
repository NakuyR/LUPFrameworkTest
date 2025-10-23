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

            data = ResourceManager.Instance.LoadStaticData(stage.StageKind);

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
