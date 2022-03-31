using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected Player player;
    protected GameManager gameManager;
    protected GameStateMachine stateMachine;

    protected bool isExitingState;

    public GameState(Player player, GameManager gameManager, GameStateMachine stateMachine)
    {
        this.player = player;
        this.gameManager = gameManager;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        DoChecks();
        Debug.Log(stateMachine.currentState);
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void DoChecks()
    {

    }

}
