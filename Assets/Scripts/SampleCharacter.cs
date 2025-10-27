using Manager;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SampleCharacter : MonoBehaviour
{
    private RoguelikeRuntimeData runtimedata;

    public string name;
    public string description;
    public string stat;
    public int gold;

    void Awake()
    {
        RoguelikeStage stage = Manager.StageManager.Instance.GetCurrentStage() as RoguelikeStage;
        if (stage != null)
        {
            RoguelikeStaticData staticdata = (RoguelikeStaticData)stage.StaticData;
            RoguelikeRuntimeData runtimeData = (RoguelikeRuntimeData)stage.RuntimeData;
            List<RoguelikeScriptData> datalist = staticdata.GetRoguelikeDataList();

            name = datalist[0].name;
            description = datalist[0].description;
            stat = datalist[0].stat;
            gold = datalist[0].gold;

            runtimedata = runtimeData;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (runtimedata == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            runtimedata.id += 100;  
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            runtimedata.ResetData();
            runtimedata.SaveData(); 
        }
    }
}
