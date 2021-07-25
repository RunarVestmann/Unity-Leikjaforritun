using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] float themeFadeInTime;

    void Awake() => SoundManager.instance.FadeInSound("Level1Theme", themeFadeInTime);
}
