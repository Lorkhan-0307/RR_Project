using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigboy : Entity
{
    public Bigboy_IdleState idleState { get; private set; }
    public Bigboy_MoveState moveState { get; private set; }
    public Bigboy_PlayerDetectedState playerDetectedState { get; private set; }
    public Bigboy_ChargeState chargeState { get; private set; }
    public Bigboy_LookForPlayerState lookForPlayerState { get; private set; }
    public Bigboy_MeleeAttackState meleeAttackState { get; private set; }
    public Bigboy_RangeAttackState rangeAttackState { get; private set; }
    public Bigboy_StunState stunState { get; private set; }


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
    private D_RangeAttackState rangeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;



    [SerializeField]
    Transform RangeAttackPosition;
    [SerializeField]
    Transform MeleeAttackPosition;

    public override void Awake()
    {
        base.Awake();
        idleState = new Bigboy_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Bigboy_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Bigboy_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Bigboy_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Bigboy_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Bigboy_MeleeAttackState(this, stateMachine, "meleeAttack", MeleeAttackPosition, meleeAttackStateData, this);
        rangeAttackState = new Bigboy_RangeAttackState(this, stateMachine, "rangeAttack", RangeAttackPosition, rangeAttackStateData, this);
        stunState = new Bigboy_StunState(this, stateMachine, "stun", stunStateData, this);


    }

    public override bool CheckPlayerInCloseRangeAction()
    {
        return base.CheckPlayerInCloseRangeAction();
    }

    public override bool CheckPlayerInMaxAgroRange()
    {
        return base.CheckPlayerInMaxAgroRange();
    }

    public override bool CheckPlayerInMinAgroRange()
    {
        return base.CheckPlayerInMinAgroRange();
    }

    public override void DamageHop(float velocity)
    {
        base.DamageHop(velocity);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void ResetStunResistance()
    {
        base.ResetStunResistance();
    }

    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    public override void Update()
    {
        base.Update();
    }
}
