using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [SerializeField] private Mask requiredMask;

    public bool CanBeDestroyed()
    {
        return PlayerMask.Instance != null &&
               PlayerMask.Instance.GetPlayerMask() == requiredMask;
    }
}
