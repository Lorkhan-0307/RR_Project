using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//레이어를 player로 두지 말것!!


public class MeleeEnemy : MonoBehaviour
{

    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float Yrange;

    [SerializeField] private float damage;


    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;


    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private PlayerHealth Phealth;

    private Animator anim;


    private EnemyPatrol enemypatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        
        //Attack only when player on sight

        if(PlayerInSight())
        { 
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                //Attack

            }
        }

        if(enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInSight();
        }
    }

    //Finding player by RaycastHit2d
    private bool PlayerInSight()
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y*Yrange, boxCollider.bounds.size.z), 0,Vector2.left,0,playerLayer);

        if(hit.collider != null)
        {
            Phealth = hit.transform.GetComponent<PlayerHealth>();
        }

        return hit.collider != null;
    }


    //Check boundaries for Finding player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y*Yrange, boxCollider.bounds.size.z));

    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            Phealth.P_TakeDamage(damage);
        }
    }
}
