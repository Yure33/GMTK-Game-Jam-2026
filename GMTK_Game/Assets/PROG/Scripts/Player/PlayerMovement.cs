using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public GameObject PlayerDesligado;

    public Transform groundCheckTransform;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;

    public LayerMask spikeLayer;

    ControlFPS_Script fpsControlScript;

    public Slider slider;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float setDirection = 1f;

    public bool canJump = false;
    public bool destroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject fpsControl = GameObject.FindGameObjectWithTag("FpsController");
        fpsControlScript = fpsControl.GetComponent<ControlFPS_Script>();
        GameObject fpsSlider = GameObject.FindGameObjectWithTag("FpsSlider"); 
        slider = fpsSlider.GetComponent<Slider>();
    }
    private void Update()
    {
        slider.value = (int)fpsControlScript.targetFPS / 15;
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
            setDirection = 1;
            spriteRenderer.flipX = false;
        }
        if (moveInput.x < 0)
        {
            setDirection = -1;
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

    public void TurnOff(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TurnOffBody();
        }
    }

    public void TakeFilm(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            fpsControlScript.ConstanteAtivo = !fpsControlScript.ConstanteAtivo;
        }
    }

    public void ChangeFPS(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(fpsControlScript.targetFPS == 5f)
            {

                fpsControlScript.targetFPS = 15f;
            }
            else if (fpsControlScript.targetFPS == 15f)
            {
                fpsControlScript.targetFPS = 30f;
            }
            else if (fpsControlScript.targetFPS == 30f)
            {
                fpsControlScript.targetFPS = 5f;
            }
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

    private void TurnOffBody()
    {
        SpriteRenderer spriteRendererCorpo = Instantiate(PlayerDesligado, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>();
        if(setDirection == 1)
        {
            spriteRendererCorpo.flipX = false;
        } if (setDirection == -1)
        {  
            spriteRendererCorpo.flipX = true; 
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(spikeLayer.Contains(collision.gameObject.layer))
        {
            TurnOffBody();
        }
    }
}