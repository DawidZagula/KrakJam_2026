using System;
using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private Transform _obstacleSpawnPoint;
    [SerializeField] private Transform _obstaclesParent;
    [Space]

    [SerializeField] private float _obstacleSpawnTime;
    [SerializeField] private float _obstacleSpeed;
    [Space]

    [Header("Configuration - Variable spawn time")]
    [SerializeField] private bool _isSpawnTimeVariable;
    [Space]

    [SerializeField] private float _currentMinSpawnTime;
    [SerializeField] private float _currentMaxSpawnTime;
    [Space]

    [Header("Configuration - Difficulty increase")]
    [SerializeField] private bool _shouldIncreaseSpawnTime;
    [Space]

    [SerializeField] private float _minTimeBeforeIncreasingSpawnTime;
    [SerializeField] private float _maxTimeBeforeIncreasingSpawnTime;
    [SerializeField] private float _singleMaxSpawnTimeDecrease;
    [Space]

    [SerializeField] private bool _shouldDecreaseMinSpawnTime;
    [SerializeField] private float _minSpawnTimeDecreaseRate;
    [SerializeField] private float _singleMinSpawnTimeDecrease;
    [SerializeField] private float _finalMinSpawnTime;

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

        if (!_isSpawnTimeVariable) { return; }

        StartCoroutine(SpawnVariableTimeRoutine());

        if (!_shouldIncreaseSpawnTime) { return; }
        
            StartCoroutine(IncreaseSpawnFrequencyRoutine());

        if (!_shouldDecreaseMinSpawnTime) { return; }
        
            StartCoroutine(DecreaseMinSpawnTimeRoutine());
        
    }

    private IEnumerator SpawnVariableTimeRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => _shouldSpawn);

            float nextSpawnTime = UnityEngine.Random.Range(
                _currentMinSpawnTime,
                _currentMaxSpawnTime
            );

            yield return new WaitForSeconds(nextSpawnTime);

            if (!_shouldSpawn)
            {
                continue;
            }

            Spawn();
        }
    }

    private IEnumerator IncreaseSpawnFrequencyRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => _shouldSpawn);

            float timeUntilNextIncrease = UnityEngine.Random.Range(
                _minTimeBeforeIncreasingSpawnTime,
                _maxTimeBeforeIncreasingSpawnTime
            );

            yield return new WaitForSeconds(timeUntilNextIncrease);

            if (!_shouldSpawn)
            {
                continue;
            }

            _currentMaxSpawnTime -= _singleMaxSpawnTimeDecrease;

            _currentMaxSpawnTime = Mathf.Max(
                _currentMaxSpawnTime,
                _currentMinSpawnTime
            );
        }
    }

    private IEnumerator DecreaseMinSpawnTimeRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => _shouldSpawn);

            yield return new WaitForSeconds(_minSpawnTimeDecreaseRate);

            if (!_shouldSpawn)
            {
                continue;
            }

            _currentMinSpawnTime -= _singleMinSpawnTimeDecrease;

            _currentMinSpawnTime = Mathf.Max(
                _currentMinSpawnTime,
                _finalMinSpawnTime
            );

            _currentMaxSpawnTime = Mathf.Max(
                _currentMaxSpawnTime,
                _currentMinSpawnTime
            );
        }
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
        if (!_shouldSpawn || _isSpawnTimeVariable) { return; }

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
