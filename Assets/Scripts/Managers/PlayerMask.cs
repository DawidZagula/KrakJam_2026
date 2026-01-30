using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public static PlayerMask Instance {  get; private set; }

    [Header("Debugging Only")]
    [SerializeField] private Mask _currentMask;

    private void Awake()
    {
        Instance = this;
    }

    public Mask GetPlayerMask()
    {
        return _currentMask;
    }

    public void SetPlayerMask(Mask newMask)
    {
        _currentMask = newMask;
        Debug.Log("Set player new mask: " + newMask);
    }
}
