using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDodgeState DodgeState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerAttackState MeleeAttackState { get; private set; }
    public PlayerAttackState RangeAttackState { get; private set; }
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    #endregion


    #region Other Variables

    [SerializeField]
    private PlayerData playerData;
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        DodgeState = new PlayerDodgeState(this, stateMachine, playerData, "dodge");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, stateMachine, playerData, "land");
        WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
        MeleeAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        RangeAttackState= new PlayerAttackState(this, stateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Inventory = GetComponent<PlayerInventory>();

        MeleeAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.melee]);

        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    //private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    #endregion

}
