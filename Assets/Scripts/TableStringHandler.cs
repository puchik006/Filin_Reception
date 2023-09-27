using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableStringHandler : MonoBehaviour
{
    public int Id;
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

    public static event Action ButtonAddPressed;
    public static event Action<int> ButtonDeletePressed;

    private void Awake()
    {
        _save.Add(() => Save());
        _add.Add(() => ButtonAddPressed?.Invoke());
        _delete.Add(() => ButtonDeletePressed?.Invoke(Id));
    }

    private void Save()
    {
        TableData tableData = new TableData
        {
            Name = _name.text,
            Type = _type.text,
            Speaker = _speaker.text,
            Place = _place.text,
            TimeStart = _start.text,
            TimeEnd = _end.text,
        };

        string jsonData = JsonUtility.ToJson(tableData);

        PlayerPrefs.SetString("TableData_" + Id.ToString(), jsonData);

        TableDataManager.TableUpdated?.Invoke();
    }

    public void Load(int id)
    {
        string jsonData = PlayerPrefs.GetString("TableData_" + id.ToString());

        if (!string.IsNullOrEmpty(jsonData))
        {
            TableData tableData = JsonUtility.FromJson<TableData>(jsonData);

            _name.text = tableData.Name;
            _type.text = tableData.Type;
            _speaker.text = tableData.Speaker;
            _place.text = tableData.Place;
            _start.text = tableData.TimeStart;
            _end.text = tableData.TimeEnd;
        }
        else
        {
            Debug.LogWarning("No data found for _id: " + id);
        }
    }
}
