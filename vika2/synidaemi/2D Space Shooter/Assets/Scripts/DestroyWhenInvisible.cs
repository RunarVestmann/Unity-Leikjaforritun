using UnityEngine;

public class DestroyWhenInvisible : MonoBehaviour
{
    public GameObject objectToDestroy;
    void OnBecameInvisible()
    {
        if (objectToDestroy != null) Destroy(objectToDestroy);
        else Destroy(gameObject);
    }
}
