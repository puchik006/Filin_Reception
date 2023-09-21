using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LeftScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _todayDate;
    [SerializeField] private TMP_Text _rightNowName;
    [SerializeField] private TMP_Text _rightNowText;

    private int currentPage = 1;

    private void Awake()
    {
        ShowTodayDate();
        ShowRightNowTableData();
    }

    private void Start()
    {
        _rightNowText.pageToDisplay = currentPage;
        StartCoroutine(AutoChangePage());
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
            yield return new WaitForSeconds(3);
            currentPage++;
            if (currentPage > _rightNowText.textInfo.pageCount) currentPage = 1;
            _rightNowText.pageToDisplay = currentPage;
        }
    }

    private void ShowRightNowTableData()
    {
        DateTime currentTime = DateTime.Now;

        int id = 0;

        while (PlayerPrefs.HasKey("TableData_" + id.ToString()))
        {
            string json = PlayerPrefs.GetString("TableData_" + id.ToString());

            Debug.Log(json);

            TableData tableData = JsonUtility.FromJson<TableData>(json);

            DateTime timeStart = DateTime.ParseExact(tableData.TimeStart, "HH:mm", null);
            DateTime timeEnd = DateTime.ParseExact(tableData.TimeEnd, "HH:mm", null);

            if (tableData.Day == currentTime.Day && tableData.Month == currentTime.Month)
            {
                Debug.Log("asgasashasaaaaaa" + id.ToString());

                if (currentTime.TimeOfDay >= timeStart.TimeOfDay && currentTime.TimeOfDay <= timeEnd.TimeOfDay)
                {
                    Debug.Log("asgasash" + id.ToString());

                    _rightNowName.text = tableData.Name;
                    _rightNowText.text = tableData.Description;
                    break;
                }
            }

            id++;
        }
    }
    
}
