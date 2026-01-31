using UnityEngine;

public class ObstacleMask : MonoBehaviour
{
    [SerializeField] private Mask requiredMask;

    public void TryDefeatObstacle(Mask playerCurrentMask)
    {
        if (playerCurrentMask == requiredMask)
        {
            Debug.Log("Defeat");
        }
        else
        {
            Debug.Log("Wrong mask");
        }
    }
}
