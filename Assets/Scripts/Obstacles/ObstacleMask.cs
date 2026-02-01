using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Mask _requiredMask;

    //Cached References
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool TryDefeatObstacle(Mask playerCurrentMask)
    {
        if (playerCurrentMask == _requiredMask)
        {
           // _animator.Play("AfterMaskHit");
           Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
            //Debug.Log("Wrong mask");
        }
    }

    public void SetMask(Mask mask)
    {
        _requiredMask = mask;
    }
}
