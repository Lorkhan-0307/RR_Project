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
    //private bool isGrounded = false; //이 변수 잠시 놔두기(나중에 쓸 것 같음)
    private bool isRolling = false;
    private int m_facingDirection = 1;
    public SpriteRenderer m_SR;
    public float meleeAttackDamage = 20f;
    public Transform startingPoint;

    private bool can = true;
    private int jumpCount;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int amountsOfJump = 3;


    public GameObject interactIcon;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private float inputX = 0.0f;

    public CameraShake camerashake;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPoint.transform.position;
    }

    private void Update()
    {
        //공격키를 눌렀을 때
        if (playerInput.attack)
        {
            //구르고 있지 않다면
            if (!isRolling)
            {
                Attack();
            }
        }

        // 점프키를 눌렀을 때 최대 점프 횟수에 도달하지 않았다면 and 구르는 상태가 아니라면
        if (playerInput.jump && jumpCount < amountsOfJump && !isRolling)
        {
            jumpCount++;
            Jump();
            Debug.Log("jumpcount: " + jumpCount);
        }

        if (!isRolling && playerInput.roll == true && can == true)
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

        camerashake.GetComponent<CameraShake>().NormalAttackShake();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        //어떤 콜라이더와 닿았을때 "Ground"이면
        if (collision.gameObject.tag == "Ground")
        {
            //jumpCount를 0으로 리셋
            //isGrounded = true;
            jumpCount = 0;
        }*/

        if (collision.contacts[0].normal.y > 0.7f)
        {
            //isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = false;
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