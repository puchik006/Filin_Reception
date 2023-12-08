using UnityEngine;

public class WeekDataHandler: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _prefab;

    private void OnEnable()
    {
        LoadStrings();
    }

    private void LoadStrings()
    {
        int id = 0;

        DestroyStrings();

        GameObject weekString;

        while (PlayerPrefs.HasKey("TieckerData_" + id.ToString()))
        {
            string json = PlayerPrefs.GetString("TieckerData_" + id.ToString());

            TableData tableData = JsonUtility.FromJson<TableData>(json);

            weekString =  Instantiate(_prefab, _content);
            weekString.GetComponent<WeekStringHandler>().LoadString(tableData.TimeStart, tableData.Name);

            id++;
        }
    }

    private void DestroyStrings()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }
    }
}
