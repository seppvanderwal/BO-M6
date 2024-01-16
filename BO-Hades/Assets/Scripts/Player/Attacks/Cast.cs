using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : Animation
{
    internal static Dictionary<Transform, bool> inSpecial = new();

    internal string name;

    internal State state;

    internal Transform character;

    internal KeyCode key;

    internal float attackTime;

    internal bool cooldown;

    private Audio UserAudio;

    public Cast(string name, State state, Transform character, KeyCode key) : base(name, state, character)
    {
        this.name = name;

        this.state = state;

        this.character = character;

        this.key = key;

        this.cooldown = false;

        this.attackTime = .5f;

        UserAudio = character.GetComponent<Audio>();

        BaseAttacks.CurrentCast = this;
    }

    public IEnumerator Fire()
    {
        if (!cooldown)
        {
            cooldown = true;

            UserAudio.Play(name);
            playAnimation(name);

            yield return new WaitForSeconds(attackTime);

            Hitbox.SpawnHitbox("Cast", "Ranged", character.Find("meleepoint"), 1.2f, 30);

            yield return new WaitForSeconds(.25f);

            playAnimation("Idle");
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Hitbox.SpawnHitbox("Cast", "Ranged");
        }
    }*/
}
