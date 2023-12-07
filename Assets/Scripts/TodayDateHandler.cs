using System;
using TMPro;
using UnityEngine;

public class TodayDateHandler: MonoBehaviour
{
    [SerializeField] private TMP_Text _todayDate;

    private void Awake()
    {
        ShowTodayDate();
    }

    private void ShowTodayDate()
    {
        DateTime currentDate = DateTime.Now;
        string russianDate = currentDate.ToString("dd MMMM", new System.Globalization.CultureInfo("ru-RU"));
        _todayDate.text = russianDate;
    }
}
