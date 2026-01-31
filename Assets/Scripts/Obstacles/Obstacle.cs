using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.right * (_moveSpeed * Time.deltaTime));
    }

    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
}
