using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewsTicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _tickerTextPrefab;
    [Range(0f, 10f)]
    [SerializeField] private float _duration;
    [SerializeField] private string[] _items;
    [SerializeField] private int _maxVisibleItems = 3;

    private float _width;
    private float _pixelPerSecond;
    private Queue<TMP_Text> _textPool;
    private List<TMP_Text> _activeTexts;
    private int _currentIndex;
    private float _timeSinceLastSpawn;

    private void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _pixelPerSecond = _width / _duration;
        InitializeTextPool();
        _currentIndex = 0;
        _timeSinceLastSpawn = 0f;
    }

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _duration)
        {
            _timeSinceLastSpawn = 0f;
            ShowNextTickerItem();
        }

        MoveTextItems();
    }

    private void InitializeTextPool()
    {
        _textPool = new Queue<TMP_Text>();
        _activeTexts = new List<TMP_Text>();

        for (int i = 0; i < _maxVisibleItems; i++)
        {
            TMP_Text textItem = Instantiate(_tickerTextPrefab, transform);
            textItem.rectTransform.anchoredPosition = new Vector2(_width, 0f);
            _textPool.Enqueue(textItem);
        }
    }

    private void ShowNextTickerItem()
    {
        if (_items.Length > 0)
        {
            TMP_Text textItem = _textPool.Dequeue();
            textItem.text = _items[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _items.Length;
            _activeTexts.Add(textItem);
        }
    }

    private void MoveTextItems()
    {
        for (int i = 0; i < _activeTexts.Count; i++)
        {
            TMP_Text textItem = _activeTexts[i];
            RectTransform rectTransform = textItem.rectTransform;
            rectTransform.anchoredPosition += Vector2.left * _pixelPerSecond * Time.deltaTime;

            if (rectTransform.anchoredPosition.x <= -rectTransform.rect.width)
            {
                // Move text back to the right and return it to the pool
                rectTransform.anchoredPosition = new Vector2(_width, 0f);
                _activeTexts.RemoveAt(i);
                _textPool.Enqueue(textItem);
                i--;
            }
        }
    }
}

