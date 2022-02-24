
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
    private bool isGrounded;
    private bool canJump;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isRolling = false;
    private int m_facingDirection = 1;
    public SpriteRenderer m_SR;
    public float meleeAttackDamage = 20f;
    public Transform startingPoint;

    private bool can = true;
    private int jumpCount;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform attackPoint;
    public Transform groundCheck;
    public Transform wallCheck;
    public float attackRange = 0.5f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float wallHopForce;
    public float wallJumpForce;
    public LayerMask enemyLayers;
    public LayerMask whatIsGround;
    public int maxJumpCount;


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
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();

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

        //점프키를 누르고 구르는 상태가 아닐때
        if(playerInput.jump && !isRolling)
        {
            /*
            //땅에 닿아있다면
            if (isGrounded)
            {
                jumpCount = 0;
                Jump();
            }
            else
            {   
                //최대 점프 횟수에 도달하지 않았다면
                if (jumpCount < maxJumpCount)
                {
                    Jump();
                }
            }*/
            Jump();
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
        //점프할 수 있는지 확인
        CheckIfCanJump();
        //WallSlide 중인지 확인
        CheckIfWallSliding();
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
        CheckSurroundings();
    }

    private void Move()
    {
        if(isGrounded)
        {
            playerRigidbody.velocity = new Vector2(inputX * moveSpeed, playerRigidbody.velocity.y);
            animator.SetFloat("Walkspeed", Mathf.Abs(inputX * moveSpeed));
        }

        else if(!isGrounded && !isWallSliding && inputX != 0)
        {
            Vector2 ForceToAdd = new Vector2(inputX * movementForceInAir, 0);
            playerRigidbody.AddForce(ForceToAdd, ForceMode2D.Impulse);

            if (Mathf.Abs(playerRigidbody.velocity.x) > moveSpeed)
            {
                playerRigidbody.velocity = new Vector2(inputX * moveSpeed, playerRigidbody.velocity.y);
            }
        }

        else if(!isGrounded && !isWallSliding && inputX == 0)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * airDragMultiplier, playerRigidbody.velocity.y);
        }

        if (isWallSliding)
        {
            if (playerRigidbody.velocity.y < -wallSlideSpeed)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -wallSlideSpeed);
            }
        }
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
        //공격범위 설정
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //GroundCheck 범위 설정
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //WallCheck 범위 설정
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }


    private void Jump()
    {
        /*jumpCount++;
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);*/

        if (canJump && !isWallSliding)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);
            jumpCount++;
        }

        //WallSlide중에 move값 없다면
        else if(isWallSliding && inputX == 0 && canJump)
        {
            isWallSliding = false;
            jumpCount++;
            //WallHop방향으로 addforce
            Vector2 ForceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -m_facingDirection, wallHopForce * wallHopDirection.y);
            playerRigidbody.AddForce(ForceToAdd, ForceMode2D.Impulse);
        }

        //move값 있다면
        else if ((isWallSliding || isTouchingWall) && inputX!=0 && canJump)
        {
            isWallSliding = false;
            jumpCount++;
            //WallJump방향으로 addforce
            Vector2 ForceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * inputX, wallJumpForce * wallJumpDirection.y);
            playerRigidbody.AddForce(ForceToAdd, ForceMode2D.Impulse);
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, (m_facingDirection == 1?transform.right:-transform.right), wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if((isGrounded && playerRigidbody.velocity.y <= 0) || isWallSliding)
        {
            jumpCount = 0;
        }

        if (jumpCount >= maxJumpCount)
        {
            canJump = false;
        }

        else
        {
            canJump = true;
        }
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && playerRigidbody.velocity.y<=0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
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

}