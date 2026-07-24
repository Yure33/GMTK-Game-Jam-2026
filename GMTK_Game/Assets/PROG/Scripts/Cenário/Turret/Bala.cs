using UnityEngine;

public class Bala : MonoBehaviour
{
    public float shotSpeed = 3f;
    public Turret turret;
    Rigidbody2D rb;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject turretObject = GameObject.FindGameObjectWithTag("Turret");
        turret = turretObject.GetComponent<Turret>();
        rb.linearVelocity = new Vector2(shotSpeed * turret.setDirection, rb.linearVelocityY);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(groundLayer.Contains(collision.gameObject.layer) || playerLayer.Contains(collision.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }
}
