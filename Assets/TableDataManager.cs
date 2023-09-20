using UnityEngine;

public class TableDataManager: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _stringPrefab;

    private void OnEnable()
    {
        TableStringHandler.ButtonAddPressed += () => AddDataString();
    }

    private void Awake()
    {
        LoadDataTable();
    }

    private void LoadDataTable()
    {
        int lastId = 0;
        while (PlayerPrefs.HasKey("TableData_" + lastId.ToString()))
        {
            LoadDataString(lastId);
            lastId++;
        }
    }

    private void LoadDataString(int id)
    {
        GameObject dataString;
        dataString =  Instantiate(_stringPrefab, _content);
        dataString.GetComponent<TableStringHandler>().Load(id);
    }

    private void AddDataString()
    {
        Instantiate(_stringPrefab, _content);
    }
}
