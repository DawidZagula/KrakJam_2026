using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitReceiver : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private int _lifes;

    [Header("Configuration")]
    [SerializeField] private float _hitBackDistance;
    [SerializeField] private float _hitBackTime;

    private Coroutine _hitBackRoutine;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Hit();
        }
    }

    public void Hit()
    {

        if (_lifes == 0)
        {
            ProcessDeath();
            return;
        }

        if (_hitBackRoutine != null) { return; }

        _hitBackRoutine = StartCoroutine(HitBackRoutine());
        _lifes--;

    }

    private IEnumerator HitBackRoutine()
    {

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition - transform.right * _hitBackDistance;

        float elapsedTime = 0f;

        while (elapsedTime < _hitBackTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _hitBackTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;

        _hitBackRoutine = null;
    }

    private void ProcessDeath()
    {
        Debug.Log("Player Died");
    }
}
