using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            pauseMenu.SetActive(isMenuOpen);
            Time.timeScale = isMenuOpen ? 0f : 1f;
        }
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        pauseMenu.SetActive(false);
    }
}
