using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float _maxRunAnimationSpeedModifier;

    [Header("Debugging Only")]
    [SerializeField] private float _currentRunAnimationSpeedModifier = 1.0f;

    //Cached References
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerMask.Instance.OnChangedMask += PlayerMask_OnChangedMask;

        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        DifficultyManager.Instance.OnSpeedUpdated += DifficultyManager_OnSpeedUpdated;
    }

    private void DifficultyManager_OnSpeedUpdated(object sender, DifficultyManager.OnSpeedUpdatedEventArgs e)
    {
        float percentIncrease = e.SpeedIncreasePercent;

        float percentMultiplier = 1f + (percentIncrease / 100f);

        float newCurrentRunAnimationSpeedModifier = _currentRunAnimationSpeedModifier * percentMultiplier;

        _currentRunAnimationSpeedModifier =
            Mathf.Min(newCurrentRunAnimationSpeedModifier, _maxRunAnimationSpeedModifier);

        _animator.speed = _currentRunAnimationSpeedModifier;
    }

    private void OnDestroy()
    {
        PlayerMask.Instance.OnChangedMask -= PlayerMask_OnChangedMask;
        DifficultyManager.Instance.OnSpeedUpdated -= DifficultyManager_OnSpeedUpdated;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.Playing)
        {
            _animator.Play("playerNoMaskRun");
        }
        else if (e.NewGameState == GameState.GameOver)
        {
            _animator.Play("playerNoMaskBase");
        }
    }

    private void PlayerMask_OnChangedMask(object sender, PlayerMask.OnChangedMaskEventArgs e)
    {
        switch (e.NewMask)
        {
            default:
            case Mask.Repair:
                //
                break;

            case Mask.Destruction:
                //
                break;

            case Mask.Consolation:
                //
                break;

            case Mask.Fright:
                //

                break;
        }
    }
}
