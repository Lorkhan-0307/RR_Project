using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigboy_IdleState : IdleState
{
    private Bigboy bigboy;
    public Bigboy_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Bigboy bigboy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(isIdleTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(bigboy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(bigboy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
