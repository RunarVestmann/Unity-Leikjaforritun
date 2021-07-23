using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResetUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    bool canReset = false;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void OnPlayerHealthChange(int health)
    {
        if (health > 0) return;

        textMesh.enabled = true;
        canReset = true;
    }

    void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
