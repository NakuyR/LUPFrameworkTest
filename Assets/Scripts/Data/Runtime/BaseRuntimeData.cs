using System;
using UnityEditor;
using UnityEngine;

public abstract class BaseRuntimeData : ScriptableObject
{
    [SerializeField]
    public string filename;

    public event Action OnValueChanged;

    protected void NotifyValueChanged()
    {
        OnValueChanged?.Invoke();
    }

    public abstract void ResetData();
    public abstract void SaveData();
}
