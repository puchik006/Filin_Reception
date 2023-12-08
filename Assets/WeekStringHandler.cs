using TMPro;
using UnityEngine;

public class WeekStringHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _date;
    [SerializeField] private TMP_Text _Name;

    public void LoadString(string date, string name)
    {
        _date.text = date;
        _Name.text = name;
    }
}
