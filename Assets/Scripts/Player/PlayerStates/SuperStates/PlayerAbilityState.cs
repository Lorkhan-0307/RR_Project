using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    private bool isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

<<<<<<< Updated upstream
        isGrounded = player.CheckIfGrounded();
=======
        isGrounded = core.CollisionSenses.Ground;
>>>>>>> Stashed changes
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAbilityDone)
        {
<<<<<<< Updated upstream
            if(isGrounded && player.CurrentVelocity.y < 0.01f)
=======
            if(isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
>>>>>>> Stashed changes
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
