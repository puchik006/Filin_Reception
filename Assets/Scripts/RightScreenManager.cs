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

    [SerializeField] private TMP_InputField _todayInputDate;
    [SerializeField] private TMP_InputField _tomorrowInputDate;
    [SerializeField] private TMP_Text _leftScreenToday;

    private List<int> _todaySchedule = new();
    private int currentPage = 0;

    private void Awake()
    {
        TableDataManager.TableUpdated += OnTableUpdated;

        LoadDates();

        FillTodaySchedule();
        StartCoroutine(ShowTodaySchedule());

        _todayInputDate.onSubmit.AddListener(UpdateTodayDate);
        _tomorrowInputDate.onSubmit.AddListener(UpdateTomorrowDate);
    }

    private void LoadDates()
    {
        if (PlayerPrefs.HasKey("Today"))
        {
            _todayDate.text = PlayerPrefs.GetString("Today");
            _leftScreenToday.text = PlayerPrefs.GetString("Today");
            _todayInputDate.text = PlayerPrefs.GetString("Today");
        }

        if (PlayerPrefs.HasKey("Tomorrow"))
        {
            _tomorowDate.text = PlayerPrefs.GetString("Tomorrow");
            _tomorrowInputDate.text = PlayerPrefs.GetString("Tomorrow");
        }
    }

    private void UpdateTodayDate(string date)
    {
        _todayDate.text = date;
        _leftScreenToday.text = date;

        PlayerPrefs.SetString("Today",date);
    }

    private void UpdateTomorrowDate(string date)
    {
        _tomorowDate.text = date;

        PlayerPrefs.SetString("Tomorrow", date);
    }

    private void OnTableUpdated()
    {
        StopAllCoroutines();
        FillTodaySchedule();
        StartCoroutine(ShowTodaySchedule());
    }

    private void FillTodaySchedule()
    {
        int id = 0;

        _todaySchedule.Clear();

        while (PlayerPrefs.HasKey("TableData_" + id.ToString()))
        {
            _todaySchedule.Add(id);

            id++;
        }
    }

    private IEnumerator ShowTodaySchedule()
    {
        while (true)
        {
            int pageQty = _todaySchedule.Count % 3 == 0 ? _todaySchedule.Count / 3 : (_todaySchedule.Count / 3) + 1;

            yield return new WaitForSeconds(5);
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
