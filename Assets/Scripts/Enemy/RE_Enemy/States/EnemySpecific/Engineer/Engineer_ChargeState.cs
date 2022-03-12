using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer_ChargeState : ChargeState
{
    private Engineer engineer;

    public Engineer_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState chargeStateData, Engineer engineer) : base(entity, stateMachine, animBoolName, chargeStateData)
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



        if(isChargeTimeOver)
        {
            if(!isDetectingLedge || isDetectingWall)
            {
                stateMachine.ChangeState(engineer.lookForPlayerState);
            }
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(engineer.playerDetectedState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
