using UnityEngine;
using UnityEngine.UI;

public class GameOverStateManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private RectTransform _gameOverUIContainer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _bactToMenuButton;

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
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.GameOver)
        {
            _gameOverUIContainer.gameObject.SetActive(true);
        }
    }
}
