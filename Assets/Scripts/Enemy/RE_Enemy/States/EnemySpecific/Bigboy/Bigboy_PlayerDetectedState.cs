using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bigboy_PlayerDetectedState : PlayerDetectedState
{
    private Bigboy bigboy;
    public Bigboy_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(performCloseRangeAction)
        {
            stateMachine.ChangeState(bigboy.meleeAttackState);
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(bigboy.chargeState);
        }
        else if(isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(bigboy.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(bigboy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement?.Flip();
            stateMachine.ChangeState(bigboy.lookForPlayerState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}