using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float castSpeed = 5f;
    public GhostHit Ghosthit;
    internal bool ranged;
    internal bool removed;

    internal float lifetime;
    internal float damage;
    internal float timer;

    internal Vector3 direction;

    internal Transform character;

    internal string type;

    private static void SpawnCrystal(Hitbox hitbox)
    {
        if (hitbox.type == "Ranged")
        {
            hitbox.removed = true;

            Transform crystal = Instantiate(Resources.Load<Transform>(@"Assets/Cast/Crystal"));
            crystal.position = hitbox.transform.position;

            Crystal crystalComponent = crystal.GetComponent<Crystal>();
            crystalComponent.character = hitbox.character;

            /*
            crystal.AddComponent<Crystal>();
            */

            Destroy(hitbox.gameObject);
        }
    }


    public static void SpawnHitbox(string name, string type, Transform character, Transform spawnpoint, float lifetime, float damage)
    {
        Transform hitbox = Instantiate(Resources.Load<Transform>(@$"Hitboxes/{name}"));

        hitbox.tag = "Attack";
        hitbox.position = spawnpoint.position;
        hitbox.LookAt(spawnpoint.forward * 100);
        hitbox.Rotate(new(0, 180, 0));

        Hitbox component = hitbox.AddComponent<Hitbox>();

        component.ranged = false;
        component.removed = false;

        component.damage = damage;
        component.lifetime = lifetime;
        component.timer = 0;

        component.direction = spawnpoint.forward;

        component.type = type;

        if (character)
        {
            component.character = character;
        }

        if (type == "Melee")
        {
            Destroy(hitbox.gameObject, lifetime);
        }
        else if (type == "Ranged")
        {
            component.ranged = true;
            component.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            return;
        }

        if (collider.CompareTag("Pot"))
        {
            Debug.Log(collider);
            Transform pot = collider.transform;
            Animator potAnimator = collider.GetComponent<Animator>();

            potAnimator.SetBool("broken", true);
            pot.position = new Vector3(pot.position.x, pot.position.y, pot.position.z - 1f);

            collider.enabled = false;
            SpawnCrystal(this);
        }
        else if (collider.CompareTag("Ghost"))
        {
            GhostHit Ghosthit = collider.GetComponent<GhostHit>();
            Ghosthit.hit = true;

            SpawnCrystal(this);
        }
    }
    private void Update()
    {
        if (!ranged) { return; }

        if (timer >= lifetime && !removed)
        {
            SpawnCrystal(this);
        }
        else
        {
            timer += Time.deltaTime;
            transform.position += direction * Time.deltaTime * castSpeed;
        }
    }
}

