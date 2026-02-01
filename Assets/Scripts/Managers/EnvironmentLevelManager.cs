using System;
using System.Collections;
using UnityEngine;

public class EnvironmentLevelManager : MonoBehaviour
{
    public static EnvironmentLevelManager Instance {  get; private set; }
    
    [Header("Debugging Only")]
    [SerializeField] private EnvironmentLevel _currentEnvironmentLevel;
    [SerializeField] private float _currentTimeToChangeLevel;

    [Header("Level Change Timer Configuration")]
    [SerializeField] private float _minTimeToChangeLevel;
    [SerializeField] private float _maxTimeToChangeLevel;

    private Coroutine _countdownToChangeLevelRoutine;

    public event EventHandler<OnLevelChangedEventArgs> OnLevelChanged;
    public class OnLevelChangedEventArgs : EventArgs
    {
        public EnvironmentLevel NewEnvironmentLevel { get; private set; }

        public OnLevelChangedEventArgs(EnvironmentLevel environmentLevel)
        {
            NewEnvironmentLevel = environmentLevel;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.Playing)
        {
            ProcessStartNextLevel();
        }
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
    }

    private void ProcessStartNextLevel()
    {
        if (_countdownToChangeLevelRoutine == null)
        {
            SetNewTimeToChangeLevel();
            _countdownToChangeLevelRoutine = StartCoroutine(CountdownToChangeLevelRoutine());
        }
    }

    private void SetNewTimeToChangeLevel()
    {
        _currentTimeToChangeLevel = UnityEngine.Random.Range(_minTimeToChangeLevel, _maxTimeToChangeLevel);
    }

    private IEnumerator CountdownToChangeLevelRoutine()
    {
        yield return new WaitForSeconds(_currentTimeToChangeLevel);
        SelectRandomNextLevel();

        OnLevelChanged?.Invoke(this, new OnLevelChangedEventArgs(_currentEnvironmentLevel));

        _countdownToChangeLevelRoutine = null;

        ProcessStartNextLevel();
    }

    private void SelectRandomNextLevel()
    {
        int randomLevelIndex;
        EnvironmentLevel newEnvironmentLevel = _currentEnvironmentLevel;
        EnvironmentLevel oldEnvironmentLevel = _currentEnvironmentLevel;

        while (newEnvironmentLevel == oldEnvironmentLevel)
        {
            randomLevelIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnvironmentLevel)).Length);
            newEnvironmentLevel = (EnvironmentLevel)Enum.GetValues(typeof(EnvironmentLevel)).GetValue(randomLevelIndex);

            _currentEnvironmentLevel = newEnvironmentLevel;
        }
    }

}
