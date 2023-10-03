using UnityEngine;
using UnityEngine.UI;

public class CircleImageChanged: MonoBehaviour
{
    private Image _image;
    [SerializeField] private Color _color;
    private Button _button;

    [SerializeField] private Image _circle;
    [SerializeField] private Image _rect;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.Add(ColorChange);
    }

    private void ColorChange()
    {
        _circle.sprite = _image.sprite;
        _rect.color = _color;
    }
}
