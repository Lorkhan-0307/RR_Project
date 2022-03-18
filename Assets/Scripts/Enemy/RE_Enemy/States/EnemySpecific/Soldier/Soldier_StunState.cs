using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_StunState : StunState
{
    private Soldier soldier;
    public Soldier_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Soldier soldier) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.soldier = soldier;
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
        base.LogicUpdate();
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(soldier.dodgeState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(soldier.rangeAttackState);
            }
            else
            {
                soldier.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(soldier.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
