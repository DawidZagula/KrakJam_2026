using UnityEngine;

public class ChosenDifficultyManager : MonoBehaviour
{
    public enum GlobalDiffulty
    {
        Normal,
        Hard
    }

    [Header("Debugging Only")]
    [SerializeField] private GlobalDiffulty _selectedGlobalDifficulty;

    public static ChosenDifficultyManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public GlobalDiffulty GetSelectedDifficulty()
    {
        return _selectedGlobalDifficulty;
    }

    public void SetSelectedDifficulty(GlobalDiffulty newDifficulty)
    {
        _selectedGlobalDifficulty = newDifficulty;
    }
}
