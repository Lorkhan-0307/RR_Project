using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool jumpInput;
<<<<<<< Updated upstream
    private bool grabInput;
    private bool coyoteTime;
=======
    private bool jumpInputStop;
    private bool grabInput;
    private bool coyoteTime;
    private bool isJumping;
>>>>>>> Stashed changes
    private bool wallJumpCoyoteTime;
    private bool isTouchingLedge;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

<<<<<<< Updated upstream
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge();
=======
        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingWallBack = core.CollisionSenses.WallBack;
        isTouchingLedge = core.CollisionSenses.Ledge;
>>>>>>> Stashed changes

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
<<<<<<< Updated upstream
        grabInput = player.InputHandler.GrabInput;
        isGrounded = player.CheckIfGrounded();

        //Debug.Log($"{isTouchingWall}, {xInput}, {player.FacingDirection}, {player.CurrentVelocity.y}");
=======
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        isGrounded = core.CollisionSenses.Ground;

        CheckJumpMultiplier();

        //Debug.Log($"{isTouchingWall}, {xInput}, {core.Movement.FacingDirection}, {core.Movement.CurrentVelocity.y}");
>>>>>>> Stashed changes
        if (player.InputHandler.AttackInputs[(int)CombatInputs.melee])
        {
            stateMachine.ChangeState(player.MeleeAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.range])
        {
            stateMachine.ChangeState(player.RangeAttackState);
        }
<<<<<<< Updated upstream
        else if (isGrounded && player.CurrentVelocity.y < 0.01f)
=======
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
>>>>>>> Stashed changes
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(isTouchingWall && !isTouchingLedge &&!isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
<<<<<<< Updated upstream
            isTouchingWall = player.CheckIfTouchingWall();
=======
            isTouchingWall = core.CollisionSenses.WallFront;
>>>>>>> Stashed changes
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
            coyoteTime = false;
        }
        else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
<<<<<<< Updated upstream
        else if(isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
=======
        else if(isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0)
>>>>>>> Stashed changes
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
<<<<<<< Updated upstream
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
=======
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
>>>>>>> Stashed changes

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

<<<<<<< Updated upstream
=======
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

>>>>>>> Stashed changes
    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }


    public void StartCoyoteTime() => coyoteTime = true;

<<<<<<< Updated upstream
=======
    public void SetIsJumping() => isJumping = true;

>>>>>>> Stashed changes
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

}
