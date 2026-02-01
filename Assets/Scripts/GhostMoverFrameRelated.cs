using System.Collections;
using UnityEngine;

public class GhostMoverFrameRelated : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private float _movementFramesPerSecond = 8f;

    [SerializeField] private Transform[] _waypoints;

    // Runtime
    private int _currentWaypointIndex;
    private Coroutine _flyingCoroutine;

    private void Awake()
    {
        _currentWaypointIndex = 0;
    }

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }

    private void OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.Playing)
        {
            StartFlying();
        }
        else if (e.NewGameState == GameState.GameOver || e.NewGameState == GameState.Paused)
        {
            StopFlying();
        }
    }

    private void StartFlying()
    {
        if (_flyingCoroutine != null)
            return;

        _flyingCoroutine = StartCoroutine(FlyingRoutine());
    }

    private void StopFlying()
    {
        StopCoroutine(_flyingCoroutine);    
    }

    private IEnumerator FlyingRoutine()
    {
        float tickInterval = 1f / _movementFramesPerSecond;
        WaitForSeconds wait = new WaitForSeconds(tickInterval);

        Transform currentTarget = _waypoints[_currentWaypointIndex];

        while (true)
        {
            while (Vector3.Distance(transform.position, currentTarget.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    currentTarget.position,
                    _moveSpeed * tickInterval
                );

                yield return wait;
            }

            _currentWaypointIndex = GetNextWaypointIndex();
            currentTarget = _waypoints[_currentWaypointIndex];
        }
    }

    private int GetNextWaypointIndex()
    {
        int nextIndex;

        do
        {
            nextIndex = Random.Range(0, _waypoints.Length);
        }
        while (nextIndex == _currentWaypointIndex);

        return nextIndex;
    }
}
