using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : Entity
{
    public Engineer_IdleState idleState { get; private set; }
    public Engineer_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new Engineer_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Engineer_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(moveState);
    }
}
