using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float fireRate = 0f;
    [SerializeField] float fireTime = 1.5f;
    public float setDirection;
    public Transform firePoint;
    public GameObject bala;
    public Rigidbody2D balaRb;
    public LayerMask balaLayer;
    public SpriteRenderer balaSprite;
    public SpriteRenderer sprite;
    public ControlFPS_Script fpsControl;
    public ObjetosDinamicos_Controlador objControl;

    public bool balaYDirection;
    public float Ydirection;

    private void Start()
    {

    }
    void Update()
    {
        if (fpsControl.targetFPS > objControl.FPS_Exigido)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bala"));
            {
                sprite.color = objControl.invisible_visible[0];
                balaSprite.color = objControl.invisible_visible[0];
                //bala.layer = 0;
            }
        }
        if(fpsControl.targetFPS == objControl.FPS_Exigido)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bala")) ;
            {
                sprite.color = objControl.invisible_visible[1];
                balaSprite.color = objControl.invisible_visible[1];
                //bala.layer = 10 << 0;
            }
        }
        if (sprite.flipX)
        {
            balaSprite.flipX = true;
            setDirection = 1;
        } else
        {
            balaSprite.flipX = false;
            setDirection = -1;
        }
        if(!fpsControl.ConstanteAtivo)
        {
            return;
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
        Instantiate(bala, firePoint.position, firePoint.rotation, transform.parent);
    }
}
