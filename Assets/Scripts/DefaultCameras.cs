using System.Collections.Generic;
using UnityEngine;

public class DefaultCameras : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sets;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < _sets.Count; i++)
            {
                CameraDisplaySwitcher displaySwitcher = _sets[i].GetComponent<CameraDisplaySwitcher>();
                if (displaySwitcher != null)
                {
                    displaySwitcher.SwitchCameraDisplay(i);
                }
                else
                {
                    Debug.LogWarning("CameraDisplaySwitcher component not found on camera " + _sets[i].name);
                }
            }
        }
    }
}
