using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = Settings.instance.GetVolume();
        slider.onValueChanged.AddListener(Settings.instance.SetVolume);
    }
}
