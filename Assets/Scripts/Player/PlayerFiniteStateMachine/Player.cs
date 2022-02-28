using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, stateMachine, playerData, "land");
        WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        FacingDirection = 1;

        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion


    #region Other Functions

    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
