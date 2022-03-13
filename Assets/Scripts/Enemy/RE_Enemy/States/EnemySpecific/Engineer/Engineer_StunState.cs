using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer_StunState : StunState
{
    private Engineer engineer;
    public Engineer_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Engineer engineer) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.engineer = engineer;
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
        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(engineer.meleeAttackState);
            }
            else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(engineer.chargeState);
            }
            else
            {
                engineer.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(engineer.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
