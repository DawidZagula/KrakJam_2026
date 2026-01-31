using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverStateManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private RectTransform _gameOverUIContainer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _bactToMenuButton;
    [SerializeField] private TMP_Text finalScoreValue;
    [SerializeField] private TextMeshProUGUI _selectedDifficultyText;

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        _restartButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.ProcessLoadScene(Scenes.GameScene, true);
        });

        _bactToMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.ProcessLoadScene(Scenes.MainMenu, true);
        });

        _selectedDifficultyText.text =
            (ChosenDifficultyManager.Instance.GetSelectedDifficulty() == ChosenDifficultyManager.GlobalDiffulty.Normal) 
            ? "NORMAL" : "HARD";
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.GameOver)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.StopScoring();
                if (finalScoreValue) finalScoreValue.SetText(ScoreManager.Instance.GetScore().ToString());
            }
            _gameOverUIContainer.gameObject.SetActive(true);
        }
    }
}
