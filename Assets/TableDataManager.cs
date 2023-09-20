using UnityEngine;

public class TableDataManager: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _stringPrefab;

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddString();
        }
    }

    private void AddString()
    {
        Instantiate(_stringPrefab, _content);
    }
}
