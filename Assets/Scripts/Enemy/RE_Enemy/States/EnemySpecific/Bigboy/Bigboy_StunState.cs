using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bigboy_StunState : StunState
{
    private Bigboy bigboy;
    public Bigboy_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(bigboy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(bigboy.chargeState);
            }
            else
            {
                bigboy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(bigboy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}