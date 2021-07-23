using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    [SerializeField] float themeFadeInTime;

    [SerializeField]
    Slider volumeSlider;

    void Awake()
    {
        SoundManager.instance.FadeInSound("Level1Theme", themeFadeInTime);
        volumeSlider.value = Settings.instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(Settings.instance.SetVolume);
    }
}
