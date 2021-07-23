using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] int zombiesInFirstRound;
    [SerializeField] int zombieIncreasePerRound;
    [SerializeField] float timeBetweenRounds;

    [SerializeField] Transform minTransform;
    [SerializeField] Transform maxTransform;
    [SerializeField] UnityEvent<int> OnRoundChange;

    int zombieCount = 0;
    int round = 0;

    void Awake()
    {
        SpawnZombies();
    }

    void SpawnZombies()
    {
        zombieCount = zombiesInFirstRound + zombieIncreasePerRound * round;
        for (int i = 0; i < zombieCount; i++) SpawnZombie();
    }

    void SpawnZombie()
    {
        float x = Random.Range(minTransform.position.x, maxTransform.position.x);
        float y = Random.Range(minTransform.position.y, maxTransform.position.y);
        var zombieObject = Instantiate(zombiePrefab, new Vector3(x, y, 0f), Quaternion.identity);
        var zombie = zombieObject.GetComponent<Zombie>();
        zombie.onDeath.AddListener(OnZombieDeath);
    }

    void OnZombieDeath()
    {
        zombieCount--;
        if (zombieCount <= 0)
        {
            SoundManager.instance.PlaySuccessSound();
            Invoke(nameof(StartNextRound), timeBetweenRounds);
        }
    }

    void StartNextRound()
    {
        round++;
        OnRoundChange.Invoke(round + 1);
        SpawnZombies();
    }
}
