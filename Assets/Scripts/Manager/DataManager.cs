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
        ScriptablePlayerData data;

        //private T GetStaticData<T>(Manager.StageGameType type)
        //{
        //   T data;

        //   switch (type)
        //   {
        //       case Manager.StageGameType.Shooting:
        //           //data = Manager.ResourceManager.Instance.Load ~~~~
        //           //return data; 
        //           break;
        //       case Manager.StageGameType.SLG:
        //           //data = Manager.ResourceManager.Instance.Load ~~~~
        //           //return data;
        //           break;
        //       case Manager.StageGameType.ExtractionShooter:
        //           //data = Manager.ResourceManager.Instance.Load ~~~~
        //           //return data;
        //           break;
        //       case Manager.StageGameType.Roguelike:
        //          // data = Manager.ResourceManager.Instance.Load ~~~~
        //           return data;
        //           break;
        //       case Manager.StageGameType.Build:
        //           //data = Manager.ResourceManager.Instance.Load ~~~~
        //           return data;
        //           break;
        //   }
        //   //return null;
        //}

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


        public void LoadData()
        {
            // Manager.ResourceManager.~~~

        }
    }
}
