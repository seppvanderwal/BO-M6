using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float lifetime = 1;

    public float height = .2f;
    public float speed = 2;
    public float moveSpeed = 30;
    public float increment = 0.05f;

    private Vector3 originalPos;

    private bool idle = true;

    private float currentIncrement;
    private float timer = 0;

    private Transform character;

    private void Start()
    {
        originalPos = transform.position;

        currentIncrement = increment;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            character = collider.transform;
            idle = false;

            State state = character.GetComponent<State>();
            state.cast.cooldown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!idle)
        {
            if (timer >= lifetime)
            {
                Destroy(gameObject);
            }

            timer += Time.deltaTime;

            transform.localScale -= new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime * moveSpeed;
        }
        else
        {
            if (transform.position.y >= originalPos.y + height)
            {
                currentIncrement = -increment;
            }
            else if (transform.position.y <= originalPos.y - height)
            {
                currentIncrement = increment;
            }

            transform.position += new Vector3(0, currentIncrement * speed * Time.deltaTime, 0);
        }

        /*
        if (Vector3.Distance(character.position, originalPos) <= distance)
        {
            idle = false;
        }*/
    }
}
