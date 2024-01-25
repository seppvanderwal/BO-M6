using System.Collections;
using UnityEngine;

public class UIDialog : MonoBehaviour
{
    public GameObject firstLine;
    public GameObject secondLine;
    internal IEnumerator Dialog()
    {
        yield return new WaitForSeconds(2f);
        firstLine.SetActive(false);
        yield return new WaitForSeconds(10f);
        secondLine.SetActive(true);
        yield return new WaitForSeconds(3f);
        secondLine.SetActive(false);
    }
    void Start()
    {
        secondLine.SetActive(false);
        StartCoroutine(Dialog());
    }
}
