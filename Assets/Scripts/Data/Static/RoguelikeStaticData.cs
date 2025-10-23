using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "RoguelikeStaticData", menuName = "Scriptable Objects/RoguelikeStaticData")]
public class RoguelikeStaticData : BaseStaticData
{
    protected override string URL => "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv&gid=2025045110";

    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField]
    public List<RoguelikeScriptData> DataList = new List<RoguelikeScriptData>();
    public List<RoguelikeScriptData> GetRoguelikeDataList() => DataList;

    public override List<object> GetDataList()
    {
        return DataList.Cast<object>().ToList();
    }

    protected override object ParseDataRow(string[] values)
    {
        string name = values[0].Trim();
        string desc = values[1].Trim();
        string stat = values[2].Trim();

        if (int.TryParse(values[3].Trim(), out int gold))
        {
            return new RoguelikeScriptData(name, desc, stat, gold);
        }

        Debug.LogWarning($"[RoguelikeStaticData] Failed to parse gold value: '{values[3]}'");
        return null;
    }

    protected override void ClearDataList()
    {
        DataList.Clear();
    }

    protected override void AddToDataList(object data)
    {
        if (data is RoguelikeScriptData rogueData)
        {
            DataList.Add(rogueData);
        }
    }

    public override IEnumerator LoadSheet()
    {
        Debug.Log($"[RoguelikeStaticData] Starting to load sheet from: {URL}");

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        Debug.Log($"[RoguelikeStaticData] Request completed. Result: {www.result}");

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[RoguelikeStaticData] Failed to load sheet: {www.error}");
            Debug.LogError($"[RoguelikeStaticData] Response Code: {www.responseCode}");
            yield break;
        }

        string csvData = www.downloadHandler.text;
        Debug.Log($"[RoguelikeStaticData] Downloaded {csvData.Length} characters");

        ParseSheet(csvData);
    }
}

