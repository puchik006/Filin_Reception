using System;
using TMPro;
using UnityEngine;

public class Ticker: MonoBehaviour
{
    [SerializeField] private TickerItem _tickerItemPrefab;
    [Range(0f, 10f)][SerializeField] private float _duration;
    [SerializeField] private string[] _items;

    private float _width;
    private float _pixelPerSecond;
    private TickerItem _curentItem;

    private void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _pixelPerSecond = _width / _duration;
        AddTickerItem(_items[0]);
    }

    private void AddTickerItem(string message)
    {
        _curentItem = Instantiate(_tickerItemPrefab, transform);
        _curentItem.Initialize(_width, _pixelPerSecond, message);
    }
}
