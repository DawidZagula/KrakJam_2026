using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerMask.Instance.OnChangedMask += PlayerMask_OnChangedMask;

        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
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

    private void OnDestroy()
    {
        PlayerMask.Instance.OnChangedMask -= PlayerMask_OnChangedMask;
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
