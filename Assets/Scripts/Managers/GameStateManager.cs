using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
   public static GameStateManager Instance { get; private set; }
    
    [Header("Debugging Only")]
    [SerializeField] private GameState _currentGameState;

    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;
    public class OnGameStateChangedEventArgs : EventArgs
    {
        public GameState NewGameState { get; }

        public OnGameStateChangedEventArgs(GameState newGameState)
        {
            NewGameState = newGameState;
        }
    }

    private void Awake()
    {
        Instance = this;

       // ChangeCurrentGameState(GameState.NotStarted);
    }

    public void ChangeCurrentGameState(GameState newGameState)
    {
        _currentGameState = newGameState;

        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs(newGameState));
    }
}
