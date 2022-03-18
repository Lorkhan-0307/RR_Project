using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entitydata;

    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    private Vector2 velocityWorkspace;

    [SerializeField]
    public Transform playerCheck;
    [SerializeField]
    public Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isStunned;
    protected bool isDead;

    

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        currentHealth = entitydata.maxHealth;
        currentStunResistance = entitydata.stunResistance;

        stateMachine = new FiniteStateMachine();
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);

        if(Time.time >= lastDamageTime + entitydata.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entitydata.minAgroDistance, entitydata.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entitydata.maxAgroDistance, entitydata.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entitydata.closeRangeActionDistance, entitydata.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Core.Movement.RB.velocity.x, velocity);
        Core.Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entitydata.stunResistance;
    }

    

    public virtual void OnDrawGizmos()
    {

        if (Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entitydata.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entitydata.ledgeCheckDistance));
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entitydata.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entitydata.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entitydata.maxAgroDistance), 0.2f);
        }
    }
}