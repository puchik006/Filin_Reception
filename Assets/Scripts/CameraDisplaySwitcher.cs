using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDisplaySwitcher : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List<Button> _buttons;

    private void Awake()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int displayIndex = i;
            _buttons[i].onClick.AddListener(() => SwitchCameraDisplay(displayIndex));
        }

        if (PlayerPrefs.HasKey(_camera.name))
        {
            SwitchCameraDisplay(PlayerPrefs.GetInt(_camera.name));
        }
    }

    public void SwitchCameraDisplay(int displayIndex)
    {
        _camera.targetDisplay = displayIndex;
        _buttons.ForEach(e => e.GetComponent<Image>().color = Color.white);
        _buttons[displayIndex].GetComponent<Image>().color = Color.yellow;

        PlayerPrefs.SetInt(_camera.name, displayIndex);
    }
}
