 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;

    private Rigidbody2D rb;

    [SerializeField]
    private float damageRadius;

    private bool hasHitGround;


    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    public float damageAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        xStartPos = transform.position.x;

    }

    private void FixedUpdate()
    {
        if(!hasHitGround)
        {
            Collider2D[] damageHit = Physics2D.OverlapCircleAll(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            foreach (Collider2D collider in damageHit)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage(damageAmount);
                }
                Destroy(gameObject);
            }

            if(groundHit)
            {
                /*hasHitGround = true;
                rb.velocity = Vector2.zero;*/
                Destroy(gameObject);
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
