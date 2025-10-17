using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public enum StageGameType : int {
        Debug,
        Main,
        Intro,
        Roguelike,
        Shooting,
        ExtractionShooter,
        Build,
        SLG,
        MAX,
    }

    public class StageManager : Singleton<StageManager> 
    {
        //지금은 단일스테이지인데 나중에 멀티스테이지도 가능하대서 옵저버패턴 사용
        private List<BaseStage> stages = new List<BaseStage>();

        //트랜지션 변수
        bool[,] stageTransition = new bool[(int)StageGameType.MAX, (int)StageGameType.MAX];

        void Awake()
        {
            base.Awake();
            initTransition();
        }

        public void loadStage(StageGameType startStageType, StageGameType endStageType)
        {
            if (!checkTransition(startStageType, endStageType))
                return;

            onStageExit();

            switch (endStageType)
            {
                case StageGameType.Debug:
                    SceneManager.LoadScene("DebugStage");
                    break;
                case StageGameType.Main:
                    SceneManager.LoadScene("MainStage");
                    break;
                case StageGameType.Intro:
                    SceneManager.LoadScene("IntroStage");
                    break;
                case StageGameType.Roguelike:
                    SceneManager.LoadScene("RoguelikeSampleStage");
                    break;
                case StageGameType.Shooting:
                    SceneManager.LoadScene("ShootingSampleStage");
                    break;
                case StageGameType.ExtractionShooter:
                    SceneManager.LoadScene("ExtractionShooterSampleStage");
                    break;
                case StageGameType.Build:
                    SceneManager.LoadScene("BuildSampleStage");
                    break;
                case StageGameType.SLG:
                    SceneManager.LoadScene("SLGSampleStage");
                    break;
            }

            onStageEnter();
        }

        void onStageEnter()
        {
            if (stages.Count > 0)
            {
                foreach (var stage in stages)
                    stage.onStageEnter();
            }
        }
        void onStageStay()
        {
            if (stages.Count > 0)
            {
                foreach (var stage in stages)
                    stage.onStageStay();
            }
        }
        void onStageExit()
        {
            foreach (var stage in new List<BaseStage>(stages))
                stage.onStageExit();

        }
        
        public void addObserver(BaseStage stage)
        {
            if (!stages.Contains(stage))
                stages.Add(stage);
        }

        public void removeObserver(BaseStage stage)
        {
            stages.Remove(stage);
        }

        private void initTransition()
        {
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Roguelike] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.SLG] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.ExtractionShooter] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Build] = true;
            stageTransition[(int)StageGameType.Debug, (int)StageGameType.Shooting] = true;
            
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Roguelike] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.SLG] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.ExtractionShooter] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Build] = true;
            stageTransition[(int)StageGameType.Main, (int)StageGameType.Shooting] = true;
            
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Roguelike] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.SLG] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.ExtractionShooter] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Build] = true;
            stageTransition[(int)StageGameType.Intro, (int)StageGameType.Shooting] = true;
            
            stageTransition[(int)StageGameType.Roguelike, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Roguelike, (int)StageGameType.Roguelike] = true;
            stageTransition[(int)StageGameType.Roguelike, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Roguelike, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Roguelike, (int)StageGameType.Build] = true;
            
            stageTransition[(int)StageGameType.SLG, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.SLG, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.SLG, (int)StageGameType.SLG] = true;
            stageTransition[(int)StageGameType.SLG, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.SLG, (int)StageGameType.Build] = true;
            
            stageTransition[(int)StageGameType.ExtractionShooter, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.ExtractionShooter, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.ExtractionShooter, (int)StageGameType.ExtractionShooter] = true;
            stageTransition[(int)StageGameType.ExtractionShooter, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.ExtractionShooter, (int)StageGameType.Build] = true;
            
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Roguelike] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.SLG] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.ExtractionShooter] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Build] = true;
            stageTransition[(int)StageGameType.Build, (int)StageGameType.Shooting] = true;
            
            stageTransition[(int)StageGameType.Shooting, (int)StageGameType.Main] = true;
            stageTransition[(int)StageGameType.Shooting, (int)StageGameType.Debug] = true;
            stageTransition[(int)StageGameType.Shooting, (int)StageGameType.Intro] = true;
            stageTransition[(int)StageGameType.Shooting, (int)StageGameType.Build] = true;
            stageTransition[(int)StageGameType.Shooting, (int)StageGameType.Shooting] = true;
        }

        private bool checkTransition(StageGameType startStage, StageGameType endStage)
        {
            return stageTransition[(int)startStage,(int)endStage];
        }
    }
}