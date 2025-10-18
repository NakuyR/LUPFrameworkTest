using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// 이 클래스만 수정하세요 제발요!!!!!!!!!!!!!!!!
[System.Serializable]
public class RoguelikeData
{
    public string name;
    public string description;
    public string stat;
    public int gold;

    public RoguelikeData(string name, string description, string stat, int gold)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.gold = gold;
    }
}

[CreateAssetMenu(fileName = "RoguelikeStaticData", menuName = "Scriptable Objects/RoguelikeStaticData")]
public class RoguelikeStaticData : BaseStaticData
{
    protected override string URL => "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv&gid=2025045110";

    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField] public List<RoguelikeData> DataList = new List<RoguelikeData>();

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

    protected override void ParseSheet(string csvData)
    {
        Debug.Log($"[RoguelikeStaticData] ParseSheet called with {csvData.Length} chars");

        string[] lines = csvData.Split('\n');
        Debug.Log($"[RoguelikeStaticData] Split into {lines.Length} lines");

        if (lines.Length < 2)
        {
            Debug.LogWarning("[RoguelikeStaticData] Not enough lines in CSV (need at least 2)");
            return;
        }

        string[] headers = lines[0].Split(',');
        Debug.Log($"[RoguelikeStaticData] Headers: {string.Join(", ", headers)}");

        for (int i = 0; i < headers.Length; i++)
            headers[i] = headers[i].Trim();

        DataList.Clear();

        int successCount = 0;
        int failCount = 0;

        for (int i = START_ROW_LENGTH; i < lines.Length; i++)
        {
            if (END_ROW_LENGTH > 0 && i > END_ROW_LENGTH)
                break;

            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] values = lines[i].Split(',');

            if (values.Length >= 4)
            {
                string name = values[0].Trim();
                string desc = values[1].Trim();
                string stat = values[2].Trim();

                if (int.TryParse(values[3].Trim(), out int cur))
                {
                    DataList.Add(new RoguelikeData(name, desc, stat, cur));
                    successCount++;
                }
                else
                {
                    Debug.LogWarning($"[RoguelikeStaticData] Failed to parse 'cur' value: '{values[3]}' at line {i}");
                    failCount++;
                }
            }
            else
            {
                Debug.LogWarning($"[RoguelikeStaticData] Line {i} has only {values.Length} values (need 4)");
                failCount++;
            }
        }
        Debug.Log($"[RoguelikeStaticData] Loaded {DataList.Count} entries (Success: {successCount}, Failed: {failCount})");
    }
}

