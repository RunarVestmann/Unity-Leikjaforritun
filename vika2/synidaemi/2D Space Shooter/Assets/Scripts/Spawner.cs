using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenSpawns;

    float timeSinceLastSpawn = 0f;

    void Awake()
    {
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }

    void Update()
    {
        if (timeSinceLastSpawn < timeBetweenSpawns)
        {
            timeSinceLastSpawn += Time.deltaTime;
            return;
        }
        timeSinceLastSpawn = 0f;
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
}
