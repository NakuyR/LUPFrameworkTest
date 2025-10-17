using UnityEngine;

public abstract class DataReaderBase : ScriptableObject
{
    [Header("시트의 주소")][SerializeField] public string associatedSheet = "1V-RyOVtTezZFVAjnL1rmhmIRvjgNHPfoYeA-X2usk4c";
    [Header("스프레드 시트의 시트 이름")][SerializeField] public string associatedWorksheet = "Shooting";
    [Header("읽기 시작할 행 번호")][SerializeField] public int START_ROW_LENGTH = 1;
    [Header("읽을 마지막 행 번호")][SerializeField] public int END_ROW_LENGTH = 3;
}
