using TMPro;
using UnityEngine;

public class ScheduleStringHandler: MonoBehaviour
{
    [SerializeField] private TMP_Text _start;
    [SerializeField] private TMP_Text _finish;
    [SerializeField] private TMP_Text _speaker;
    [SerializeField] private TMP_Text _hall;
    [SerializeField] private TMP_Text _type;
    [SerializeField] private TMP_Text _name;

    public void Load(int id)
    {
        string jsonData = PlayerPrefs.GetString("TableData_" + id.ToString());

        if (!string.IsNullOrEmpty(jsonData))
        {
            TableData tableData = JsonUtility.FromJson<TableData>(jsonData);

            _name.text = tableData.Name;
            _type.text = tableData.Type;
            _speaker.text = tableData.Speaker;
            _hall.text = tableData.Place;
            _start.text = tableData.TimeStart;
            _finish.text = tableData.TimeEnd;
        }
        else
        {
            Debug.LogWarning("No data found for _id: " + id);
        }
    }
}
