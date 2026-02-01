using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Mask _requiredMask;

    //Cached References
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public bool TryDefeatObstacle(Mask playerCurrentMask)
    {
        if (playerCurrentMask == _requiredMask)
        {
           _animator.Play("AfterMaskHit");
            _boxCollider.enabled = false;

          // play sound

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMask(Mask mask)
    {
        _requiredMask = mask;
    }
}
