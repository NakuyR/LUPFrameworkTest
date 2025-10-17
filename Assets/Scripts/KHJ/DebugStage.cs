using UnityEngine;

public class DebugStage : BaseStage
{
    [SerializeField] StageType DebugTargetStage;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("Main Stage Enter");
    }

    public override void OnExit()
    {
        Debug.Log("Main Stage Exit");
    }

    protected override void Awake()
    {
        base.Awake();

        GoToStage(DebugTargetStage);
    }
}
