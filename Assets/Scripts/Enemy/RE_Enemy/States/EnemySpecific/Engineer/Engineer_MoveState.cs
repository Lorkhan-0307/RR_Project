using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer_MoveState : MoveState
{
    private Engineer engineer;

    public Engineer_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState statedata, Engineer engineer) : base(entity, stateMachine, animBoolName, statedata)
    {
        this.engineer = engineer;
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(engineer.playerDetectedState);
        }

        else if(isDetectingWall || !isDetectingLedge)
        {
            engineer.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(engineer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
