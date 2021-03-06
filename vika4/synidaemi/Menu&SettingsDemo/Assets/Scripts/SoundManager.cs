using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    void Awake()
    {
        bool onlyOneSoundManagerActive = SetUpSingleton();
        if (!onlyOneSoundManagerActive) return;
        SetUpSounds();
        OnVolumeChange(Settings.VolumeScale);
    }

    bool SetUpSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return false;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        return true;
    }

    void SetUpSounds()
    {
        foreach (var sound in sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            source.playOnAwake = sound.playOnAwake;
            if (sound.playOnAwake) source.Play();
            sources.Add(sound.name, source);
        }
    }

    public void OnVolumeChange(float volumeScale)
    {
        foreach (var sound in sounds)
        {
            if (sound.isFadingOut)
            {
                StopCoroutine(sound.routine);
                sound.isFadingOut = false;
                sources[sound.name].Stop();
            }
            else
            {
                if (sound.isFadingIn)
                {
                    StopCoroutine(sound.routine);
                    sound.isFadingIn = false;
                }
                sources[sound.name].volume = sound.volume * volumeScale;
            }
        }
    }

    public void PlaySound(string name)
    {
        if (!sources.ContainsKey(name))
        {
            Debug.LogWarning($"Could not find sound with name: {name}");
            return;
        }
        sources[name].Play();
    }

    public void StopSound(string name)
    {
        if (!sources.ContainsKey(name))
        {
            Debug.LogWarning($"Could not find sound with name: {name}");
            return;
        }
        sources[name].Stop();
    }

    public void StopAllSounds()
    {
        foreach (var source in sources.Values) source.Stop();
    }

    public void FadeInSound(string name, float time) => FadeSound(name, time, true);
    public void FadeOutSound(string name, float time) => FadeSound(name, time, false);

    void FadeSound(string name, float time, bool fadeIn)
    {
        if (!sources.ContainsKey(name))
        {
            Debug.LogWarning($"Could not find sound with name: {name} in dictionary");
            return;
        }
        float totalVolume = -1f;
        Sound sound = null;
        for (int i = 0; i < sounds.Length; i++)
            if (sounds[i].name == name)
            {
                sound = sounds[i];
                totalVolume = sounds[i].volume;
                break;
            }

        if (totalVolume == -1f)
        {
            Debug.LogWarning($"Could not find sound with name: {name} in sounds array");
            return;
        }

        Coroutine routine = null;
        if (fadeIn) routine = StartCoroutine(FadeInSoundRoutine(sources[name], totalVolume * Settings.instance.GetVolume(), time, sound));
        else routine = StartCoroutine(FadeOutSoundRoutine(sources[name], sources[name].volume, time, sound));
        sound.routine = routine;
    }

    IEnumerator FadeInSoundRoutine(AudioSource source, float totalVolume, float fadeTime, Sound sound)
    {
        sound.isFadingIn = true;
        source.Play();
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            source.volume = totalVolume * (elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        source.volume = totalVolume;
        sound.isFadingIn = false;
    }

    IEnumerator FadeOutSoundRoutine(AudioSource source, float totalVolume, float fadeTime, Sound sound)
    {
        sound.isFadingOut = true;
        float timeRemaining = fadeTime;
        while (timeRemaining > 0f)
        {
            source.volume = totalVolume * (timeRemaining / fadeTime);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        source.volume = 0f;
        source.Stop();
        sound.isFadingOut = false;
    }

}
