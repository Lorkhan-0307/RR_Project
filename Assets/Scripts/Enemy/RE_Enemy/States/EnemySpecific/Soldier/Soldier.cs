using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    public Soldier_IdleState idleState { get; private set; }
    public Soldier_MoveState moveState { get; private set; }
    public Soldier_PlayerDetectedState playerDetectedState { get; private set; }

    [SerializeField]
    public D_IdleState idleStateData;
    [SerializeField]
    public D_MoveState moveStateData;
    [SerializeField]
    public D_PlayerDetected playerDetectedStateData;

    public override void Start()
    {
        base.Start();
        idleState = new Soldier_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Soldier_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Soldier_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

