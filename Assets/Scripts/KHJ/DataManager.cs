using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DataManager : Singleton<DataManager>
{
    // Shooting 시트
    const string ShootingURL = "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv";

    // Roguelike 시트
    const string LikeURL = "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv&gid=2025045110#gid=2025045110";
   
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(LikeURL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }

    public void SaveDatas()
    {

    }

    public void LoadDatas()
    {

    }
}
