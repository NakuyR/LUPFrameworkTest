using UnityEngine;

public abstract class BaseStage : MonoBehaviour
{
    [Header("Stage Info")]
    public StageType stageType;

    /// Stage 진입 시 호출
    public void OnEnter()
    {
        DataManager.Instance.LoadDatas();
    }

    /// Stage 종료 시 호출
    public void OnExit()
    {
        OnDataSaved();
    }

    // Stage 종료 시 데이터 저장 
    public abstract void OnDataSaved();

    // Stage 시작 시 데이터 불러오기 
    public abstract void OnDataLoaded();

    /// 다른 Stage로 이동
    protected void GoToStage(StageType targetStage)
    {
        StageManager.Instance.LoadStage(targetStage);
    }

    virtual protected void Awake()
    {

    }

}
