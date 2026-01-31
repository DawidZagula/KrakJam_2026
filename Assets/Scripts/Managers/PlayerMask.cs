using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public static PlayerMask Instance { get; private set; }

    [Header("Debugging Only")]
    [SerializeField] private Mask _currentMask;

    [Header("Configuration - Obstacle Detection")]
    [SerializeField] private float _obstacleDetectionRayLength = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

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
                obstacleMask.TryDefeatObstacle(_currentMask);
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up * .5f, transform.right * _obstacleDetectionRayLength);
    }
}
