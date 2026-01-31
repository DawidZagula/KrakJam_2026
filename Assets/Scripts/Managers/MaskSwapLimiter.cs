using System;
using TMPro;
using UnityEngine;

public class MaskSwapLimiter : MonoBehaviour
{
    public static MaskSwapLimiter Instance {  get; private set; }
    
    [Header("Cache References")]
    [SerializeField] private TextMeshProUGUI _swapsCountText;

    [Header("Configuration")]
    [SerializeField] private int _maxMaskSwaps;

    [Header("Debugging Only")]
    [SerializeField] private int _currentMaskSwaps;


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
