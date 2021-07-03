using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float offset;
    public float minimumSpawnRate;
    public float maximumSpawnRate;
    float spawnRate;
    float timeSinceLastSpawn = 0f;

    Vector3[] positions = new Vector3[3];
    void Awake()
    {
        // Top
        positions[0] = transform.position + Vector3.up * offset;

        // Middle
        positions[1] = transform.position;

        // Bottom
        positions[2] = transform.position + Vector3.down * offset;

        spawnRate = Random.Range(minimumSpawnRate, maximumSpawnRate);
    }

    void Update()
    {
        if (timeSinceLastSpawn < spawnRate)
        {
            timeSinceLastSpawn += Time.deltaTime;
            return;
        }
        spawnRate = Random.Range(minimumSpawnRate, maximumSpawnRate);
        timeSinceLastSpawn = 0f;
        Vector3 randomPosition = positions[Random.Range(0, positions.Length)];
        Instantiate(spawnObject, randomPosition, Quaternion.identity);
    }
}
