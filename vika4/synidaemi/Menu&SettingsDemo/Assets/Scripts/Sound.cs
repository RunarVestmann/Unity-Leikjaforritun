using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch = 1f;

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]
    public bool isFadingOut;

    [HideInInspector]
    public bool isFadingIn;

    [HideInInspector]
    public Coroutine routine;
}
