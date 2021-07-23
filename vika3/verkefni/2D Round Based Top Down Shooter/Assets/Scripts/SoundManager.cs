using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    AudioClip shootSound;

    [Range(0f, 1f)]
    [SerializeField]
    float shootSoundVolume;

    [SerializeField]
    AudioClip hitSound1;

    [SerializeField]
    AudioClip hitSound2;

    [Range(0f, 1f)]
    [SerializeField]
    float hitSoundVolume;

    [SerializeField]
    AudioClip reloadSound;

    [Range(0f, 1f)]
    [SerializeField]
    float reloadSoundVolume;

    [SerializeField]
    AudioClip noAmmoSound;

    [Range(0f, 1f)]
    [SerializeField]
    float noAmmoSoundVolume;

    [SerializeField]
    AudioClip successSound;

    [Range(0f, 1f)]
    [SerializeField]
    float successSoundVolume;

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

    public void PlayShootSound()
    {
        PlaySound(GetAvailableAudioSource(), shootSound, shootSoundVolume);
    }

    public void PlayHitSound()
    {
        PlaySound(GetAvailableAudioSource(), Random.Range(0, 2) == 0 ? hitSound1 : hitSound2, hitSoundVolume);
    }

    public void PlayReloadSound()
    {
        PlaySound(GetAvailableAudioSource(), reloadSound, reloadSoundVolume);
    }

    public void PlayNoAmmoSound()
    {
        PlaySound(GetAvailableAudioSource(), noAmmoSound, noAmmoSoundVolume);
    }

    public void PlaySuccessSound()
    {
        PlaySound(GetAvailableAudioSource(), successSound, successSoundVolume);
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