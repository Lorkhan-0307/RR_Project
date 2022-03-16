using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
<<<<<<< Updated upstream
            player.SetVelocityY(-playerData.wallSlideVelocity);
=======
            core.Movement.SetVelocityY(-playerData.wallSlideVelocity);
>>>>>>> Stashed changes

            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
