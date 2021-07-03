using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Score sound
    [SerializeField]
    AudioClip scoreSound;

    [Range(0f, 1f)][SerializeField] 
    float scoreSoundVolume;

    // Hit sound
    [SerializeField] 
    AudioClip hitSound;

    [Range(0f, 1f)][SerializeField] 
    float hitSoundVolume;

    // Death sound
    [SerializeField] 
    AudioClip deathSound;

    [Range(0f, 1f)][SerializeField] 
    float deathSoundVolume;

    List<AudioSource> sources = new List<AudioSource>();

    void Awake()
    {
        var objects = GameObject.FindGameObjectsWithTag("SoundManager");
        if (objects.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        for (int i = 0; i < 5; i++) sources.Add(gameObject.AddComponent<AudioSource>());
    }

    public void PlayScoreSound()
    {
        PlaySound(GetAvailableAudioSource(), scoreSound, scoreSoundVolume);
    }

    public void PlayHitSound()
    {
        PlaySound(GetAvailableAudioSource(), hitSound, hitSoundVolume);
    }

    public void PlayDeathSound()
    {
        PlaySound(GetAvailableAudioSource(), deathSound, deathSoundVolume);
    }

    AudioSource GetAvailableAudioSource()
    {
        for (int i = 0; i < sources.Count; i++)
            if (!sources[i].isPlaying) return sources[i];
        var newSource = gameObject.AddComponent<AudioSource>();
        sources.Add(newSource);
        return newSource;
    }

    void PlaySound(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }
}
