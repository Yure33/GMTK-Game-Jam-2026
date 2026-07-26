using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static event Action OnMainFunctionCalled;
    Vector2 moveInput;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public GameObject PlayerDesligado;

    public Transform groundCheckTransform;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
    public LayerMask corpoLayer;
    public LayerMask caixaLayer;
    public LayerMask balaLayer;

    public LayerMask spikeLayer;
    public LayerMask fumacaLayer;

    public Botao button;
    public ControlFPS_Script fpsControlScript;
    public Alavanca alavancaScript;

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
            SoundFXManager.instance.jumpSound.PlayOneShot(SoundFXManager.instance.jumpSound.clip,0.12f);

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
        if(context.performed && fpsControlScript.Tape.TapeSlider.value > 0)
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
                fpsControlScript.ChangeFPS(15);
            }
            else if (fpsControlScript.targetFPS == 15f)
            {
                fpsControlScript.ChangeFPS(30);
            }
            else if (fpsControlScript.targetFPS == 30f)
            {
                fpsControlScript.ChangeFPS(5);
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnMainFunctionCalled?.Invoke();
        }
    }

    private void CheckIsGrounded()
    {
        Collider2D hitCorpo = Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, corpoLayer);
        Collider2D hit = Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, groundLayer);
        Collider2D hitCaixa = Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, caixaLayer);
        if (hit != null || hitCorpo != null || hitCaixa != null)
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
        SoundFXManager.instance.deathSound.PlayOneShot(SoundFXManager.instance.deathSound.clip,0.12f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(spikeLayer.Contains(collision.gameObject.layer) || balaLayer.Contains(collision.gameObject.layer))
        {
            TurnOffBody();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fumacaLayer.Contains(collision.gameObject.layer))
        {
            TurnOffBody();
        }
    }
}