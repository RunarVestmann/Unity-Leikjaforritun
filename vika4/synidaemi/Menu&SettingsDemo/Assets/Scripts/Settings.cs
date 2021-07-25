using UnityEngine;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField]
    UnityEvent<float> onVolumeChange;

    static float volumeScale = 0.5f;

    public static float VolumeScale
    {
        get => volumeScale;
        set => instance.SetVolume(value);
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float _volumeScale)
    {
        volumeScale = _volumeScale;
        onVolumeChange.Invoke(volumeScale);
    }
    public float GetVolume() => volumeScale;
}
