using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    public float dashTime;
    public float dashingCooldown = 1f;

    bool inCooldown = false;

    private CharacterController Controller;
    private State UserStates;
    private Audio UserAudio;

    private GameObject dashParticle;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        UserStates = GetComponent<State>();
        UserAudio = GetComponent<Audio>();

        UserAudio.SetSounds(new()
        {
            {"Dashing", 5},
        });

        dashParticle = transform.Find("DashParticle").gameObject;
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

            UserAudio.Play("Dashing");
            UserStates.ChangeState("Dashing");

            dashParticle.SetActive(true);
            dashParticle.GetComponent<ParticleSystem>().Play();

            while (Time.time < startTime + dashTime)
            {
                // Calculate movement during the dash directly in this script
                Vector3 dashDirection = transform.forward;
                Controller.Move(dashDirection * dashSpeed * Time.deltaTime);

                if (Time.time >= startTime + dashTime / 1.3)
                {
                    dashParticle.GetComponent<ParticleSystem>().Stop();
                }


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
