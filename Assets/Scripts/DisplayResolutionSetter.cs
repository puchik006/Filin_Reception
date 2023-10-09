using UnityEngine;

public class DisplayResolutionSetter : MonoBehaviour
{
    [SerializeField] private int display1Width = 2160;
    [SerializeField] private int display1Height = 3840;
    [SerializeField] private int display2Width = 1920;
    [SerializeField] private int display2Height = 1080;
    [SerializeField] private int display3Width = 2160;
    [SerializeField] private int display3Height = 3840;

    private void Start()
    {
        // Check if there are enough displays available
        if (Display.displays.Length >= 3)
        {
            // Set the resolution for Display 1
            Display.displays[0].Activate();
            Screen.SetResolution(display1Width, display1Height, FullScreenMode.FullScreenWindow);

            // Set the resolution for Display 2
            Display.displays[1].Activate();
            Screen.SetResolution(display2Width, display2Height, FullScreenMode.FullScreenWindow);

            // Set the resolution for Display 3
            Display.displays[2].Activate();
            Screen.SetResolution(display3Width, display3Height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Debug.LogError("Not enough displays available to set resolutions for all three displays.");
        }
    }
}

