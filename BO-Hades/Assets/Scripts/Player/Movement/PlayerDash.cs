using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    CharacterController controller;
    public float dashSpeed;
    public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            // Calculate movement during the dash directly in this script
            Vector3 dashDirection = GetInputDirection();
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    Vector3 GetInputDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }
}
