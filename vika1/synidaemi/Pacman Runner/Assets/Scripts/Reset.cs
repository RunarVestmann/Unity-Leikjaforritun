using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    bool canReset = false;
    public void OnPacmanDamaged(int health)
    {
        if (health <= 0)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            canReset = true;
        }
    }

    void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
