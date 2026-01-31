using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private float _moveSpeed;

    [Header("Configuration")]
    [SerializeField] private float _timeToSelfDestruct;

    private void Start()
    {
        ObstacleSpawner.Instance.OnSpeedUpdated += ObstacleSpawner_OnSpeedUpdated;

        Destroy(gameObject, _timeToSelfDestruct);
    }

    private void OnDestroy()
    {
        ObstacleSpawner.Instance.OnSpeedUpdated -= ObstacleSpawner_OnSpeedUpdated;
    }

    private void ObstacleSpawner_OnSpeedUpdated(object sender, ObstacleSpawner.OnSpeedUpdatedEventArgs e)
    {
        _moveSpeed = e.NewSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.right * (_moveSpeed * Time.deltaTime));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHitReceiver playerHitReceiver))
        {
            playerHitReceiver.Hit();
        }
    }

    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
}
