using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //input variables
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;

    //bool variables
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isJumping;
    private bool isTouchingLedge;

    //time variables
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private float startWallJumpCoyoteTime;

    //Core components
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected CollisionSenses collisionSenses;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected Movement movement;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = CollisionSenses.Ground;
        isTouchingWall = CollisionSenses.WallFront;
        isTouchingWallBack = CollisionSenses.WallBack;
        isTouchingLedge = CollisionSenses.LedgeHorizontal;

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

        //input variables
        xInput = player.InputHandler.NormInputX;
        //yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        isGrounded = CollisionSenses.Ground;

        CheckJumpMultiplier();

        //Debug.Log($"jumpinput: {jumpInput}, isTouchingWall: {isTouchingWall}, isTouchingWallBack: {isTouchingWallBack}, wallJumpCoyoteTime: {wallJumpCoyoteTime}");

        if (player.InputHandler.AttackInputs[(int)CombatInputs.melee])
        {
            stateMachine.ChangeState(player.MeleeAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.range])
        {
            stateMachine.ChangeState(player.RangeAttackState);
        }
        //change to land state
        else if (isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        //change to LedgeClimbState 
        else if (isTouchingWall && !isTouchingLedge &&!isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        //change to WallJumpState 
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        //change to JumpState 
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
            coyoteTime = false;
        }
        //change to WallGrabState 
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        //change to WallSlideState 
        else if (isTouchingWall && xInput == Movement.FacingDirection && Movement.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        //set animation
        else
        {
            Movement.CheckIfShouldFlip(xInput);
            Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

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

    public void SetIsJumping() => isJumping = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

}
