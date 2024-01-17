using System.Collections;
using UnityEngine;

public class GhostFade : MonoBehaviour
{
    private ObjectFader _fader;

    void Start()
    {
        _fader = gameObject.GetComponent<ObjectFader>();
    }
    internal IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        _fader.DoFade = false;
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


    // Update is called once per frame
    void Update()
    {

    }
}
