using Manager;
using UnityEngine;

namespace Manager
{
    public class DebugStage : BaseStage
    {
        public StageKind TargetStage = StageKind.Main;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StageManager.Instance.LoadStage(TargetStage);
        }
        protected override void LoadResources()
        {
            //resource = ResourceManager.Instance.Load...
        }

        protected override void GetDatas()
        {
            //data = DataManager.Instance.GetData...
        }
    }
}

