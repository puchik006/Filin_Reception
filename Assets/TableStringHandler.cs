using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableStringHandler : MonoBehaviour
{
    private int _id;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _type;
    [SerializeField] private TMP_InputField _speaker;
    [SerializeField] private TMP_InputField _place;
    [SerializeField] private TMP_InputField _description;
    [SerializeField] private TMP_InputField _start;
    [SerializeField] private TMP_InputField _end;
    [SerializeField] private TMP_Dropdown _day;
    [SerializeField] private TMP_Dropdown _month;
    [SerializeField] private Button _save;
    [SerializeField] private Button _add;
    [SerializeField] private Button _delete;

    private void Awake()
    {
        FindLastId();
        _save.Add(() => Save());
    }

    private void FindLastId()
    {
        int lastId = 0;
        while (PlayerPrefs.HasKey("TableData_" + lastId.ToString()))
        {
            lastId++;
        }
        _id = lastId;

        Debug.Log(_id);
    }

    private void Save()
    {
        TableData tableData = new TableData
        {
            Name = _name.text,
            Type = _type.text,
            Speaker = _speaker.text,
            Place = _place.text,
            Description = _description.text,
            TimeStart = _start.text,
            TimeEnd = _end.text,
            Day = _day.value,     
            Month = _month.value
        };

        string jsonData = JsonUtility.ToJson(tableData);

        PlayerPrefs.SetString("TableData_" + _id.ToString(), jsonData);
    }

    private void Load(int id)
    {
        string jsonData = PlayerPrefs.GetString("TableData_" + id.ToString());

        if (!string.IsNullOrEmpty(jsonData))
        {
            TableData tableData = JsonUtility.FromJson<TableData>(jsonData);

            _name.text = tableData.Name;
            _type.text = tableData.Type;
            _speaker.text = tableData.Speaker;
            _place.text = tableData.Place;
            _description.text = tableData.Description;
            _start.text = tableData.TimeStart;
            _end.text = tableData.TimeEnd;
            _day.value = tableData.Day;
            _month.value = tableData.Month;
        }
        else
        {
            Debug.LogWarning("No data found for _id: " + id);
        }
    }
}

[Serializable]
public class TableData
{
    public string Name;
    public string Type;
    public string Speaker;
    public string Place;
    public string Description;
    public string TimeStart;
    public string TimeEnd;
    public int Day;
    public int Month;
}
