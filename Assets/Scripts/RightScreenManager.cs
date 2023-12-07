using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _todayDate;
    [SerializeField] private TMP_Text _tomorowDate;
    //[SerializeField] private GameObject _firstString;
    //[SerializeField] private GameObject _secondString;
    //[SerializeField] private GameObject _thirdString;
    //[SerializeField] private GameObject _fourthString;
    //[SerializeField] private GameObject _fifsString;

    [SerializeField] private List<GameObject> _scheduleStrings;

    [SerializeField] private TMP_InputField _todayInputDate;
    [SerializeField] private TMP_InputField _tomorrowInputDate;
    [SerializeField] private TMP_Text _leftScreenToday;

    private List<int> _todaySchedule = new();
    //private int currentPage = 0;

    private void Awake()
    {
        TableDataManager.TableUpdated += OnTableUpdated;
        SaveAll.SaveAllData += SaveAndUpdateDates;

        LoadDates();
        FillTodaySchedule();
        //StartCoroutine(ShowTodaySchedule());
        SwithOffStrings();
        FillUpTodaySchedule();
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

    private void SaveAndUpdateDates()
    {
        PlayerPrefs.SetString("Today", _todayInputDate.text);
        PlayerPrefs.SetString("Tomorrow", _tomorrowInputDate.text);
        LoadDates();
    }

    private void OnTableUpdated()
    {
        //StopAllCoroutines();
        FillTodaySchedule();
        //StartCoroutine(ShowTodaySchedule());
        FillUpTodaySchedule();
    }

    private void SwithOffStrings()
    {
        _scheduleStrings.ForEach(e => e.SetActive(false));
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

    private void FillUpTodaySchedule()
    {
        SwithOffStrings();

        for (int i = 0; i < _todaySchedule.Count; i++)
        {
            _scheduleStrings[i].SetActive(true);
            _scheduleStrings[i].GetComponent<ScheduleStringHandler>().Load(i);
        }
    }

    //private IEnumerator ShowTodaySchedule()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5);

    //        int pageQty = _todaySchedule.Count % 3 == 0 ? _todaySchedule.Count / 3 : (_todaySchedule.Count / 3) + 1;

    //        currentPage++;

    //        if (currentPage >= pageQty) currentPage = 0;

    //        if ((0 + currentPage*3) < _todaySchedule.Count)
    //        {
    //            _firstString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[0 + currentPage * 3]);
    //            _firstString.SetActive(true);
    //        }
    //        else
    //        {
    //            _firstString.SetActive(false);
    //        }

    //        if ((1 + currentPage*3) < _todaySchedule.Count)
    //        {
    //            _secondString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[1 + currentPage * 3]);
    //            _secondString.SetActive(true);
    //        }
    //        else
    //        {
    //            _secondString.SetActive(false);
    //        }

    //        if ((2 + currentPage*3) < _todaySchedule.Count)
    //        {
    //            _thirdString.GetComponent<ScheduleStringHandler>().Load(_todaySchedule[2 + currentPage * 3]);
    //            _thirdString.SetActive(true);
    //        }
    //        else
    //        {
    //            _thirdString.SetActive(false);
    //        }
    //    }
    //}

    private void OnDisable()
    {
        TableDataManager.TableUpdated -= OnTableUpdated;
        SaveAll.SaveAllData -= SaveAndUpdateDates;
    }

}
