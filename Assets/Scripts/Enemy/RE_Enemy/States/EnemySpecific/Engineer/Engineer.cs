using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : Entity
{
    public Engineer_IdleState idleState { get; private set; }
    public Engineer_MoveState moveState { get; private set; }
    public Engineer_PlayerDetectedState playerDetectedState { get; private set; }
    public Engineer_ChargeState chargeState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    public override void Start()
    {
        base.Start();

        moveState = new Engineer_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Engineer_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Engineer_PlayerDetectedState(this, stateMachine, "playerDetected",playerDetectedStateData, this);
        chargeState = new Engineer_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        stateMachine.Initialize(moveState);
    }
}
