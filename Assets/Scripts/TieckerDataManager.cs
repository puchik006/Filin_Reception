using System;
using UnityEngine;
using UnityEngine.UI;

public class TieckerDataManager: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _stringPrefab;
    [SerializeField] private Button _btnAdd;

    public static Action TableUpdated;

    private void OnEnable()
    {
        _btnAdd.Add(() => AddDataString());
        TieckerStringHandler.ButtonDeletePressed += (id) => DeleteDataString(id);
    }

    private void Awake()
    {
        LoadDataTable();
    }

    private void LoadDataTable()
    {
        int lastId = 0;

        if (!PlayerPrefs.HasKey("TieckerData_" + lastId.ToString()))
        {
            AddDataString();
            return;
        }

        while (PlayerPrefs.HasKey("TieckerData_" + lastId.ToString()))
        {
            LoadDataString(lastId);
            lastId++;
        }
    }

    private int LastId()
    {
        int lastId = 0;

        while (PlayerPrefs.HasKey("TieckerData_" + lastId.ToString()))
        {
            lastId++;
        }

        return lastId;
    }

    private void LoadDataString(int id)
    {
        GameObject dataString;
        dataString = Instantiate(_stringPrefab, _content);
        dataString.GetComponent<TieckerStringHandler>().Load(id);
        dataString.GetComponent<TieckerStringHandler>().Id = id;
    }

    private void AddDataString()
    {
        GameObject dataString;
        dataString = Instantiate(_stringPrefab, _content);
        dataString.GetComponent<TieckerStringHandler>().Id = LastId();
        dataString.GetComponent<TieckerStringHandler>().Save();
    }

    private void DeleteDataString(int id)
    {
        string dataKey = "TieckerData_" + id.ToString();

        if (PlayerPrefs.HasKey(dataKey))
        {
            PlayerPrefs.DeleteKey(dataKey);

            foreach (Transform child in _content)
            {
                TieckerStringHandler stringHandler = child.GetComponent<TieckerStringHandler>();
                if (stringHandler != null && stringHandler.Id == id)
                {
                    Destroy(child.gameObject);
                    UpdateDataStringIds(id + 1);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Data string with ID " + id + " not found.");
        }
    }

    private void UpdateDataStringIds(int startId)
    {
        foreach (Transform child in _content)
        {
            TieckerStringHandler stringHandler = child.GetComponent<TieckerStringHandler>();
            if (stringHandler != null && stringHandler.Id >= startId)
            {
                stringHandler.Id--;

                string oldKey = "TieckerData_" + (stringHandler.Id + 1).ToString();
                string newKey = "TieckerData_" + stringHandler.Id.ToString();

                if (PlayerPrefs.HasKey(oldKey))
                {
                    string data = PlayerPrefs.GetString(oldKey);
                    PlayerPrefs.SetString(newKey, data);
                    PlayerPrefs.DeleteKey(oldKey);
                }
            }
        }

        TableUpdated?.Invoke();
    }

    private void OnDisable()
    {
        TieckerStringHandler.ButtonDeletePressed -= (id) => DeleteDataString(id);
    }
}
