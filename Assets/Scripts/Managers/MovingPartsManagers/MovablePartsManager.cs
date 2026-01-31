using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MovablePartsManager : MonoBehaviour
{
    public static MovablePartsManager Instance { get; private set; }
    
    [Header("Cached References")]
    [SerializeField] private Transform _movablePartsParent;

    [SerializeField] private Transform _movablePartObjectPrefab;

    [Header("Configuration")]
    [SerializeField] private int _numberOfPartsAtStart;
    [Space]
    [SerializeField] private float _floorPartLength;
    [SerializeField] private float _backgroundPartLength;
    [Space]
    [SerializeField] private float _floorMoveSpeed;
    [SerializeField] private float _foregroundMoveSpeed;
    [SerializeField] private float _backgroundMoveSpeed;
    [Space]
    [Header("Configuration: Foreground Specific")]
    [SerializeField] private float _minimumForegroundXOffset;
    [SerializeField] private float _maximumForegroundXOffset;

    //run-time
    private bool _shouldRun;

    private readonly List<Transform> _spawnedFloorParts = new List<Transform>();

    //
    private readonly List<Transform> _spawnedEdgeParts = new List<Transform>();

    private readonly List<Transform> _spawnedBackgroundParts = new List<Transform>();
    private readonly List<Transform> _spawnedForegroundParts = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstantiateStartParts();

        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        EnvironmentLevelManager.Instance.OnLevelChanged += EnvironmentLevelManager_OnLevelChanged;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        _shouldRun = e.NewGameState == GameState.Playing;
    }

    private void OnDestroy()
    {
        EnvironmentLevelManager.Instance.OnLevelChanged -= EnvironmentLevelManager_OnLevelChanged;
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;

    }

    private void EnvironmentLevelManager_OnLevelChanged(object sender, EnvironmentLevelManager.OnLevelChangedEventArgs e)
    {
        // Position environment object with special transition sprite exactly at the edges of level transition
        InstantiateEdgeSprite();
    }

    private void InstantiateEdgeSprite()
    {
        float spawnXOffset = GetEdgeSpawnXOffset();

        Vector3 spawmPosition = new Vector3(spawnXOffset, 0, 0);

        Transform spawnedEdgePartInstance =
             Instantiate(_movablePartObjectPrefab, spawmPosition, transform.rotation, _movablePartsParent);

        _spawnedEdgeParts.Add(spawnedEdgePartInstance);

        spawnedEdgePartInstance.GetComponent<MovablePartObject>().SetEdgeSprite();
    }

    private float GetEdgeSpawnXOffset()
    {
         return _spawnedFloorParts[_spawnedFloorParts.Count - 1].transform.position.x + (_floorPartLength * .5f);
    }

    private void Update()
    {
        if (!_shouldRun) { return; }

        MoveParts(_spawnedFloorParts,MovableParts.Floor);
        MoveParts(_spawnedEdgeParts, MovableParts.Edge);

        //No graphics yet
       // MoveParts(_spawnedBackgroundParts, MovableParts.Background);
       // MoveParts(_spawnedForegroundParts, MovableParts.Foreground);
    }

    private void InstantiateStartParts()
    {
        for (int i = 0; i < _numberOfPartsAtStart; i++)
        {
            SpawnPartInstance(MovableParts.Floor);
        }
    }

    private void SpawnPartInstance(MovableParts movablePartType)
    {
        float spawnXOffset = GetSpawnXOffset(movablePartType);

        Vector3 spawmPosition = new Vector3(spawnXOffset, 0, 0);

        Transform spawnFloorPartInstance =
             Instantiate(_movablePartObjectPrefab, spawmPosition, transform.rotation, _movablePartsParent);

        _spawnedFloorParts.Add(spawnFloorPartInstance);

        spawnFloorPartInstance.GetComponent<MovablePartObject>().SetRandomSprite(movablePartType);
    }

    private float GetSpawnXOffset(MovableParts movablePartType)
    {
        float spawnXOffset;
        if (_spawnedFloorParts.Count == 0)
        {
            spawnXOffset = 0f;
        }
        else
        {
            if (movablePartType == MovableParts.Foreground)
            {
                spawnXOffset = Random.Range(_minimumForegroundXOffset, _maximumForegroundXOffset);
            }
            else
            {
                spawnXOffset = _spawnedFloorParts[_spawnedFloorParts.Count - 1].transform.position.x + _floorPartLength;
            }
        }

        return spawnXOffset;
    }

    private void MoveParts(List<Transform> listOfObjectsToMove, MovableParts movablePartType)
    {
        float moveSpeed = GetMoveSpeed(movablePartType);

        for (int i = 0; i < listOfObjectsToMove.Count; i++)
        {
            Transform spawnedFloorPartInstance = listOfObjectsToMove[i];
            spawnedFloorPartInstance.Translate(-transform.right * (moveSpeed * Time.deltaTime));

            if (spawnedFloorPartInstance.position.x <= Camera.main.transform.position.x - _floorPartLength)
            {
                listOfObjectsToMove.Remove(spawnedFloorPartInstance);
                Destroy(spawnedFloorPartInstance.gameObject);
                SpawnPartInstance(MovableParts.Floor);
            }
        }
    }

    private float GetMoveSpeed(MovableParts movablePartType)
    {
        switch (movablePartType)
        {
            default:
            case MovableParts.Floor:
            case MovableParts.Edge:
                return _floorMoveSpeed;

            case MovableParts.Background:
                return _backgroundMoveSpeed;

            case MovableParts.Foreground:
                return _foregroundMoveSpeed;
        }
    }

    // For the difficulty increasing manager
    public float GetFloorMoveSpeed() => _floorMoveSpeed;
    public float GetBackgroundMoveSpeed() => _backgroundMoveSpeed;
    public float GetForegroundMoveSpeed() => _foregroundMoveSpeed;

    public void SetFloorMoveSpeed(float newSpeed) => _floorMoveSpeed = newSpeed;
    public void SetBackgroundMoveSpeed(float newSpeed) => _backgroundMoveSpeed = newSpeed;
    public void SetForegroundMoveSpeed(float newSpeed) => _foregroundMoveSpeed = newSpeed;
}
