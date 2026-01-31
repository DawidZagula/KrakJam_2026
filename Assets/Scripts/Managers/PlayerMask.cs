using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public static PlayerMask Instance { get; private set; }

    [Header("Debugging Only")]
    [SerializeField] private Mask _currentMask;

    [Header("Configuration - Obstacle Detection")]
    [SerializeField] private float _obstacleDetectionRayLength = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

    //run-time
    private Collider _lastHitObstacleCollider;

    public event EventHandler<OnChangedMaskEventArgs> OnChangedMask;
    public class OnChangedMaskEventArgs : EventArgs
    {
        public Mask NewMask { get; }

        public OnChangedMaskEventArgs(Mask newMask)
        {
            NewMask = newMask;
        }
    }

    public event EventHandler OnDefeatedObstacle;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ShootRayForObstacles();
    }

    private void ShootRayForObstacles()
    {
        Vector2 origin = transform.position + Vector3.up * .5f;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _obstacleDetectionRayLength, _obstacleLayer);

        // Debug ray (widoczny w Scene)
        // Debug.DrawRay(origin, direction * _rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out ObstacleMask obstacleMask))
            {
              if (_lastHitObstacleCollider == null || _lastHitObstacleCollider != hit.collider)
                {
                    if (obstacleMask.TryDefeatObstacle(_currentMask))
                    {
                        OnDefeatedObstacle?.Invoke(this, EventArgs.Empty);

                    }
                }
            }
        }
    }

    public Mask GetPlayerMask()
    {
        return _currentMask;
    }

    public void SetPlayerMask(Mask newMask)
    {
        _currentMask = newMask;

        AudioManager.AudioName selectedAudioName;

        switch (newMask)
        {
            default:
            case Mask.Repair:
                selectedAudioName = AudioManager.AudioName.Mask_Repair;
                break;

            case Mask.Destruction:
                selectedAudioName = AudioManager.AudioName.Mask_Destroy;

                break;

            case Mask.Fright:
                selectedAudioName = AudioManager.AudioName.Mask_scary;

                break;

            case Mask.Consolation:
                selectedAudioName = AudioManager.AudioName.Mask_cheer_up;

                break;
        }

        AudioManager.Instance.PlaySound(selectedAudioName);

        OnChangedMask?.Invoke(this, new OnChangedMaskEventArgs(newMask));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up * .5f, transform.right * _obstacleDetectionRayLength);
    }
}
