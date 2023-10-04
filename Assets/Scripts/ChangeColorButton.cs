using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton: MonoBehaviour
{
    public TextType Type;
    private Button _button;
    private Image _image;

    public static Action<TextType, Color> ColorChanged;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _button.Add(() => ColorChanged?.Invoke(Type,_image.color));
    }
}