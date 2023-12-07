using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttacks : MonoBehaviour
{
    public static Dictionary<string, List<Special>> Specials = new();

    public static Attack currentAttack;

    public Transform meleepoint;

    public List<float> combo;

    //private bool pressing = false;

    void Update()
    {
        foreach (var special in Specials)
        {
            if (Input.GetKeyDown(special.key))
            {
                Debug.Log("key has been pressed");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Fire());
        }

        /*
        // Test

        if (Input.GetKeyUp(KeyCode.W))
        {
            pressing = false;

            State UserStates = GetComponent<State>();

            string state = UserStates.GetState();

            if (state == "Idle" || state == "Walking")
            {
                UserStates.ChangeState(transform, "Idle");
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || pressing)
        {
            pressing = true;

            State UserStates = GetComponent<State>();

            string state = UserStates.GetState();

            if (state == "Idle" || state == "Walking")
            {
                Debug.Log("moving");
                UserStates.ChangeState(transform, "Walking");
            }
        }*/
    }

    private IEnumerator Fire()
    {
        Attack attack = currentAttack;

        if (attack.canAttack)
        {
            attack.canAttack = false;

            attack.playAnimation(null);

            if (attack.index < attack.max)
            {
                yield return new WaitForSeconds(.3f);
                attack.NextAttack();
            }

            yield return new WaitForSeconds(attack.cooldown);

            if (currentAttack.canAttack && (attack.index + 1) == currentAttack.index)
            {
                attack.resetCombo();
            }

            if (attack.index == attack.max)
            {
                if (attack == currentAttack)
                {
                    attack.NextAttack();
                }
            }
        }
    }

    /*
    private IEnumerator Fire_Special()
    {

    }*/
}

public class Attack
{
    public static Dictionary<Transform, Dictionary<int, Attack>> Attacks = new();

    internal string name;

    internal State state;

    internal int index;
    internal int max;

    internal bool canAttack;

    internal Transform character;
    internal float cooldown;

    internal Attack(string name, int index, Transform character, State state)
    {
        List<float> combo = character.GetComponent<BaseAttacks>().combo;

        this.name = name;

        this.state = state;

        this.index = index;
        this.max = combo.Count - 1;

        this.canAttack = false;

        this.character = character;

        this.cooldown = combo[index];

        Dictionary<int, Attack> result;

        Attacks.TryGetValue(character, out result);

        if (result == null)
        {
            Attacks.Add(character, new());
        }

        if (this.index == 0)
        {
            BaseAttacks.currentAttack = this;
            canAttack = true;
        }

        Attacks[character].Add(index, this);
    }

    internal void playAnimation(string name)
    {
        if (name != null)
        {
            state.ChangeState(character, name);
        }
        else
        {
            state.ChangeState(character, this.name);
        }
    }

    internal void NextAttack()
    {
        foreach (var kv in Attacks[character])
        {
            Attack attack = kv.Value;

            if (index == max)
            {
                if (attack.index == 0)
                {
                    canAttack = false;

                    BaseAttacks.currentAttack = attack;
                    attack.canAttack = true;

                    attack.playAnimation("Idle");
                }
            }
            else
            {
                if (attack.index == (index + 1))
                {
                    canAttack = false;

                    BaseAttacks.currentAttack = attack;
                    attack.canAttack = true;
                    break;
                }
            }
        }
    }

    internal void resetCombo()
    {
        foreach (var kv in Attacks[character])
        {
            Attack attack = kv.Value;

            if (attack.index == 0)
            {
                canAttack = false;

                BaseAttacks.currentAttack = attack;
                attack.canAttack = true;

                attack.playAnimation("Idle");

                break;
            }
        }
    }
}

public class Special
{
    internal static Dictionary<Transform, bool> inSpecial = new();

    internal string name;

    internal Transform character;

    internal KeyCode key;

    internal Special(string Name, Transform character, KeyCode key)
    {
        this.name = Name;

        this.character = character;

        this.key = key;

        Dictionary<string, Special> result;

        BaseAttacks.Specials.TryGetValue(name, out result);

        if (result == null)
        {
            Attacks.Add(character, new());
        }

    }
}

