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
        if(!isPlayerInMaxAgroRange)
        {
            engineer.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(engineer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
