using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_PlayerDetectedState : PlayerDetectedState
{
    private Soldier soldier;
    public Soldier_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Soldier soldier) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
