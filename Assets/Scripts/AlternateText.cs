using System.Collections;
using TMPro;
using UnityEngine;

public class AlternateText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int currentPage = 1;

    private void Start()
    {
        _text.pageToDisplay = currentPage;
        StartCoroutine(AutoChangePage());
    }

    private IEnumerator AutoChangePage()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            currentPage++;
            if (currentPage > _text.textInfo.pageCount) currentPage = 1;
            _text.pageToDisplay = currentPage;
        }
    }
}
