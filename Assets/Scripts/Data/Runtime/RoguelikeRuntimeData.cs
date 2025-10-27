using System;
using UnityEngine;

[Serializable]
public class RoguelikeRuntimeData : BaseRuntimeData
{
    [SerializeField] private int _id = 0;
    [SerializeField] private string _name = "RoguelikePlayer";

    public int id
    {
        get => _id;
        set
        {
            if (_id != value)
            {
                _id = value;
                NotifyValueChanged();  // 자동 저장 트리거
            }
        }
    }

    public string name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                NotifyValueChanged();  // 자동 저장 트리거
            }
        }
    }

    public override void ResetData()
    {
        _id = 0;
        _name = "RoguelikePlayer";
    }
}
