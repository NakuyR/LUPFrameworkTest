using UnityEngine;

public class RoguelikeStage : BaseStage
{

    public override void OnEnter()
    {
        Debug.Log("Roguelike Stage Enter");
    }

    public override void OnExit()
    {
        Debug.Log("Roguelike Stage Exit");
    }

    public void ReturnToMain()
    {
        GoToStage(StageType.Main);
    }
}