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
    public Soldier_LookForPlayerState lookForPlayerState { get; private set; }
    public Soldier_StunState stunState { get; private set; }
    public Soldier_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    
    [SerializeField]
    private D_RangeAttackState rangeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    Transform RangeAttackPosition;

    public override void Awake()
    {
        base.Awake();
        idleState = new Soldier_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Soldier_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Soldier_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        dodgeState = new Soldier_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangeAttackState = new Soldier_RangeAttackState(this, stateMachine, "rangeAttack",RangeAttackPosition, rangeAttackStateData, this);
        lookForPlayerState = new Soldier_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new Soldier_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Soldier_DeadState(this, stateMachine, "dead", deadStateData, this);



        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }

        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

