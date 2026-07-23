using UnityEngine;

public class Cano : MonoBehaviour
{
    public BoxCollider2D fumacaCollider;

    public Animator animator;

    public float fumacaTimer = 0f;
    public float fumacaTimerActive = 0f;
    [SerializeField] float fumacaTime = 5f;

    public bool fumacaEnabled = true;
    public bool fumacando = false;
    void Start()
    {

    }

    void Update()
    {
        animator.SetBool("fumacaEnabled", fumacaEnabled);
        animator.SetBool("fumacando", fumacando);
        fumacaTimer += Time.deltaTime;
        if( fumacaTimer >= fumacaTime )
        {
            fumacaEnabled = true;
            fumacaTimerActive += Time.deltaTime;
            if( fumacaTimerActive >= fumacaTime)
            {
                fumacando = false;
                fumacaTimerActive = 0f;
                fumacaTimer = 0f;
            }
        }
    }

    private void Fumacando()
    {
        fumacaCollider.enabled = true;
        fumacando = true;
    }

    private void ParouFumacar()
    {
        fumacaCollider.enabled = false;
        fumacaEnabled = false;
    }
}
