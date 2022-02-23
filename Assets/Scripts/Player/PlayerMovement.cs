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

        //���� or ������ �߿��� ���� ����.
        if (!isJumping && playerInput.jump > 0 && !isRolling)
        {
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


    /*


    bool onClimbable = false;
    bool isClimbing = false;
    float climbPercentage;
    float ClimbingSpeed = 0.5f;
    Vector2 vectorStart, vectorEnd; //Starting and Ending Point of the Climbable

    void UseClimbable()
    {
        float inputVer = playerInput.jump;

        if (inputVer != 0)
        {
            //Climb base on the percentage so we could back and forward based on the inputVer
            climbPercentage += Time.deltaTime * ClimbingSpeed * inputVer;
            this.gameObject.transform.position = Vector2.Lerp(vectorStart, vectorEnd, climbPercentage);
        }

        climbPercentage = Mathf.Clamp01(climbPercentage);

        //if the Player reaches any end he can move again
        if (climbPercentage == 0 || climbPercentage == 1)
        {
            isClimbing = false;
            //enableMove();
        }
        else
        {
            isClimbing = true;
            //disableMove();
        }
    }

    //Called to set the Climbable Data
    public void SetClimbableData(bool onClimbable, Vector2 StartY, Vector2 EndY, bool isDown, float ClimbingSpeed)
    {
        this.onClimbable = onClimbable;

        this.vectorStart = StartY;
        this.vectorEnd = EndY;

        //to Check at what end the Player is
        if (isDown)
            climbPercentage = 0;
        else
            climbPercentage = 1;

        this.ClimbingSpeed = ClimbingSpeed;
    }
    public void OffClimbable()
    {
        onClimbable = false;
    }
    */

}