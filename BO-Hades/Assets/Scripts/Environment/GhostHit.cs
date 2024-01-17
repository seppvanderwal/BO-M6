using System.Collections;
using UnityEngine;

public class GhostHit : MonoBehaviour
{
    public ParticleSystem Particle;
    public bool hit = false;
    private void Start()
    {

    }
    internal IEnumerator Die()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Particle.Play();
        yield return new WaitForSeconds(1f);
        Particle.Stop();
    }
    void Update()
    {
        if (hit == true)
        {
            StartCoroutine(Die());
            Debug.Log("test");
            hit = false;
        }
    }
}
