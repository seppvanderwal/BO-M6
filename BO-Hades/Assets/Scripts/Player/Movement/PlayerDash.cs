using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    CharacterController controller;
    public float dashSpeed;
    public float dashTime;
    private bool isDashing;
    private bool canDash = true;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            // Calculate movement during the dash directly in this script
            Vector3 dashDirection = GetInputDirection();
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);

            yield return null;
        }

        // Wait for the dash time
        yield return new WaitForSeconds(dashingTime);


        // Wait for the dash cooldown and enable dashing again
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        isDashing = false;

        yield break;
    }

    Vector3 GetInputDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }
}