using UnityEngine;

public class MainStage : BaseStage
{
    public override void OnEnter()
    {
        Debug.Log("Main Stage Enter");
    }

    public override void OnExit()
    {
        Debug.Log("Main Stage Exit");
    }

    // UI 버튼에서 호출
    public void GoToRoguelike()
    {
        GoToStage(StageType.Roguelike);
    }

    public void GoToShooting()
    {
        GoToStage(StageType.Shooting);
    }

    public void GoToDeckStrategy()
    {
        GoToStage(StageType.DeckStrategy);
    }

    public void GoToExtractionShooter()
    {
        GoToStage(StageType.ExtractionShooter);
    }

    public void GoToProduction()
    {
        GoToStage(StageType.Production);
    }
}
