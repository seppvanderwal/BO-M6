using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class State : MonoBehaviour
{
    private static Dictionary<Transform, List<string>> animations = new();
    private static Dictionary<Transform, string> state = new();

    private Animator animator;

    public Cast cast;

    void Start()
    {
        animator = GetComponent<Animator>();

        var animatorController = animator.runtimeAnimatorController;

        animations.Add(transform, new());
        state.Add(transform, "");

        var states = new string[]{
            "Idle",
            "Attack1",
            "Attack2",
            "Attack3",
            "SpecialQ",
            "Walking",
            "Dashing",
            "Cast",
            "Placeholder Idle",
            "metarig|metarigAction_002"
        };

        foreach (var state in states)
        {
            if (Regex.IsMatch(state, "Attack"))
            {
                int index = int.Parse(Regex.Match(state, @"\d+").Value) - 1;

                new Attack(state, this, transform, index);
            }
            else if (Regex.IsMatch(state, "Special"))
            {
                string lastDigit = state.Substring(state.Length - 1);
                KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), lastDigit);

                new Special(state, this, transform, key);
            }
            else if (Regex.IsMatch(state, "Cast"))
            {
                cast = new Cast(state, this, transform, KeyCode.E);
            }

            animations[transform].Add(state);
        }

        ChangeState("Idle");
    }

    internal void ChangeState(string newState)
    {
        if (state[transform] == newState) { return; }

        bool inAnimations = false;

        foreach (string checkState in animations[transform])
        {
            if (checkState == newState)
            {
                inAnimations = true;

                break;
            }
        }

        if (!inAnimations)
        {
            Debug.LogWarning($"Cannot find '{newState}' in animation layers");
        }
        else
        {
            state[transform] = newState;

            PlayAnimation(transform);
        }
    }

    internal string GetState()
    {
        return state[transform];
    }

    private void PlayAnimation(Transform character)
    {
        animator.CrossFade(state[character], .2f);
    }
}
