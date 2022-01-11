using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 10f;
    public float jumpPower = 10f;
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        //점프 중에는 점프 못함.
        if (!isJumping && playerInput.jump > 0)
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector2 moveDistance = new Vector2(playerInput.move, 0) * moveSpeed * Time.fixedDeltaTime;
        //playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        playerRigidbody.position += moveDistance;
    }

    private void Jump()
    {
        Vector2 jumpVelocity = new Vector2(0, jumpPower);

        playerRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
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
}
