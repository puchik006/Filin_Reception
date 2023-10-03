using TMPro;
using UnityEngine;

public class ColorTextChanger : MonoBehaviour
{
    public TextType Type;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        ChangeColorButton.ColorChanged += OnColorChanged;
    }

    private void OnColorChanged(TextType type, Color color)
    {
        if (Type == type)
        {
            _text.color = color;
        }
    }

    private void OnDisable()
    {
        ChangeColorButton.ColorChanged -= OnColorChanged;
    }
}



