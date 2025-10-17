using UnityEngine;

public class RoguelikeStage : BaseStage
{
    // private RoguelikeGameCenter gameCenter;

    public override void OnEnter()
    {
        Debug.Log("Roguelike Stage Enter");

        // GameCenter 초기화
        //gameCenter = FindObjectOfType<RoguelikeGameCenter>();
        //if (gameCenter != null)
        //{
        //    gameCenter.Initialize();
        //}

        // 필요한 리소스 로드
        // ResourceManager.Instance.LoadRoguelikeResources();
    }

    public override void OnExit()
    {
        Debug.Log("Roguelike Stage Exit");

        //if (gameCenter)
        //    gameCenter.Cleanup();
    }

    // UI 버튼에서 호출
    public void ReturnToMain()
    {
        GoToStage(StageType.Main);
    }
}