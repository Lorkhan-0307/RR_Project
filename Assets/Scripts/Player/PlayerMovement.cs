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
    //private bool isGrounded = false; //�� ���� ��� ���α�(���߿� �� �� ����)
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
        //����Ű�� ������ ��
        if (playerInput.attack)
        {
            //������ ���� �ʴٸ�
            if (!isRolling)
            {
                Attack();
            }
        }

        // ����Ű�� ������ �� �ִ� ���� Ƚ���� �������� �ʾҴٸ� and ������ ���°� �ƴ϶��
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        //� �ݶ��̴��� ������� "Ground"�̸�
        if (collision.gameObject.tag == "Ground")
        {
            //jumpCount�� 0���� ����
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
}