using UnityEngine;

public class PotBreak : MonoBehaviour
{
    public Rigidbody test;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");
    }
}
