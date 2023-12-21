using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public static void SpawnHitbox(string name, Vector3 meleepoint)
    {
        Transform hitbox = Instantiate(Resources.Load<Transform>(@$"Hitboxes/{name}"));

        hitbox.tag = "Attack";

        hitbox.position = meleepoint;

        hitbox.AddComponent<Hitbox>();

        Destroy(hitbox.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Pot"))
        {
            Transform pot = collider.transform;
            Animator potAnimator = collider.GetComponent<Animator>();

            potAnimator.SetBool("broken", true);
            pot.position = new Vector3(pot.position.x, pot.position.y, pot.position.z - 1f);

            collider.enabled = false;
        }
    }
}
