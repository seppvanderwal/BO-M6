using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private CharacterController Controller;

    private State UserStates;
    private Audio UserAudio;

    private void Start()
    {
        UserStates = GetComponent<State>();
        UserAudio = GetComponent<Audio>();
        Controller = GetComponent<CharacterController>();

        UserAudio.SetSounds(new()
        {
            {"Walking", 3},
        });
    }
    void Update()
    {
        /*
        float horizantal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizantal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }*/

        // Base Code for walking

        string state = UserStates.GetState();

        if (state == "Idle" || state == "Walking")
        {
            float horizantal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizantal, 0f, vertical).normalized;

            if (direction.magnitude < .1f)
            {
                UserStates.ChangeState("Idle");
                UserAudio.Play(null);
            }
            else
            {
                if (state != "Walking")
                {
                    UserStates.ChangeState("Walking");
                    UserAudio.Play("Walking");
                }

                float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Controller.Move(direction * speed * Time.deltaTime);
            }
        }
    }
}
