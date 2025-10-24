using Manager;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacter : MonoBehaviour
{
    public string name;
    public string description;
    public string stat;
    public int gold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if ((RoguelikeStage)Manager.StageManager.Instance.GetCurrentStage())
        {
            RoguelikeStage stage = (RoguelikeStage)Manager.StageManager.Instance.GetCurrentStage();
            RoguelikeStaticData data = (RoguelikeStaticData)stage.data;
            List<RoguelikeScriptData> datalist = data.GetRoguelikeDataList();

            name = datalist[0].name;
            description = datalist[0].description;
            stat = datalist[0].stat;
            gold = datalist[0].gold;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
