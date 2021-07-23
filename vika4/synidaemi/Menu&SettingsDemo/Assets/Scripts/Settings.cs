using UnityEngine;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField]
    UnityEvent<float> onVolumeChange;

    float volumeScale = 0.5f;

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

    public void SetVolume(float volumeScale)
    {
        this.volumeScale = volumeScale;
        onVolumeChange.Invoke(volumeScale);
    }
    public float GetVolume() => volumeScale;
}
