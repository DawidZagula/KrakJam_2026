using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [SerializeField] private Mask _requiredMask;

    public bool TryDefeatObstacle(Mask playerCurrentMask)
    {
        if (playerCurrentMask == _requiredMask)
        {
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
