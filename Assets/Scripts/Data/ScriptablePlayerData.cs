using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePlayerData", menuName = "Scriptable Objects/ScriptablePlayerData")]
public class ScriptablePlayerData : ScriptableObject
{
    [SerializeField]
    float level;

    [SerializeField]
    float soundVolume;
}
