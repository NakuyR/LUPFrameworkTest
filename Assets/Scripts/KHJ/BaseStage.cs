using UnityEngine;

public abstract class BaseStage : MonoBehaviour
{
    [Header("Stage Info")]
    public StageType stageType;

    ScriptablePlayerData playerdata;

    virtual protected void Awake()
    {
        
    }

    // Stage 진입 시 호출
    virtual public void OnEnter()
    {
        
    }

    // Stage 종료 시 호출
    virtual public void OnExit()
    {
        
    }

    /// 다른 Stage로 이동
    protected void GoToStage(StageType targetStage)
    {
        StageManager.Instance.LoadStage(targetStage);
    }


    // 각 스테이지마다 자료구조?를 만들어서 사용해서 읽어 들인 데이터들을 넣기
    // public abstract void LoadDatas();
}
