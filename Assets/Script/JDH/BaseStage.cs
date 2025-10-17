using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class BaseStage : MonoBehaviour
    {
        public StageGameType stageType = StageGameType.MAX;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Awake()
        {
            StageManager.Instance.addObserver(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void LoadStage(string stage)
        {
            StageGameType endStageType = stageType;
            switch (stage)
            {
                case "Debug":
                    endStageType = StageGameType.Debug;
                    break;
                case "Main":
                    endStageType = StageGameType.Main;
                    break;
                case "Intro":
                    endStageType = StageGameType.Intro;
                    break;
                case "Roguelike":
                    endStageType = StageGameType.Roguelike;
                    break;
                case "Shooting":
                    endStageType = StageGameType.Shooting;
                    break;
                case "ExtractionShooter":
                    endStageType = StageGameType.ExtractionShooter;
                    break;
                case "Build":
                    endStageType = StageGameType.Build;
                    break;
                case "SLG":
                    endStageType = StageGameType.SLG;
                    break;
            }
            StageManager.Instance.loadStage(stageType, endStageType);
        }

        public virtual void onStageEnter()
        {
        }
        public virtual void onStageStay()
        {
        }

        public virtual void onStageExit()
        {
            StageManager.Instance.removeObserver(this);
        }
    }
}

