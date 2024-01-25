using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    public float dashTime;
    public float dashingCooldown = 1f;

    bool inCooldown = false;

    public AudioSource dashAudioSource;

    private CharacterController Controller;

    private State UserStates;

    // Start is called before the first frame update
    void Start()
    {
        UserStates = GetComponent<State>();
        Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string state = UserStates.GetState();

            if (state == "Idle" || state == "Walking")
            {
                //dashAudioSource.Play();
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
        if (!inCooldown)
        {
            inCooldown = true;

            float startTime = Time.time;

            UserStates.ChangeState("Dashing");

            while (Time.time < startTime + dashTime)
            {
                // Calculate movement during the dash directly in this script
                Vector3 dashDirection = transform.forward;
                Controller.Move(dashDirection * dashSpeed * Time.deltaTime);

                yield return null;
            }

            UserStates.ChangeState("Idle");

            // Wait for the dash cooldown and enable dashing again
            yield return new WaitForSeconds(dashingCooldown);

            inCooldown = false;

            yield break;
        }
    }
}
