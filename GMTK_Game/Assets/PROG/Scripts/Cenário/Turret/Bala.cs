using UnityEngine;

public class Bala : MonoBehaviour
{
    public float shotSpeed = 4f;
    public Turret turret;
    Rigidbody2D rb;
    Collider2D col;

    public ControlFPS_Script fpsControl;
    public ObjetosDinamicos_Controlador objControl;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        GameObject turretObject = GameObject.FindGameObjectWithTag("Turret");
        turret = turretObject.GetComponent<Turret>();
        GameObject fpsControlObject = GameObject.FindGameObjectWithTag("FpsController");
        fpsControl = fpsControlObject.GetComponent<ControlFPS_Script>();
        objControl = GetComponentInParent<ObjetosDinamicos_Controlador>();

        rb.linearVelocity = new Vector2(shotSpeed * turret.setDirection, rb.linearVelocityY);
    }

    private void Update()
    {
        if (fpsControl.targetFPS == objControl.FPS_Exigido)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
        if (fpsControl.ConstanteAtivo)
        {
            rb.linearVelocity = new Vector2(shotSpeed * turret.setDirection, rb.linearVelocityY);
        } else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(groundLayer.Contains(collision.gameObject.layer) || (playerLayer.Contains(collision.gameObject.layer) && fpsControl.targetFPS == objControl.FPS_Exigido))
        {
            Destroy(gameObject);
        }
    }
}
