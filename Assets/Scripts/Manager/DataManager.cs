using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine;

namespace Manager
{
    public class DataManager : Singleton<DataManager>
    {
        [SerializeField]
        BaseStaticData data;

        public BaseStaticData GetStaticData(StageKind stagekind, int stagetype)
        {
            BaseStaticData data = null;

            data = ResourceManager.Instance.LoadStaticData(stagekind, stagetype);

            if (!data)
            {
                Debug.LogError($"Failed to load static data");
                return null;
            }

            return data;
        }

        public BaseRuntimeData GetRuntimeData(StageKind stagekind, int stagetype)
        {
            BaseRuntimeData data = null;

            // 나중에 런타임데이터 스크립터블오브젝트가 들어가면 사용?
            // data = ResourceManager.Instance.LoadRuntimeData(stagekind, stagetype);

            // todo..
            //data = JsonDataHelper.LoadData();

            if (!data)
            {
                Debug.LogError($"Failed to load runtime data");
                return null;
            }

            return data;
        }

        public override void Awake()
        {
            base.Awake();

            // Manager.ResourceManager.Instance.Load
        }

        public void SaveRuntimeData(BaseRuntimeData runtimeData)
        {

        }
    }
}
