using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    public Soldier_IdleState idleState { get; private set; }
    public Soldier_MoveState moveState { get; private set; }
    public Soldier_PlayerDetectedState playerDetectedState { get; private set; }
    public Soldier_DodgeState dodgeState { get; private set; }
    public Soldier_RangeAttackState rangeAttackState { get; private set; }

    [SerializeField]
    public D_IdleState idleStateData;
    [SerializeField]
    public D_MoveState moveStateData;
    [SerializeField]
    public D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    public D_RangeAttackState rangeAttackStateData;

    [SerializeField]
    Transform RangeAttackPosition;

    public override void Start()
    {
        base.Start();
        idleState = new Soldier_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Soldier_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Soldier_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        dodgeState = new Soldier_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangeAttackState = new Soldier_RangeAttackState(this, stateMachine, "rangeAttack",RangeAttackPosition, rangeAttackStateData, this);


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

