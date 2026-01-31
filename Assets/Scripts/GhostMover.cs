using System.Collections;
using UnityEngine;

public class GhostMover : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform[] _waypoints;

    //run-time
    private int _currentWaypointIndex;

    private void Awake()
    {
        _currentWaypointIndex = 0;
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
        if (e.NewGameState == GameState.Playing)
        {
           StartCoroutine(FlyingRoutine());
        }
        else if (e.NewGameState == GameState.GameOver)
        {
            StopCoroutine(FlyingRoutine());
        }
    }

    private IEnumerator FlyingRoutine()
    {

        Transform currentTarget = _waypoints[_currentWaypointIndex];

        while (true)
        {
            while (Vector3.Distance(transform.position, currentTarget.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    currentTarget.position,
                    _moveSpeed * Time.deltaTime
                );

                yield return null;
            }

            int nextIndex;
            do
            {
                nextIndex = Random.Range(0, _waypoints.Length);
            }
            while (nextIndex == _currentWaypointIndex);

            _currentWaypointIndex = nextIndex;
            currentTarget = _waypoints[_currentWaypointIndex];
        }
    }
}
