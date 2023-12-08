using System.Collections;
using UnityEngine;

public class WeekViewManager : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    

    private void Start()
    {
        // Start the coroutine when the script is first initialized
        StartCoroutine(ToggleContent());
    }

    private IEnumerator ToggleContent()
    {
        while (true)
        {
            // Wait for 10 seconds
            yield return new WaitForSeconds(10f);

            // Toggle the active state of the GameObject
            if (_content != null)
            {
                _content.SetActive(!_content.activeSelf);
            }
        }
    }
}
