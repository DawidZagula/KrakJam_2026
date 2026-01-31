using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectionUI : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private Button _normalSelectionButton;
    [SerializeField] private Button _hardSelectionButton;
    [SerializeField] private TextMeshProUGUI _difficultyDescriptionText;

    private void Start()
    {
        _normalSelectionButton.onClick.AddListener(() =>
        {
            SelectDifficulty(ChosenDifficultyManager.GlobalDiffulty.Normal);
            ProcessStartGame();

        });

        _hardSelectionButton.onClick.AddListener(() =>
        {
            SelectDifficulty(ChosenDifficultyManager.GlobalDiffulty.Hard);
            ProcessStartGame();
        });
    }

    private void SelectDifficulty(ChosenDifficultyManager.GlobalDiffulty chosenDifficulty)
    {
        ChosenDifficultyManager.Instance.SetSelectedDifficulty(chosenDifficulty);
    }

    private void ProcessStartGame()
    {
        SceneLoader.Instance.ProcessLoadScene(Scenes.GameScene, true);
    }

    public void DisplayDifficultyDescriptionText(string textToDisplay)
    {
        _difficultyDescriptionText.text = textToDisplay;
    }
}
