using UnityEngine;

public class Animation
{
    private string name;

    private State state;

    private Transform character;

    public Animation(string name, State state, Transform character)
    {
        this.name = name;

        this.state = state;

        this.character = character;
    }

    public void playAnimation(string name)
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
}
