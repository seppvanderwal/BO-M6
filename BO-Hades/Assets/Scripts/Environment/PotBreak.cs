using UnityEngine;

public class PotBreak : MonoBehaviour
{
    public Animator Animation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Debug.Log(collision.gameObject.name);
            Animation.SetBool("broken", true);
            pot.position = new Vector3(pot.position.x, pot.position.y, pot.position.z - 1f);
        }
    }*/
}
