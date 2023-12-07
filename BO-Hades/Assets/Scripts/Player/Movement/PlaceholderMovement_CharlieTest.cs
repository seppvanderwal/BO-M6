using UnityEngine;
public class PlaceholderMovement_CharlieTest : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var velocity = Vector3.forward * Input.GetAxis("Vertical") * speed;
        transform.Translate(velocity * Time.deltaTime);
        animator.SetFloat("Speed", velocity.magnitude);
    }
}