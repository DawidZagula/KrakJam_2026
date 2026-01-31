using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnvironmentLevelManager : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private EnvironmentLevel _currentEnvironmentLevel;
    [SerializeField] private float _currentTimeToChangeLevel;

    [Header("Level Change Timer Configuration")]
    [SerializeField] private float _minTimeToChangeLevel;
    [SerializeField] private float _maxTimeToChangeLevel;

    private Coroutine _countdownToChangeLevelRoutine;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SelectRandomNextLevel();
        }
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
            Debug.Log(newEnvironmentLevel);

        }
    }


    private void SetNewTimeToChangeLevel()
    {
        _currentTimeToChangeLevel = UnityEngine.Random.Range(_minTimeToChangeLevel, _maxTimeToChangeLevel);
    }
}
