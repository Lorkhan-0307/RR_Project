using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    public GameState currentState { get; private set; }

    public void Initialize(GameState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(GameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
