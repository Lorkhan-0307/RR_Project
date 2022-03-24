using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bigboy_LookForPlayerState : LookForPlayerState
{
    private Bigboy bigboy;
    public Bigboy_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(bigboy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(bigboy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}