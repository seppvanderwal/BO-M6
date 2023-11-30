using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject special;
    public Transform specialpoint;
    private float cooldown = 30f;
    bool canSpecial = true;

    internal IEnumerator cd()
    {
        canSpecial = false;
        yield return new WaitForSeconds(cooldown);
        canSpecial = true;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canSpecial == true)
        {
           GameObject specialInstance = Instantiate(special, specialpoint);
            StartCoroutine(cd());
           Destroy(specialInstance, 0.2f);
        }
    }
}
