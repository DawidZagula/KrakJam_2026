using UnityEngine;
using UnityEngine.UI;

public class GamePausedManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private bool isGamePaused = false;

    [Header("Cached References")]
    [SerializeField] private Transform _pausedGameUIContainer;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _bactToMenuButton;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Menu.Enable();
        _playerInputActions.Menu.PauseGame.performed += PauseGame_performed;
    }

    private void OnDisable()
    {
        _playerInputActions.Menu.PauseGame.performed -= PauseGame_performed;
        _playerInputActions.Menu.Disable();
    }

    private void Start()
    {
        //odznaczenie, że gra jest zapauzowana przy starcie sceny.
        isGamePaused = false;


        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        _continueButton.onClick.AddListener(() =>
        {
            ChangePausedState();
        });

        _restartButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.ProcessLoadScene(Scenes.GameScene, true);
        });

        _bactToMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.ProcessLoadScene(Scenes.MainMenu, true);
        });

        _pausedGameUIContainer.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
    }

    private void PauseGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ChangePausedState();
    }

    private void ChangePausedState()
    {
        GameState state = GameStateManager.Instance.GetCurrentGameState();
        if (!(state == GameState.Playing || state == GameState.Paused)) return;

        isGamePaused = !isGamePaused;

        //if (isGamePaused) _pausedGameUIContainer.gameObject.SetActive(true);
        //else _pausedGameUIContainer.gameObject.SetActive(false);

        if (isGamePaused) GameStateManager.Instance.ChangeCurrentGameState(GameState.Paused);
        else GameStateManager.Instance.ChangeCurrentGameState(GameState.Playing);
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.Paused)
        {
            //isGamePaused = true;

            _pausedGameUIContainer.gameObject.SetActive(true);

            // NOTE: przykład tego, że poniższe warunki powinien być we ScoreManager jako funkcja subskrybująca
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.StopScoring();
            }
        }
        else
        {
            //isGamePaused = false;

            _pausedGameUIContainer.gameObject.SetActive(false);

            if (ScoreManager.Instance != null && e.NewGameState == GameState.Playing)
            {
                ScoreManager.Instance.StartScoring();
            }
        }
    }
}
