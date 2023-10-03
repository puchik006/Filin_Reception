using UnityEngine;
using UnityEngine.UI;

public class ColorImageChanger : MonoBehaviour
{
    public TextType Type;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnColorChanged(TextType type, Color color)
    {
        if (Type == type)
        {
            _image.color = color;
        }
    }

    private void OnDisable()
    {
        ChangeColorButton.ColorChanged -= OnColorChanged;
    }
} 



