using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
   bool audio = false;
   public AudioSource SFX;
   public CharacterController controller;
   public float speed = 6f;
   public float turnSmoothTime = 0.1f;
   float turnSmoothVelocity;
    void Update()
    {
        float horizantal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizantal, 0f, vertical).normalized;
    
        if (direction.magnitude >= 0.1f)
        {
            if(audio == false){
                audio = true;
                SFX.Play();
            } 
          
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        } else {
            audio = false;
            SFX.Stop();
        }
    }

}
