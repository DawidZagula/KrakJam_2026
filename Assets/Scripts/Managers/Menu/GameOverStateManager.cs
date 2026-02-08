using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class GameOverStateManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private RectTransform _gameOverUIContainer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _bactToMenuButton;
    [SerializeField] private TMP_Text finalScoreValue;
    [SerializeField] private TextMeshProUGUI _selectedDifficultyText;

    [Header("Displaying Configuration")]
    [SerializeField] private float _scalingTime;

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        _restartButton.onClick.AddListener(() =>
        {     
            _gameOverUIContainer.gameObject.LeanScale(Vector3.zero, _scalingTime).setOnComplete(() =>
            {
                SceneLoader.Instance.ProcessLoadScene(Scenes.GameScene, false);
            });
            
        });

        _bactToMenuButton.onClick.AddListener(() =>
        {
            _gameOverUIContainer.gameObject.LeanScale(Vector3.zero, _scalingTime).setOnComplete(() =>
            {
                SceneLoader.Instance.ProcessLoadScene(Scenes.MainMenu, false);
            });

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

            FadeTransitioner.Instance.FadeOut(DisplayWindow);

            MusicManager.Instance.StopPlaying();
            AudioManager.Instance.PlaySound(AudioManager.AudioName.Dead_sound);
        }
    }

    private void DisplayWindow()
    {    
        _gameOverUIContainer.localScale = Vector3.zero;
        _gameOverUIContainer.gameObject.SetActive(true);
        _gameOverUIContainer.gameObject.LeanScale(Vector3.one, _scalingTime);
    }

}


