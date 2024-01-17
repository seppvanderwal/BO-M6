using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : Animation
{
    internal static Dictionary<Transform, bool> inSpecial = new();

    internal string name;

    internal State state;

    internal Transform character;

    internal KeyCode key;

    internal float attackTime;
    internal float cooldown;

    public bool inCooldown;

    public bool Firing;

    private Audio UserAudio;

    public Special(string name, State state, Transform character, KeyCode key) : base(name, state, character)
    {
        this.name = name;

        this.state = state;

        this.character = character;

        this.key = key;

        this.attackTime = .8f;
        this.cooldown = 5f;

        UserAudio = character.GetComponent<Audio>();

        inCooldown = false;
        Firing = false;

        BaseAttacks.Specials.Add(this);
    }

    public IEnumerator Fire()
    {
        if (!Firing && !inCooldown)
        {
            inCooldown = true;
            Firing = true;

            UserAudio.Play(name);
            playAnimation(name);

            yield return new WaitForSeconds(attackTime);

            Hitbox.SpawnHitbox("Special", "Melee", character, character.Find("meleepoint"), .6f, 20);
            Firing = false;

            yield return new WaitForSeconds(.25f);

            playAnimation("Idle");

            yield return new WaitForSeconds(cooldown);

            inCooldown = false;
        }
    }
}
