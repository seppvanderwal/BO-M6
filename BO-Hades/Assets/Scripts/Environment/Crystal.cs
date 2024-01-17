using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private float height = .2f;
    [SerializeField] private float floatSpeed = 2;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float floatIncrement = 0.05f;
    [SerializeField] private float distance = 3f;

    internal Transform character;

    private Vector3 originalPos;

    private bool idle = true;
    private float currentIncrement;

    private void Start()
    {
        originalPos = transform.position;

        currentIncrement = floatIncrement;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            character = collider.transform;
            idle = false;

            State state = character.GetComponent<State>();
            state.cast.cooldown = false;

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!idle)
        {
            transform.LookAt(character.position + new Vector3(0, .6f, 0));
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
        }
        else
        {
            if (transform.position.y >= originalPos.y + height)
            {
                currentIncrement = -floatIncrement;
            }
            else if (transform.position.y <= originalPos.y - height)
            {
                currentIncrement = floatIncrement;
            }

            transform.position += new Vector3(0, currentIncrement * floatSpeed * Time.deltaTime, 0);

            float dis = Vector3.Distance(character.position, originalPos);

            if (dis <= distance)
            {
                idle = false;
            }
        }
    }
}
