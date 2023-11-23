using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   public CharacterController controller;

   public float speed = 6f;
    void Update()
    {
        float horizantal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizantal, 0f, vertical).normalized;
    
        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngel, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
    }

}
