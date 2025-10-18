using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "BaseStaticData", menuName = "Scriptable Objects/BaseStaticData")]
public abstract class BaseStaticData : ScriptableObject
{
    // 절 대 수 정 금 지 !
    protected abstract string URL { get; }

    
    [Header("스프레드 시트의 시트 이름")][SerializeField] public string associatedWorksheet = "";
    [Header("읽기 시작할 행 번호")][SerializeField] public int START_ROW_LENGTH = 1;
    [Header("읽을 마지막 행 번호")][SerializeField] public int END_ROW_LENGTH = -1;

    public abstract IEnumerator LoadSheet();

    protected abstract void ParseSheet(string csvData);
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
