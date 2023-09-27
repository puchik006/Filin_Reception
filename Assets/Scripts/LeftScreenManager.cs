using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeftScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _todayDate;
    [SerializeField] private TMP_Text _rightNowName;
    [SerializeField] private TMP_Text _rightNowText;

    [SerializeField] private Button _btnSave;
    [SerializeField] private TMP_InputField _rightNowNameInput;
    [SerializeField] private TMP_InputField _rightNowTextInput;

    private string _keyName = "RightNowName";
    private string _keyText = "RightNowText";

    private int currentPage = 1;

    private void Awake()
    {
        ShowTodayDate();
        _rightNowText.pageToDisplay = currentPage;
        StartCoroutine(AutoChangePage());
        LoadData();

        _btnSave.Add(() => SaveAndUpdate());
    }

    private void SaveAndUpdate()
    {
        ShowTodayDate();
        _rightNowName.text = _rightNowNameInput.text;
        _rightNowText.text = _rightNowTextInput.text;

        PlayerPrefs.SetString(_keyName, _rightNowNameInput.text);
        PlayerPrefs.SetString(_keyText, _rightNowTextInput.text);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(_keyName) && PlayerPrefs.HasKey(_keyText))
        {
            _rightNowName.text = PlayerPrefs.GetString(_keyName);
            _rightNowText.text = PlayerPrefs.GetString(_keyText);

            _rightNowNameInput.text = PlayerPrefs.GetString(_keyName);
            _rightNowTextInput.text = PlayerPrefs.GetString(_keyText);

        }
    }

    private void ShowTodayDate()
    {
        DateTime currentDate = DateTime.Now;
        string russianDate = currentDate.ToString("dd MMMM", new System.Globalization.CultureInfo("ru-RU"));
        _todayDate.text = russianDate;
    }

    private IEnumerator AutoChangePage()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            currentPage++;
            if (currentPage > _rightNowText.textInfo.pageCount) currentPage = 1;
            _rightNowText.pageToDisplay = currentPage;
        }
    }

    //private void ShowRightNowTableData()
    //{
    //    DateTime currentTime = DateTime.Now;

    //    int id = 0;

    //    while (PlayerPrefs.HasKey("TableData_" + id.ToString()))
    //    {
    //        string json = PlayerPrefs.GetString("TableData_" + id.ToString());

    //        TableData tableData = JsonUtility.FromJson<TableData>(json);

    //        DateTime timeStart = DateTime.ParseExact(tableData.TimeStart, "HH:mm", null);
    //        DateTime timeEnd = DateTime.ParseExact(tableData.TimeEnd, "HH:mm", null);

    //        if (tableData.Day == currentTime.Day && tableData.Month == currentTime.Month)
    //        {
    //            if (currentTime.TimeOfDay >= timeStart.TimeOfDay && currentTime.TimeOfDay <= timeEnd.TimeOfDay)
    //            {
    //                _rightNowName.text = tableData.Name;
    //                _rightNowText.text = tableData.Description;
    //                break;
    //            }
    //        }

    //        id++;
    //    }
    //}

}
