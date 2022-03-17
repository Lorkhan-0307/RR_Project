using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer_PlayerDetectedState : PlayerDetectedState
{
    private Engineer engineer;

    public Engineer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Engineer engineer) : base(entity, stateMachine, animBoolName, stateData)
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
        if(performCloseRangeAction)
        {
            stateMachine.ChangeState(engineer.meleeAttackState);
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(engineer.chargeState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(engineer.lookForPlayerState);
        }
        else if(!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(engineer.moveState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
