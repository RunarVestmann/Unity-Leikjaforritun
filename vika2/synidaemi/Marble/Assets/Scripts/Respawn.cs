using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            collider.transform.position = respawnPoint.position;
        else 
            Destroy(collider.gameObject);
    }
}
