using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class mainattack : MonoBehaviour
{
    public GameObject melee;
    public Transform meleepoint;

    private float cooldown = 3f;
    private bool canAttck = true;



    internal IEnumerator cd()
    {
        canAttck = false;
        yield return new WaitForSeconds(cooldown);

        canAttck = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttck == true)
        {
            Instantiate(melee, meleepoint);
            StartCoroutine(cd());
        }
    }
}
