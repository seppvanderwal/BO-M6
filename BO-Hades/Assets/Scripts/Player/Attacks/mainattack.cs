using System.Collections;
using UnityEngine;

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
            GameObject meleeInstance = Instantiate(melee, meleepoint);
            StartCoroutine(cd());
            Destroy(meleeInstance, 0.2f);
        }
    }
}
