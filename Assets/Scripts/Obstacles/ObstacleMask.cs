using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [SerializeField] private Mask _requiredMask;

    public void TryDefeatObstacle(Mask playerCurrentMask)
    {
        if (playerCurrentMask == _requiredMask)
        {
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Wrong mask");
        }
    }

    public void SetMask(Mask mask)
    {
        _requiredMask = mask;
    }
}
