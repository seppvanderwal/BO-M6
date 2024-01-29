using System.Collections;
using UnityEngine;

public class GhostHit : MonoBehaviour
{
    public ParticleSystem Particle;
    public bool hit = false;

    internal IEnumerator Die()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
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
