using System.Collections;
using UnityEngine;

public class GhostFade : MonoBehaviour
{
    private ObjectFader _fader;
    internal IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        _fader.DoFade = false;
    }

    void Start()
    {
        _fader = gameObject.GetComponent<ObjectFader>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _fader.DoFade = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeOut());

        }
    }
}
