using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bigboy_RangeAttackState : RangeAttackState
{
    private Bigboy bigboy;
    public Bigboy_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
        if (isAnimationFinished)
        {
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(bigboy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(bigboy.lookForPlayerState);
            }

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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