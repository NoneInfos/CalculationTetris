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
public class GameStateManager : ManagerBase<GameStateManager>
{

    public GameState CurrentState { get; private set; }
    public event System.Action<GameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetGameState(GameState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }

    public override void InitializeManager()
    {
    }

    public override void ClearManager()
    {
    }

    public override void FinalizeManager()
    {
    }
}