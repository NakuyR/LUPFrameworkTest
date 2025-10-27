using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class BaseRuntimeData
{
    [SerializeField]
    public string filename;

    public event Action OnValueChanged;

    // 지연 저장 설정
    private static MonoBehaviour coroutineRunner;
    private Coroutine saveCoroutine;
    private float saveDelay = 0.5f;  // 0.5초 후 저장

    public static void SetCoroutineRunner(MonoBehaviour runner)
    {
        coroutineRunner = runner;
    }

    public void SetSaveDelay(float delay)
    {
        saveDelay = delay;
    }

    protected void NotifyValueChanged()
    {
        OnValueChanged?.Invoke();
        SaveData();  
    }

    public abstract void ResetData();

    public void SaveData()
    {
        // 기존 저장 예약 취소
        if (saveCoroutine != null && coroutineRunner != null)
        {
            coroutineRunner.StopCoroutine(saveCoroutine);
        }

        // 새 저장 예약
        if (coroutineRunner != null)
        {
            saveCoroutine = coroutineRunner.StartCoroutine(SaveAfterDelay());
        }
        else // 코루틴 러너가 없으면 즉시 저장
        {
            SaveDataImmediate();
            Debug.LogWarning($"[{GetType().Name}] 코루틴 러너가 설정되지 않아 즉시 저장합니다.");
        }
    }

    private IEnumerator SaveAfterDelay()
    {
        yield return new WaitForSeconds(saveDelay);
        SaveDataImmediate();
        Debug.Log($"[{GetType().Name}] 저장 완료 (지연: {saveDelay}초)");
    }

    private void SaveDataImmediate()
    {
        JsonDataHelper.SaveData(this, filename);
    }
}
