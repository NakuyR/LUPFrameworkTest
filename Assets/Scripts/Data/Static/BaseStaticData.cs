using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStaticData", menuName = "Scriptable Objects/BaseStaticData")]
public abstract class BaseStaticData : ScriptableObject
{
    protected abstract string URL { get; }
    public abstract List<object> GetDataList();

    [Header("스프레드 시트의 시트 이름")][SerializeField] public string associatedWorksheet = "";
    [Header("읽기 시작할 행 번호")][SerializeField] public int START_ROW = 1;

    public abstract IEnumerator LoadSheet();

    // 각 자식 클래스가 구현해야 할 메서드들
    protected abstract object ParseDataRow(string[] values);
    protected abstract void ClearDataList();
    protected abstract void AddToDataList(object data);

    protected void ParseSheet(string csvData)
    {
        Debug.Log($"[{GetType().Name}] ParseSheet called with {csvData.Length} chars");

        string[] lines = csvData.Split('\n');
        Debug.Log($"[{GetType().Name}] Split into {lines.Length} lines");

        if (lines.Length < 2)
        {
            Debug.LogWarning($"[{GetType().Name}] Not enough lines in CSV (need at least 2)");
            return;
        }

        string[] headers = lines[0].Split(',');
        Debug.Log($"[{GetType().Name}] Headers: {string.Join(", ", headers)}");

        for (int i = 0; i < headers.Length; i++)
            headers[i] = headers[i].Trim();

        ClearDataList();

        int successCount = 0;
        int failCount = 0;

        for (int i = START_ROW; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] values = lines[i].Split(',');

            if (values.Length >= 4)
            {
                try
                {
                    object data = ParseDataRow(values);
                    if (data != null)
                    {
                        AddToDataList(data);
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"[{GetType().Name}] Failed to parse line {i}: {e.Message}");
                    failCount++;
                }
            }
            else
            {
                Debug.LogWarning($"[{GetType().Name}] Line {i} has only {values.Length} values (need 4)");
                failCount++;
            }
        }

        Debug.Log($"[{GetType().Name}] Loaded {GetDataList().Count} entries (Success: {successCount}, Failed: {failCount})");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BaseStaticData), true)]  
public class BaseStaticDataReaderEditor : Editor
{
    BaseStaticData data;

    void OnEnable()
    {
        data = (BaseStaticData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기"))
        {
            Debug.Log("[BaseStaticData] Button clicked, starting load...");
            LoadDataAsync();
        }
    }

    private async void LoadDataAsync()
    {
        try
        {
            IEnumerator coroutine = data.LoadSheet();

            while (coroutine.MoveNext())
            {
                if (coroutine.Current != null)
                {
                    if (coroutine.Current is UnityEngine.Networking.UnityWebRequestAsyncOperation asyncOp)
                    {
                        Debug.Log("[BaseStaticData] Waiting for web request...");
                        while (!asyncOp.isDone)
                        {
                            await System.Threading.Tasks.Task.Delay(100);
                        }
                        Debug.Log("[BaseStaticData] Web request completed!");
                    }
                }
            }

            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            Debug.Log("[BaseStaticData] Data loading completed!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[BaseStaticData] Error: {e.Message}\n{e.StackTrace}");
        }
    }
}
#endif
