using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "ShootingStaticData", menuName = "Scriptable Objects/ShootingStaticData")]
public class ShootingStaticData : BaseStaticData
{
    protected override string URL => "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv";

    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField]
    public List<ShootingScriptData> DataList = new List<ShootingScriptData>();
    public List<ShootingScriptData> GetShootingDataList() => DataList;

    public override List<object> GetDataList()
    {
        return DataList.Cast<object>().ToList();
    }

    protected override object ParseDataRow(string[] values)
    {
        string name = values[0].Trim();
        string desc = values[1].Trim();
        string stat = values[2].Trim();

        if (int.TryParse(values[3].Trim(), out int cur))
        {
            return new ShootingScriptData(name, desc, stat, cur);
        }

        Debug.LogWarning($"[ShootingStaticData] Failed to parse cur value: '{values[3]}'");
        return null;
    }

    protected override void ClearDataList()
    {
        DataList.Clear();
    }

    protected override void AddToDataList(object data)
    {
        if (data is ShootingScriptData shootData)
        {
            DataList.Add(shootData);
        }
    }

    public override IEnumerator LoadSheet()
    {
        Debug.Log($"[ShootingStaticData] Starting to load sheet from: {URL}");

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        Debug.Log($"[ShootingStaticData] Request completed. Result: {www.result}");

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[ShootingStaticData] Failed to load sheet: {www.error}");
            Debug.LogError($"[ShootingStaticData] Response Code: {www.responseCode}");
            yield break;
        }

        string csvData = www.downloadHandler.text;
        Debug.Log($"[ShootingStaticData] Downloaded {csvData.Length} characters");

        ParseSheet(csvData);
    }
}
