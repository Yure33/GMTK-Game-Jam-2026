using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float fireRate = 0f;
    [SerializeField] float fireTime = 1.0f;
    public float setDirection;
    public Transform firePoint;
    public GameObject bala;
    public Rigidbody2D balaRb;
    public SpriteRenderer balaSprite;
    public SpriteRenderer sprite;

    private void Start()
    {

    }
    void Update()
    {
        if (sprite.flipX)
        {
            balaSprite.flipX = true;
            setDirection = 1;
        } else
        {
            balaSprite.flipX = false;
            setDirection = -1;
        }
        fireRate += Time.deltaTime;
        if(fireRate > fireTime)
        {
            Shoot();
            fireRate = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bala, firePoint.position, firePoint.rotation);
    }
}
