using UnityEngine;

public abstract class BaseStage : MonoBehaviour
{
    [Header("Stage Info")]
    public StageType stageType;

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

    virtual protected void Awake()
    {

    }

}
