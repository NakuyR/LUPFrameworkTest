using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// 이 클래스만 수정하세요 제발요!!!!!!!!!!!!!!!!
[System.Serializable]
public class ShootingData
{
    public string name;
    public string description;
    public string stat;
    public int cur;

    public ShootingData(string name, string description, string stat, int cur)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.cur = cur;
    }
}

// 절 대 수 정 금 지 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
[CreateAssetMenu(fileName = "ShootingStaticData", menuName = "Scriptable Objects/ShootingStaticData")]
public class ShootingStaticData : BaseStaticData
{
    protected override string URL => "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv";

    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField] public List<ShootingData> DataList = new List<ShootingData>();

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

    protected override void ParseSheet(string csvData)
    {
        Debug.Log($"[ShootingStaticData] ParseSheet called with {csvData.Length} chars");

        string[] lines = csvData.Split('\n');
        Debug.Log($"[ShootingStaticData] Split into {lines.Length} lines");

        if (lines.Length < 2)
        {
            Debug.LogWarning("[ShootingStaticData] Not enough lines in CSV (need at least 2)");
            return;
        }

        string[] headers = lines[0].Split(',');
        Debug.Log($"[ShootingStaticData] Headers: {string.Join(", ", headers)}");

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
                    DataList.Add(new ShootingData(name, desc, stat, cur));
                    successCount++;
                }
                else
                {
                    Debug.LogWarning($"[ShootingStaticData] Failed to parse 'cur' value: '{values[3]}' at line {i}");
                    failCount++;
                }
            }
            else
            {
                Debug.LogWarning($"[ShootingStaticData] Line {i} has only {values.Length} values (need 4)");
                failCount++;
            }
        }
        Debug.Log($"[ShootingStaticData] Loaded {DataList.Count} entries (Success: {successCount}, Failed: {failCount})");
    }
}
