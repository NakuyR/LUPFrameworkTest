using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePlayerData", menuName = "Scriptable Objects/ScriptablePlayerData")]
public class ScriptablePlayerData : BaseRuntimeData
{
    [SerializeField]
    float level;

    [SerializeField]
    float soundVolume;

    public override void ResetData()
    {
        //
    }
}
