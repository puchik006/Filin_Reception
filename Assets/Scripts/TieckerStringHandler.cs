using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TieckerStringHandler : MonoBehaviour
{
    public int Id;
    [SerializeField] private TMP_InputField _start;
    [SerializeField] private TMP_InputField _name;

    [SerializeField] private Button _save;
    [SerializeField] private Button _delete;

    public static event Action ButtonAddPressed;
    public static event Action<int> ButtonDeletePressed;

    private void Awake()
    {
        _save.Add(() => Save());
        _delete.Add(() => ButtonDeletePressed?.Invoke(Id));
    }

    private void Save()
    {
        TableData tableData = new TableData
        {
            Name = _name.text,
            TimeStart = _start.text
            
        };

        string jsonData = JsonUtility.ToJson(tableData);

        PlayerPrefs.SetString("TieckerData_" + Id.ToString(), jsonData);

        TableDataManager.TableUpdated?.Invoke();
    }

    public void Load(int id)
    {
        string jsonData = PlayerPrefs.GetString("TieckerData_" + id.ToString());

        if (!string.IsNullOrEmpty(jsonData))
        {
            TableData tableData = JsonUtility.FromJson<TableData>(jsonData);

            _name.text = tableData.Name;
            _start.text = tableData.TimeStart;
        }
        else
        {
            Debug.LogWarning("No data found for _id: " + id);
        }
    }
}
