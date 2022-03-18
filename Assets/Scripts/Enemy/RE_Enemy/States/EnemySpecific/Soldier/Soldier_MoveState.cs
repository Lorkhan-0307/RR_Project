using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_MoveState : MoveState
{
    private Soldier soldier;
    public Soldier_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState statedata, Soldier soldier) : base(entity, stateMachine, animBoolName, statedata)
    {
        this.soldier = soldier;
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
        
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(soldier.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            soldier.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(soldier.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
