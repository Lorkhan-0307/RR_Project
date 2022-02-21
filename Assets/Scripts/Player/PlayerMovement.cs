using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 10f;
    public float jumpPower = 10f;
    public float rollspeed = 20f;
    public float RollCoolTime = 3f;
    public Animator animator;
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    private bool isJumping = false;
    private bool isRolling = false;
    private int m_facingDirection = 1;
    public SpriteRenderer m_SR;
    public float meleeAttackDamage = 20f;
    public Transform startingPoint;

    private bool can = true;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;




    public GameObject interactIcon;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private float m_disableMovementTimer = 0.0f;
    private float inputX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPoint.transform.position;
    }

    private void Update()
    {
        if (playerInput.attack)
        {
            if (!isRolling)
            {
                Attack();
            }
        }

        //점프 or 구르기 중에는 점프 못함.
        if (!isJumping && playerInput.jump > 0 && !isRolling)
        {
            Jump();
        }

        if (!isRolling && playerInput.roll == true)
        {
            isRolling = true;
            Roll();
        }

        //상호작용
        if (playerInput.interact)
        {
            CheckInteraction();
        }

    }
    private void FixedUpdate()
    {
        //움직일 수 있는지 없는지 확인
        if (!CanMove())
        {
            return;
        }

        else
        {
            //방향 전환 시 캐릭터 flip
            if (playerInput.move > 0)
            {
                m_SR.flipX = false;
                m_facingDirection = 1;
            }
            else if (playerInput.move < 0)
            {
                m_SR.flipX = true;
                m_facingDirection = -1;
            }
            //구르고 있지 않을 때 움직일 수 있음
            if (!isRolling)
            {
                inputX = playerInput.move;
                Move();
            }
        }
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector2(inputX * moveSpeed, playerRigidbody.velocity.y);
        animator.SetFloat("Walkspeed", Mathf.Abs(inputX * moveSpeed));
    }

    private void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().E_TakeDamage(meleeAttackDamage);
        }


    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    private void Jump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);

    }

    private void Roll()
    {
        animator.SetTrigger("roll");
        playerRigidbody.velocity = new Vector2(rollspeed * m_facingDirection, playerRigidbody.velocity.y);
    }

    public void ResetRoll()
    {
        isRolling = false;
    }

    private bool CanMove()
    {
        //팝업 창이 떠 있으면 움직일 수 없음
        if (null != FindObjectOfType<PopUp>())
        {
            if (FindObjectOfType<PopUp>().isOpen)
            {
                can = false;
            }
        }

        return can;
    }

    //공격 시 움직이지 못하게 함
    public void disableMove()
    {
        can = false;
        playerRigidbody.velocity = new Vector2(0, 0);
    }

    //공격 동작 마무리 시 다시 움직일 수 있게 함
    public void enableMove()
    {
        can = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;

        }
    }

    //상호작용 아이콘 On
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    //상호작용 아이콘 Off
    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    //충돌체와 상호작용 기능 함수
    public void CheckInteraction()
    {
        //충돌체를 전부 찾음
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}