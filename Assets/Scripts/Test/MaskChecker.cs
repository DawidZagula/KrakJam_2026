using UnityEngine;
using UnityEngine.UI;

public class MaskChecker : MonoBehaviour
{
    [SerializeField] private float _rayLength = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

    private void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _rayLength, _obstacleLayer);

        // Debug ray (widoczny w Scene)
        Debug.DrawRay(origin, direction * _rayLength, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("Hit something.");
            Transform parent = hit.collider.transform.parent;
            //Mask currentMask = PlayerMask.Instance.GetPlayerMask();

            if (parent != null && parent.CompareTag("Obstacle"))
            {
                Destroy(parent.gameObject);
            }
            
        }
    }
}
