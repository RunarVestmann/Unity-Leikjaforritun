using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnInterval;
    [SerializeField] float minSpawnInterval;
    [SerializeField] float spawnIntervalDecrease;
    float timeSinceLastSpawn = 0f;
       

    void Update()
    {
        if (timeSinceLastSpawn < spawnInterval)
        {
            timeSinceLastSpawn += Time.deltaTime;
            return;
        }
        timeSinceLastSpawn = 0f;
        if (spawnInterval > minSpawnInterval) spawnInterval -= spawnIntervalDecrease;
        Instantiate(spawnObject, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }
}
