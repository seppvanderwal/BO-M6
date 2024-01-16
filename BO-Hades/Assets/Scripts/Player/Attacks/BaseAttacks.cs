using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BaseAttacks : MonoBehaviour
{
    public static List<Special> Specials = new();

    public static Attack CurrentAttack;

    public List<float> combo;

    private State UserStates;
    private Audio UserAudio;

    private void Start()
    {
        UserStates = GetComponent<State>();
        UserAudio = GetComponent<Audio>();

        UserAudio.SetSounds(new()
        {
            {"SpecialQ", 0},
            {"Attack1", 1},
            {"Attack2", 1},
            {"Attack3", 2}
        });
    }

    void Update()
    {
        if (Regex.IsMatch(UserStates.GetState(), "Special")) { return; }

        foreach (var special in Specials)
        {
            if (Input.GetKeyDown(special.key))
            {
                StartCoroutine(special.Fire());
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        Attack attack = CurrentAttack;

        if (attack.canAttack)
        {
            attack.canAttack = false;

            attack.playAnimation(null);
            UserAudio.Play(attack.name);

            yield return new WaitForSeconds(.4f);

            Hitbox.SpawnHitbox("Attack", transform.Find("meleepoint").position, .3f);

            if (attack.index < attack.max)
            {
                attack.NextAttack();
            }

            yield return new WaitForSeconds(attack.cooldown);

            if (CurrentAttack.canAttack && (attack.index + 1) == CurrentAttack.index)
            {
                attack.resetCombo();
            }

            if (attack.index == attack.max)
            {
                if (attack == CurrentAttack)
                {
                    attack.NextAttack();
                }
            }
        }
    }
}
