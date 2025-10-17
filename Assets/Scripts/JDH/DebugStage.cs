using Manager;
using UnityEngine;

namespace Manager
{
    public class DebugStage : BaseStage
    {
        public StageGameType TargetStage = StageGameType.MAX;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StageManager.Instance.loadStage(stageType,TargetStage);
        }
    }
}

