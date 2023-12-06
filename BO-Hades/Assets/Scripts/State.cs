using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class State : MonoBehaviour
{
    private static Dictionary<Transform, List<string>> animations = new();
    private static Dictionary<Transform, string> state = new();

    internal Animator animator;

    internal bool boolean = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        var animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

        foreach (var layer in animatorController.layers)
        {
            animations.Add(transform, new(layer.stateMachine.states.Length));

            foreach (var animationState in layer.stateMachine.states)
            {
                string state = animationState.state.name;

                if (Regex.IsMatch(state, "Attack"))
                {
                    int index = int.Parse(Regex.Match(animationState.state.name, @"\d+").Value) - 1;

                    new Attack(state, index, transform, this);
                }

                animations[transform].Add(state);
            }
        }

        state.Add(transform, "Idle");
    }

    internal void ChangeState(Transform character, string newState)
    {
        if (state[character] == newState) { return; }

        bool inAnimations = false;

        foreach (string checkState in animations[character])
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
            state[character] = newState;

            PlayAnimation(character);
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
