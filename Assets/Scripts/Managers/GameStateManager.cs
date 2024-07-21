using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
public class GameStateManager : SingletonClass<GameStateManager>
{

    public GameState CurrentState { get; private set; }
    public event System.Action<GameState> OnGameStateChanged;

    private void Awake()
    {
    }

    public void SetGameState(GameState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }
}