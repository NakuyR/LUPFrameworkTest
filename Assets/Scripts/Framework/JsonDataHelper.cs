using System.IO;
using UnityEngine;

public static class JsonDataHelper
{
    public static void SaveData<T>(T data, string fileName)
    {
        string path = GetFilePath(fileName);
        string json = JsonUtility.ToJson(data, true); 

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"데이터 저장 완료: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 저장 실패: {e.Message}");
        }
    }

    public static T LoadData<T>(string fileName) where T : new()
    {
        string path = GetFilePath(fileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"파일이 없습니다: {path}. 새로운 데이터를 생성합니다.");
            return new T();
        }

        try
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"데이터 로드 완료: {path}");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 로드 실패: {e.Message}");
            return new T();
        }
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}

