using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePlayerData", menuName = "Scriptable Objects/ScriptablePlayerData")]
public class ScriptablePlayerData : ScriptableObject
{
    [SerializeField]
    public float soundVolume;

    [SerializeField]
    public float playerLevel;

    [SerializeField]
    public float gold;
}
