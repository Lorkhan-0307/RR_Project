using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameState : GameState
{
    public PauseGameState(Player player, GameManager gameManager, GameStateMachine stateMachine) : base(player, gameManager, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
