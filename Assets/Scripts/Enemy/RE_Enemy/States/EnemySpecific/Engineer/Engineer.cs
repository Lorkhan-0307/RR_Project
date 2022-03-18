using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Watch part 17 44:45
public class Engineer : Entity
{
    public Engineer_IdleState idleState { get; private set; }
    public Engineer_MoveState moveState { get; private set; }
    public Engineer_PlayerDetectedState playerDetectedState { get; private set; }
    public Engineer_ChargeState chargeState { get; private set; }
    public Engineer_LookForPlayerState lookForPlayerState { get; private set; }
    public Engineer_MeleeAttackState meleeAttackState { get; private set; }
    public Engineer_StunState stunState { get; private set; }
    public Engineer_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        moveState = new Engineer_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Engineer_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Engineer_PlayerDetectedState(this, stateMachine, "playerDetected",playerDetectedStateData, this);
        chargeState = new Engineer_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Engineer_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Engineer_MeleeAttackState(this, stateMachine, "meleeAttack",meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Engineer_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Engineer_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
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
}
