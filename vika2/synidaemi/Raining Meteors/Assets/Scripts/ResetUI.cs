using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResetUI : MonoBehaviour
{
    bool canReset = false;

    public void AllowReset()
    {
        canReset = true;
        GetComponent<TextMeshProUGUI>().enabled = true;
    }

    void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
