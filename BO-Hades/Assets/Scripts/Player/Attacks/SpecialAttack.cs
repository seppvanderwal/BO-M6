using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public Movement movement;
    public GameObject special;
    public Transform specialpoint;
    public AudioSource SFX;
    private float cooldown = 1.9f;
    bool canSpecial = true;
    
    
    
    internal IEnumerator cd()
    {
        
        yield return new WaitForSeconds(cooldown);
        

    }
    internal IEnumerator attack()
    {
        SFX.Play();
        movement.enabled = false;
        yield return new WaitForSeconds(1f);
        Special();
        canSpecial = true;
        movement.enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canSpecial == true)
        {
            canSpecial = false;
            StartCoroutine(attack());
            
        }
    }
    void Special()
    {
        GameObject specialInstance = Instantiate(special, specialpoint);
        StartCoroutine(cd());
        Destroy(specialInstance, 0.2f);
        
    }
}
