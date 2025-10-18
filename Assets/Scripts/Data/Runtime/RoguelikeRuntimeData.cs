using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

[CreateAssetMenu(fileName = "RoguelikeRuntimeData", menuName = "Scriptable Objects/RoguelikeRuntimeData")]
public class RoguelikeRuntimeData : BaseRuntimeData
{

    public override void SaveData()
    {
        base.SaveData();
    }

    public override void ResetData()
    {
        //
    }
}
