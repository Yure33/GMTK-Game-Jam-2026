using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Transform groundCheckTransform;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public bool canJump = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckIsGrounded();
        animator.SetFloat("moveInput.x", moveInput.x);
        animator.SetFloat("moveInput.y", moveInput.y);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckIsGrounded()
    {

        Collider2D hit = Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, groundLayer);
        if(hit != null)
        {
            canJump = true;
        } else
        {
            canJump = false;
        }
    }
}