using System;
using System.Collections;
using System.Globalization;
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

        //_btnSave.Add(() => SaveAndUpdate());

        SaveAll.SaveAllData += SaveAndUpdate;
    }

    private void Start()
    {
        // Start the coroutine to check conditions every 10 seconds
        StartCoroutine(CheckConditionsPeriodically());
    }

    private IEnumerator CheckConditionsPeriodically()
    {
        while (true)
        {
            CheckConditions();
            yield return new WaitForSeconds(10f); // Adjust the interval as needed
        }
    }

    private void CheckConditions()
    {
        // Assuming you have some IDs to iterate over
        for (int id = 0; id <= 5; id++)
        {
            string key = "TableData_" + id;

            if (PlayerPrefs.HasKey(key))
            {
                // Deserialize the stored data for the current ID
                string serializedData = PlayerPrefs.GetString(key);
                // Assuming you have a class like TableData with properties like TimeStart, TimeEnd, and Name
                TableData tableData = JsonUtility.FromJson<TableData>(serializedData);

                // Check if the current time is between TimeStart and TimeEnd
                DateTime currentTime = DateTime.Now;
                string timeStart = tableData.TimeStart;
                string timeEnd = tableData.TimeEnd;

                if (!string.IsNullOrEmpty(timeStart) && !string.IsNullOrEmpty(timeEnd) &&
                    IsValidTimeFormat(timeStart) && IsValidTimeFormat(timeEnd) &&
                    IsCurrentTimeInRange(currentTime, timeStart, timeEnd) &&
                    !string.IsNullOrEmpty(tableData.Name))
                {
                    // Set _rightNowName to the Name from the stored data
                    _rightNowName.text = tableData.Name;
                    // If you want to break out of the loop after finding the first match, you can use 'break;'
                    break;
                }
            }
        }
    }

    // Helper method to check if a time string is in the "HH:mm" format
    private bool IsValidTimeFormat(string timeString)
    {
        DateTime dummyResult;
        return DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummyResult);
    }

    // Helper method to check if the current time is between TimeStart and TimeEnd
    private bool IsCurrentTimeInRange(DateTime currentTime, string timeStart, string timeEnd)
    {
        TimeSpan startTime = TimeSpan.Parse(timeStart);
        TimeSpan endTime = TimeSpan.Parse(timeEnd);
        TimeSpan currentTimeOfDay = currentTime.TimeOfDay;

        return currentTimeOfDay >= startTime && currentTimeOfDay <= endTime;
    }

    private void SaveAndUpdate()
    {
        ShowTodayDate();
        //_rightNowName.text = _rightNowNameInput.text;
        _rightNowText.text = _rightNowTextInput.text;

        PlayerPrefs.SetString(_keyName, _rightNowNameInput.text);
        PlayerPrefs.SetString(_keyText, _rightNowTextInput.text);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(_keyName) && PlayerPrefs.HasKey(_keyText))
        {
            //_rightNowName.text = PlayerPrefs.GetString(_keyName);
            _rightNowText.text = PlayerPrefs.GetString(_keyText);

            _rightNowNameInput.text = PlayerPrefs.GetString(_keyName);
            _rightNowTextInput.text = PlayerPrefs.GetString(_keyText);

        }
    }

    private void ShowTodayDate()
    {
        DateTime currentDate = DateTime.Now;
        string russianDate = currentDate.ToString("dd MMMM", new System.Globalization.CultureInfo("ru-RU"));
        //_todayDate.text = russianDate;
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

    private void OnDisable()
    {
        SaveAll.SaveAllData -= SaveAndUpdate;
    }

}
