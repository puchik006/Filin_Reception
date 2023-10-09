using UnityEngine;

public class ActivateAllDisplays : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}

