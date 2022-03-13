using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer_MeleeAttackState : MeleeAttackState
{
    private Engineer engineer;
   

    public Engineer_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Engineer engineer) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(engineer.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(engineer.lookForPlayerState);
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
