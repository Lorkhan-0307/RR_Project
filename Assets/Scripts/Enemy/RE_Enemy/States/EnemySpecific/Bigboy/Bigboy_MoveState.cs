using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigboy_MoveState : MoveState
{
    private Bigboy bigboy;
    public Bigboy_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState statedata, Bigboy bigboy) : base(entity, stateMachine, animBoolName, statedata)
    {
        this.bigboy = bigboy;
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
        if (isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(bigboy.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            bigboy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(bigboy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}