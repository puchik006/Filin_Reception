using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewsTicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _tickerTextPrefab;
    [Range(0f, 10f)]
    [SerializeField] private float _duration;
    private List<string> _items = new();

    private float _width;
    private float _pixelPerSecond;
    private Queue<TMP_Text> _textPool;
    private List<TMP_Text> _activeTexts;
    private int _currentIndex;
    private float _timeSinceLastSpawn;
    private int _itemsSpawned;

    private void Awake()
    {
        TieckerDataManager.TableUpdated += OnTableUpdated;
        FillItems();
    }

    private void OnTableUpdated()
    {
        FillItems();
        _textPool.Clear();
        _activeTexts.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        InitializeTextPool();
    }

    private void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _pixelPerSecond = _width / _duration;
        InitializeTextPool();
        _currentIndex = 0;
        _timeSinceLastSpawn = 0f;
        _itemsSpawned = 0;
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

    private void FillItems()
    {
        int id = 0;

        _items.Clear();

        while (PlayerPrefs.HasKey("TieckerData_" + id.ToString()))
        {
            string json = PlayerPrefs.GetString("TieckerData_" + id.ToString());

            TableData tableData = JsonUtility.FromJson<TableData>(json);

            _items.Add(tableData.TimeStart + " " +  tableData.Name);
 
            id++;
        }
    }

    private void InitializeTextPool()
    {
        _textPool = new Queue<TMP_Text>();
        _activeTexts = new List<TMP_Text>();

        for (int i = 0; i < _items.Count; i++)
        {
            TMP_Text textItem = Instantiate(_tickerTextPrefab, transform);
            textItem.rectTransform.anchoredPosition = new Vector2(_width, 0f);
            _textPool.Enqueue(textItem);
        }
    }

    private void ShowNextTickerItem()
    {
        if (_items.Count > 0)
        {
            if (_textPool.Count > 0)
            {
                TMP_Text textItem = _textPool.Dequeue();
                textItem.text = _items[_currentIndex];
                _currentIndex = (_currentIndex + 1) % _items.Count;
                _activeTexts.Add(textItem);
                _itemsSpawned++;
                if (_itemsSpawned >= _items.Count)
                {
                    _itemsSpawned = 0;
                }
            }
        }
    }

    private void MoveTextItems()
    {

        if (_activeTexts.Count <= 0) return;


        for (int i = 0; i < _activeTexts.Count; i++)
        {
            TMP_Text textItem = _activeTexts[i];
            RectTransform rectTransform = textItem.rectTransform;
            rectTransform.anchoredPosition += Vector2.left * _pixelPerSecond * Time.deltaTime;

            if (rectTransform.anchoredPosition.x <= -_width)
            {
                rectTransform.anchoredPosition = new Vector2(_width , 0f);
                _activeTexts.RemoveAt(i);
                _textPool.Enqueue(textItem);
                i--;
            }
        }
    }

    private void OnDisable()
    {
        TieckerDataManager.TableUpdated -= OnTableUpdated;
    }
}

