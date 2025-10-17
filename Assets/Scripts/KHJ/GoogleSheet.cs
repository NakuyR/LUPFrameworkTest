using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GoogleSheet : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/11yM9l6g4opxVTflwsOVV0nZoIPUQ9VnA0rhkasLEi7I/export?format=csv";

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
