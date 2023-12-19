using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public List<AudioClip> Clips;

    private AudioSource SFX;

    private Dictionary<string, AudioClip> namelater = new();

    public void SetSounds(Dictionary<string, int> a)
    {
        SFX = GetComponent<AudioSource>();

        foreach (var kv in a)
        {
            namelater.Add(kv.Key, Clips[kv.Value]);
        }
    }

    public void Play(string name)
    {
        if (namelater[name])
        {
            AudioClip clip = namelater[name];

            SFX.clip = clip;

            SFX.Play();
        }
    }
}
