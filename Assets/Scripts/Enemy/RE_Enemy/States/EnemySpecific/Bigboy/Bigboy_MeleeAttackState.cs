using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigboy_MeleeAttackState : MeleeAttackState
{
    private Bigboy bigboy;
    public Bigboy_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(bigboy.playerDetectedState);
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}