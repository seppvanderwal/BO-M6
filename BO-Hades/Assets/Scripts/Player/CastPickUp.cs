using UnityEngine;

public class CastPickUp : MonoBehaviour
{
    public UICast castUI;



    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Crystal")
        {

            castUI.castAmmo = 1;
        }
    }
}
