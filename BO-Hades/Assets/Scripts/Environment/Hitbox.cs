using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public GhostHit Ghosthit;

    public static void SpawnHitbox(string name, Vector3 meleepoint, float lifetime)
    {
        Transform hitbox = Instantiate(Resources.Load<Transform>(@$"Hitboxes/{name}"));

        hitbox.tag = "Attack";

        hitbox.position = meleepoint;

        hitbox.AddComponent<Hitbox>();

        Destroy(hitbox.gameObject, lifetime);

    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.CompareTag("Pot"))
        {
            Debug.Log(collider);
            Transform pot = collider.transform;
            Animator potAnimator = collider.GetComponent<Animator>();

            potAnimator.SetBool("broken", true);
            pot.position = new Vector3(pot.position.x, pot.position.y, pot.position.z - 1f);

            collider.enabled = false;
        }
        if (collider.CompareTag("Ghost"))
        {
            GhostHit Ghosthit = collider.GetComponent<GhostHit>();
            Ghosthit.hit = true;

        }
    }
}
