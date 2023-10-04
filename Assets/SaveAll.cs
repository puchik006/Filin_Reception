using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveAll : MonoBehaviour
{
    private Button _button;
    public static Action SaveAllData;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.Add(() => SaveAllData?.Invoke());
    }
}
