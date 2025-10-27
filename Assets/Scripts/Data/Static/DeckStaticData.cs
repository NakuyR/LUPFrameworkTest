using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "DeckStaticData", menuName = "Scriptable Objects/DeckStaticData")]
public class DeckStaticData : BaseStaticData
{
    protected override string URL => "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv&gid=1544281943";

    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")]
    [SerializeField]
    public List<DeckScriptData> DataList = new List<DeckScriptData>();
    public List<DeckScriptData> GetDeckDataList() => DataList;

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
            return new DeckScriptData(name, desc, stat, gold);
        }

        Debug.LogWarning($"[DeckStaticData] Failed to parse gold value: '{values[3]}'");
        return null;
    }

    protected override void ClearDataList()
    {
        DataList.Clear();
    }

    protected override void AddToDataList(object data)
    {
        if (data is DeckScriptData deckData)
        {
            DataList.Add(deckData);
        }
    }

    public override IEnumerator LoadSheet()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            yield break;
        }

        string csvData = www.downloadHandler.text;

        ParseSheet(csvData);
    }
}
