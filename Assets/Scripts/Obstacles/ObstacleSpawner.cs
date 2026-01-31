using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private Transform _obstacleSpawnPoint;
    [SerializeField] private Transform _obstaclesParent;

    [SerializeField] private float _obstacleSpawnTime;
    [SerializeField] private float _obstacleSpeed;

    private float timeUntilObstacleSpawn;

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)];

        GameObject spawnedObstacle = 
            Instantiate(obstacleToSpawn, _obstacleSpawnPoint.position, Quaternion.identity, _obstaclesParent);

        if (spawnedObstacle.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.SetMoveSpeed(_obstacleSpeed);
        }
    }
}
