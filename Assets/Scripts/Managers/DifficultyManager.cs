using System;
using UnityEngine;
using UnityEngine.Rendering;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private float _speedIncreasePercent;

    public event EventHandler<OnSpeedUpdatedEventArgs> OnSpeedUpdated;
    public class OnSpeedUpdatedEventArgs : EventArgs
    {
        public float SpeedIncreasePercent { get; }

        public OnSpeedUpdatedEventArgs(float speedIncreasePercent)
        {
            SpeedIncreasePercent = speedIncreasePercent;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnvironmentLevelManager.Instance.OnLevelChanged += EnvironmentLevelManager_OnLevelChanged;
    }

    private void OnDestroy()
    {
        EnvironmentLevelManager.Instance.OnLevelChanged -= EnvironmentLevelManager_OnLevelChanged;
    }

    private void EnvironmentLevelManager_OnLevelChanged(object sender, EnvironmentLevelManager.OnLevelChangedEventArgs e)
    {
        UpdateSpeed();

    }

    private void UpdateSpeed()
    {
        float oldObstacleSpeed = ObstacleSpawner.Instance.GetObstacleSpeed();
        float newObstacleSpeed = oldObstacleSpeed * (1f + _speedIncreasePercent / 100f);
        ObstacleSpawner.Instance.SetObstacleSpeed(newObstacleSpeed);

        float oldBackgroundSpeed = MovablePartsManager.Instance.GetBackgroundMoveSpeed();
        float newBackgroundSpeed = oldBackgroundSpeed * (1f + _speedIncreasePercent / 100f);
        MovablePartsManager.Instance.SetBackgroundMoveSpeed(newBackgroundSpeed);

        float oldFloorSpeed = MovablePartsManager.Instance.GetFloorMoveSpeed();
        float newFloorSpeed = oldFloorSpeed * (1f + _speedIncreasePercent / 100f);
        MovablePartsManager.Instance.SetFloorMoveSpeed(newFloorSpeed);

        float oldForegroundSpeed = MovablePartsManager.Instance.GetForegroundMoveSpeed();
        float newForegroundSpeed = oldForegroundSpeed * (1f + _speedIncreasePercent / 100f);
        MovablePartsManager.Instance.SetForegroundMoveSpeed(newForegroundSpeed);

        OnSpeedUpdated?.Invoke(this, new OnSpeedUpdatedEventArgs(_speedIncreasePercent));
    }
}
