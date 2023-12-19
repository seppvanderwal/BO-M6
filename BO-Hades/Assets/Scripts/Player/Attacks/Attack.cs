using System.Collections.Generic;
using UnityEngine;

public class Attack : Animation
{
    public static Dictionary<Transform, Dictionary<int, Attack>> Attacks = new();

    internal string name;

    internal State state;

    internal int index;
    internal int max;

    internal bool canAttack;

    internal Transform character;
    internal float cooldown;

    internal Attack(string name, State state, Transform character, int index) : base(name, state, character)
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
            BaseAttacks.CurrentAttack = this;
            canAttack = true;
        }

        Attacks[character].Add(index, this);
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

                    BaseAttacks.CurrentAttack = attack;
                    attack.canAttack = true;

                    attack.playAnimation("Idle");
                }
            }
            else
            {
                if (attack.index == (index + 1))
                {
                    canAttack = false;

                    BaseAttacks.CurrentAttack = attack;
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

                BaseAttacks.CurrentAttack = attack;
                attack.canAttack = true;

                attack.playAnimation("Idle");

                break;
            }
        }
    }
}
