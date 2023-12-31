﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class TableDataManager: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _stringPrefab;
    [SerializeField] private Button _btnAdd;
    [SerializeField] private Button _btnUpdate;

    public static Action TableUpdated;

    private void OnEnable()
    {
        _btnAdd.Add(() => AddDataString());
        _btnUpdate.Add(() => TableUpdated?.Invoke());
        TableStringHandler.ButtonDeletePressed += (id) => DeleteDataString(id);
        TableStringHandler.ButtonAddPressed += (id) => AddDataStringInside(id);
    }

    private void Awake()
    {
        LoadDataTable();
    }

    private void AddDataStringInside(int insertIndex)
    {
        int newId = insertIndex + 1;


            // Move existing data strings to make space for the new one
            for (int i = _content.childCount - 1; i >= insertIndex; i--)
            {
                TableStringHandler stringHandler = _content.GetChild(i).GetComponent<TableStringHandler>();
                stringHandler.Id++;
                stringHandler.Save(); // Save the data with the updated ID
            }

            // Add the new empty data string at the specified index
            GameObject dataString = Instantiate(_stringPrefab, _content);
            TableStringHandler newStringHandler = dataString.GetComponent<TableStringHandler>();
            newStringHandler.Id = newId;
            newStringHandler.Save(); // Save the data with the new ID

            foreach (Transform child in _content)
            {
                TableStringHandler stringHandler = child.GetComponent<TableStringHandler>();
                if (stringHandler != null)
                {
                    Destroy(child.gameObject);
                }
            }

            // Load the data strings again to update the order
            LoadDataTable();
    }

    private void LoadDataTable()
    {
        int lastId = 0;

        if (!PlayerPrefs.HasKey("TableData_" + lastId.ToString()))
        {
            AddDataString();
            return;
        }

        while (PlayerPrefs.HasKey("TableData_" + lastId.ToString()))
        {
            LoadDataString(lastId);
            lastId++;
        }
    }

    private int LastId()
    {
        int lastId = 0;

        while (PlayerPrefs.HasKey("TableData_" + lastId.ToString()))
        {
            lastId++;
        }

        return lastId;
    }

    private void LoadDataString(int id)
    {
        GameObject dataString;
        dataString = Instantiate(_stringPrefab, _content);
        dataString.GetComponent<TableStringHandler>().Load(id);
        dataString.GetComponent<TableStringHandler>().Id = id;
    }

    private void AddDataString()
    {
            GameObject dataString;
            dataString = Instantiate(_stringPrefab, _content);
            dataString.GetComponent<TableStringHandler>().Id = LastId();
            dataString.GetComponent<TableStringHandler>().Save();
    }

    private void DeleteDataString(int id)
    {
        string dataKey = "TableData_" + id.ToString();

        if (PlayerPrefs.HasKey(dataKey))
        {
            PlayerPrefs.DeleteKey(dataKey);

            foreach (Transform child in _content)
            {
                TableStringHandler stringHandler = child.GetComponent<TableStringHandler>();
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
            TableStringHandler stringHandler = child.GetComponent<TableStringHandler>();
            if (stringHandler != null && stringHandler.Id >= startId)
            {
                stringHandler.Id--;

                string oldKey = "TableData_" + (stringHandler.Id + 1).ToString();
                string newKey = "TableData_" + stringHandler.Id.ToString();

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
        TableStringHandler.ButtonDeletePressed -= (id) => DeleteDataString(id);
    }

}
