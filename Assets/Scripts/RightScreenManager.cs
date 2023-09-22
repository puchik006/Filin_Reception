using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _todayDate;
    [SerializeField] private TMP_Text _tomorowDate;
    [SerializeField] private GameObject _firstString;
    [SerializeField] private GameObject _secondString;
    [SerializeField] private GameObject _thirdString;
    private List<int> _todaySchedule = new();
    private int currentPage = 0;

    private void Awake()
    {
        TableDataManager.TableUpdated += OnTableUpdated;
        _todayDate.text = GetRusStringDate(DateTime.Now);
        _tomorowDate.text = GetRusStringDate(DateTime.Now.AddDays(1));
        FillTodaySchedule();
        StartCoroutine(ShowTodaySchedule());
    }

    private void OnTableUpdated()
    {
        FillTodaySchedule();
    }

    private string GetRusStringDate(DateTime date)
    {
        return date.ToString("dd MMMM", new System.Globalization.CultureInfo("ru-RU"));
    }

    private void FillTodaySchedule()
    {
        DateTime currentTime = DateTime.Now;

        int id = 0;

        _todaySchedule.Clear();

        while (PlayerPrefs.HasKey("TableData_" + id.ToString()))
        {
            string json = PlayerPrefs.GetString("TableData_" + id.ToString());

            TableData tableData = JsonUtility.FromJson<TableData>(json);

            if (tableData.Day == currentTime.Day && tableData.Month == currentTime.Month)
            {
                _todaySchedule.Add(id);
            }

            id++;
        }
    }

    private IEnumerator ShowTodaySchedule()
    {
        while (true)
        {
            int pageQty = _todaySchedule.Count % 3 == 0 ? _todaySchedule.Count / 3 : (_todaySchedule.Count / 3) + 1;

            yield return new WaitForSeconds(3);
            currentPage++;
            if (currentPage >= pageQty) currentPage = 0;

            if ((0 + currentPage*3) < _todaySchedule.Count)
            {
                _firstString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[0 + currentPage * 3]);
            }
            else
            {
                _firstString.SetActive(false);
            }

            if ((1 + currentPage*3) < _todaySchedule.Count)
            {
                _secondString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[1 + currentPage * 3]);
                _secondString.SetActive(true);
            }
            else
            {
                _secondString.SetActive(false);
            }

            if ((2 + currentPage*3) < _todaySchedule.Count)
            {
                _thirdString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[2 + currentPage * 3]);
                _thirdString.SetActive(true);
            }
            else
            {
                _thirdString.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        TableDataManager.TableUpdated -= OnTableUpdated;
    }

}
