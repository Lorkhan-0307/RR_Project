using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_DodgeState : DodgeState
{
    private Soldier soldier;
    public Soldier_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Soldier soldier) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.soldier = soldier;
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

        if (isDodgeOver)
        {
            if (!isPlayerInMaxAgroRange)
            {
                Debug.Log("State to look");
                stateMachine.ChangeState(soldier.lookForPlayerState);
            }
            else if(isPlayerInMaxAgroRange)
            {
                Debug.Log("State to detect");
                stateMachine.ChangeState(soldier.playerDetectedState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
