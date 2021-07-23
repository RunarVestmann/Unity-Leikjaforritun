using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject quitButton;

    void Awake()
    {
        // Hide the quit button if the game is being played on a website
        if (Application.platform == RuntimePlatform.WebGLPlayer) quitButton.SetActive(false);
    }

    public void PlayGame()
    {
        SoundManager.instance.FadeOutSound("MainMenuTheme", 2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() => Application.Quit();
}
