
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
        //����Ű�� ������ ��
        if (playerInput.attack)
        {
            //������ ���� �ʴٸ�
            if (!isRolling)
            {
                Attack();
            }
        }

        //����Ű�� ������ ������ ���°� �ƴҶ�
        if(playerInput.jump && !isRolling)
        {
            /*
            //���� ����ִٸ�
            if (isGrounded)
            {
                jumpCount = 0;
                Jump();
            }
            else
            {   
                //�ִ� ���� Ƚ���� �������� �ʾҴٸ�
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

        //��ȣ�ۿ�
        if (playerInput.interact)
        {
            CheckInteraction();
        }
        //������ �� �ִ��� Ȯ��
        CheckIfCanJump();
        //WallSlide ������ Ȯ��
        CheckIfWallSliding();
    }
    private void FixedUpdate()
    {
        //������ �� �ִ��� ������ Ȯ��
        if (!CanMove())
        {
            return;
        }

        else
        {
            //���� ��ȯ �� ĳ���� flip
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
            //������ ���� ���� �� ������ �� ����
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
        //�˾� â�� �� ������ ������ �� ����
        if (null != FindObjectOfType<PopUp>())
        {
            if (FindObjectOfType<PopUp>().isOpen)
            {
                can = false;
            }
        }

        return can;
    }

    //���� �� �������� ���ϰ� ��
    public void disableMove()
    {
        can = false;
        playerRigidbody.velocity = new Vector2(0, 0);
    }

    //���� ���� ������ �� �ٽ� ������ �� �ְ� ��
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
        //���ݹ��� ����
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //GroundCheck ���� ����
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //WallCheck ���� ����
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

        //WallSlide�߿� move�� ���ٸ�
        else if(isWallSliding && inputX == 0 && canJump)
        {
            isWallSliding = false;
            jumpCount++;
            //WallHop�������� addforce
            Vector2 ForceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -m_facingDirection, wallHopForce * wallHopDirection.y);
            playerRigidbody.AddForce(ForceToAdd, ForceMode2D.Impulse);
        }

        //move�� �ִٸ�
        else if ((isWallSliding || isTouchingWall) && inputX!=0 && canJump)
        {
            isWallSliding = false;
            jumpCount++;
            //WallJump�������� addforce
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

    //�浹ü�� ��ȣ�ۿ� ��� �Լ�
    public void CheckInteraction()
    {
        //�浹ü�� ���� ã��
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

    //��ȣ�ۿ� ������ On
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    //��ȣ�ۿ� ������ Off
    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

}