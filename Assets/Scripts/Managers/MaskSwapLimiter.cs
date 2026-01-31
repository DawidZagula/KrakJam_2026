using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MaskSwapLimiter : MonoBehaviour
{
    public static MaskSwapLimiter Instance {  get; private set; }
    
    [Header("Cache References")]
    [SerializeField] private TextMeshProUGUI _swapsCountText;
    [Space]
    [Header("Configuration")]
    [SerializeField] private int _maxMaskSwaps;
    [Space]
    [Header("Configuration - Normal Difficulty Only")]
    [SerializeField] private float _swapReplenishmentDuration;
    [Space]
    [Header("Debugging Only")]
    [SerializeField] private int _currentMaskSwaps;
    [SerializeField] private bool _shouldReplenishFirstSwap;

    // run-time 
    private Coroutine _replenishSwapRoutine;

    private void Awake()
    {
        Instance = this;

        _currentMaskSwaps = _maxMaskSwaps;
        UpdateSwapsCountText();
    }

    private void Start()
    {
        PlayerMask.Instance.OnChangedMask += PlayerMask_OnChangedMask;
        PlayerMask.Instance.OnDefeatedObstacle += PlayerMask_OnDefeatedObstacle;

        _shouldReplenishFirstSwap = ChosenDifficultyManager.Instance.GetSelectedDifficulty() 
            == ChosenDifficultyManager.GlobalDiffulty.Normal;
    }


    private void OnDestroy()
    {
        PlayerMask.Instance.OnChangedMask -= PlayerMask_OnChangedMask;
        PlayerMask.Instance.OnDefeatedObstacle -= PlayerMask_OnDefeatedObstacle;

    }

    private void PlayerMask_OnChangedMask(object sender, PlayerMask.OnChangedMaskEventArgs e)
    {
        if (GameStateManager.Instance.GetCurrentGameState() == GameState.Playing)
        {
            DeductSwaps();
        }
    }
    private void PlayerMask_OnDefeatedObstacle(object sender, System.EventArgs e)
    {
        if (GameStateManager.Instance.GetCurrentGameState() == GameState.Playing)
        {
            TryIncreaseSwaps();
        }
    }

    private void DeductSwaps()
    {
        if (_currentMaskSwaps > 0)
        {
            _currentMaskSwaps--;
            UpdateSwapsCountText();
        }

        if (_currentMaskSwaps == 0 && _shouldReplenishFirstSwap)
        {
            if (_replenishSwapRoutine != null) { return; }

            _replenishSwapRoutine = StartCoroutine(ReplenishSwapRoutine());
        }

    }

    private IEnumerator ReplenishSwapRoutine()
    {
        yield return new WaitForSeconds(_swapReplenishmentDuration);
        
        if (_currentMaskSwaps == 0)
        {
            _currentMaskSwaps++;
            UpdateSwapsCountText();
        }
        _replenishSwapRoutine = null;
    }

    private void TryIncreaseSwaps()
    {
        if ( _currentMaskSwaps < 3)
        {
            _currentMaskSwaps++;
            UpdateSwapsCountText();
        }
    }

    private void UpdateSwapsCountText()
    {
        _swapsCountText.text = _currentMaskSwaps.ToString();
    }

    public int GetCurrentMaskSwaps()
    {
        return _currentMaskSwaps;
    }
}
