using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountsOfJumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountsOfJumpLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountsOfJumpLeft--;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountsOfJumpLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountsOfJumpLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountsOfJumpLeft--;
}
