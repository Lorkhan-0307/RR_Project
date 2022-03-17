using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entitydata;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }

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

    

    public virtual void Start()
    {
        currentHealth = entitydata.maxHealth;
        currentStunResistance = entitydata.stunResistance;
        stateMachine = new FiniteStateMachine();
        facingDirection = 1;
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        
    }

    public virtual void Update()
    {
        Debug.Log("Entity Update");
        stateMachine.currentState.LogicUpdate();

        if(Time.time >= lastDamageTime + entitydata.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;

    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entitydata.wallCheckDistance, entitydata.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entitydata.ledgeCheckDistance, entitydata.whatIsGround);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entitydata.groundCheckRadious, entitydata.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entitydata.minAgroDistance, entitydata.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entitydata.maxAgroDistance, entitydata.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAttack()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entitydata.closeRangeActionDistance, entitydata.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entitydata.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;


        DamageHop(entitydata.damageHopSpeed);

        Instantiate(entitydata.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right*facingDirection*entitydata.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entitydata.ledgeCheckDistance));


        //GIZMOS FOR CLOSE RANGE ATTACK, MINAGRODISTANCE AND MAX AGRO DISTANCE
        //TURNING THIS ON MAKES SOME NULLREFERENCE EXCEPTION, BUT WHEN YOU PLAY THE GAME, IT WORKS FINE SO NO WORRIES :p
        /*Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(aliveGO.transform.right * entitydata.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(aliveGO.transform.right * entitydata.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(aliveGO.transform.right * entitydata.maxAgroDistance), 0.2f);*/
    }
}