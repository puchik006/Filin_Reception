using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ticker : MonoBehaviour
{
    [SerializeField] private TickerItem _tickerItemPrefab;
    [Range(0f, 10f)]
    [SerializeField] private float _duration;
    [SerializeField] private string[] _items;

    private float _width;
    private float _pixelPerSecond;
    private TickerItem _currentItem;

    private void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _pixelPerSecond = _width / _duration;
        AddTickerItem(_items[0]);

    }

    private void Update()
    {
        if (_currentItem.GetXPosition <= -_currentItem.Width)
        {
            AddTickerItem(_items[Random.Range(0,_items.Length)]);
        }
    }

    private void AddTickerItem(string message)
    {
        _currentItem = Instantiate(_tickerItemPrefab, transform);
        _currentItem.Initialize(_width, _pixelPerSecond, message);
    }
}

