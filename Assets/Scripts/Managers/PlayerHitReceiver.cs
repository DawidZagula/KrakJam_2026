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
    [Space]
    [SerializeField] private float _immunityTimeAfterHit;

    //Cached References
    private Animator _animator;

    //run-time
    private bool _isImmune = false;

    private Coroutine _hitBackRoutine;
    private Coroutine _immmunityRoutine;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Hit()
    {
        if (_isImmune) { return; }

        if (_lifes == 0)
        {
            ProcessDeath();
            return;
        }

        if (_hitBackRoutine != null) { return; }
        _hitBackRoutine = StartCoroutine(HitBackRoutine());

        _animator.Play("playerElectroShock");

        AudioManager.Instance.PlaySound(AudioManager.AudioName.Electricity_cartoon);

        if (_immmunityRoutine != null) { return; }
        _immmunityRoutine = StartCoroutine(ImmunityRoutine());

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

    private IEnumerator ImmunityRoutine()
    {
        _isImmune = true;
        yield return new WaitForSeconds(_immunityTimeAfterHit);
        _isImmune = false;
        _immmunityRoutine = null;
    }

    private void ProcessDeath()
    {
        GameStateManager.Instance.ChangeCurrentGameState(GameState.GameOver);
    }
}
