﻿using TMPro;
using UnityEngine;

public class TickerItem : MonoBehaviour
{
    private float _width;
    private float _pixelPerSecond;
    private RectTransform _rectTransform;

    public float GetXPosition { get { return _rectTransform.anchoredPosition.x; } }
    public float Width { get { return _rectTransform.rect.width; } }

    public void Initialize(float width, float pixelsPerSecond, string message)
    {
        _width = width;
        _pixelPerSecond = pixelsPerSecond;
        _rectTransform = GetComponent<RectTransform>();
        GetComponent<TMP_Text>().text = message;
        _rectTransform.anchoredPosition = new Vector2(_width, 0f);
    }

    private void Update()
    {
        _rectTransform.position += Vector3.right * _pixelPerSecond * Time.deltaTime;

        if (GetXPosition >= _width + Width)
        {
            Destroy(gameObject);
        }
    }
}
