using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct ItemData
{
    public string name;
    [TextArea] public string description;
    public string stat;
    public int cur;

    public ItemData(string name, string description, string stat, int cur)
    {
        this.name = name;
        this.description = description;
        this.stat = stat;
        this.cur = cur;
    }
}

[CreateAssetMenu(fileName = "Reader", menuName = "Scriptable Object/ItemDataReader", order = int.MaxValue)]
public class ItemDataReader : DataReaderBase
{
    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField] public List<ItemData> DataList = new List<ItemData>();

    internal void UpdateStats(List<GSTU_Cell> list, int itemID)
    {
        string name = null;
        string description = null;
        string stat = null;
        int cur = 0;

        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "name":
                    {
                        name = list[i].value;
                        break;
                    }
                case "description":
                    {
                        description = list[i].value;
                        break;
                    }
                case "stat":
                    {
                        stat = list[i].value;
                        break;
                    }
                case "cur":
                    {
                        cur = 1;
                        break;
                    }
            }
        }

        DataList.Add(new ItemData(name, description,stat,cur));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemDataReader))]
public class ItemDataReaderEditor : Editor
{
    ItemDataReader data;

    void OnEnable()
    {
        data = (ItemDataReader)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기(API 호출)"))
        {
            UpdateStats(UpdateMethodOne);
            data.DataList.Clear();
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        for (int i = data.START_ROW_LENGTH; i <= data.END_ROW_LENGTH; ++i)
        {
            data.UpdateStats(ss.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
}
#endif
