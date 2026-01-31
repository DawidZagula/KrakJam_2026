using System;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private Transform _obstacleSpawnPoint;
    [SerializeField] private Transform _obstaclesParent;

    [SerializeField] private float _obstacleSpawnTime;
    [SerializeField] private float _obstacleSpeed;

    //run-time

    private float _timeUntilObstacleSpawn;
    private bool _shouldSpawn;

    public event EventHandler<OnSpeedUpdatedEventArgs> OnSpeedUpdated;
    public class OnSpeedUpdatedEventArgs : EventArgs
    {
        public float NewSpeed { get; }

        public OnSpeedUpdatedEventArgs(float newSpeed)
        {
            NewSpeed = newSpeed;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        _shouldSpawn = e.NewGameState == GameState.Playing;
    }

    private void Update()
    {
        if (!_shouldSpawn) { return; }

        SpawnLoop();
    }

    private void SpawnLoop()
    {
        _timeUntilObstacleSpawn += Time.deltaTime;

        if (_timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            _timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = _obstaclePrefabs[UnityEngine.Random.Range(0, _obstaclePrefabs.Length)];

        GameObject spawnedObstacle =
            Instantiate(obstacleToSpawn, _obstacleSpawnPoint.position, Quaternion.identity, _obstaclesParent);

        if (spawnedObstacle.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.SetMoveSpeed(_obstacleSpeed);
        }
    }

    // For difficulty Manager
    public float GetObstacleSpeed() => _obstacleSpeed;
    public void SetObstacleSpeed(float newSpeed)
    {
        _obstacleSpeed = newSpeed;
        OnSpeedUpdated?.Invoke(this, new OnSpeedUpdatedEventArgs(_obstacleSpeed));
    }
}
