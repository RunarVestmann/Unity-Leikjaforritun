using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    [SerializeField] int nextLevel;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            SceneManager.LoadScene(nextLevel);
    }
}
