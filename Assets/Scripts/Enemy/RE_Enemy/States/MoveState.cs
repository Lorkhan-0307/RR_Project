using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState statedata;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState statedata) : base(entity, stateMachine, animBoolName)
    {
        this.statedata = statedata;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(statedata.movementSpeed);

        
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
