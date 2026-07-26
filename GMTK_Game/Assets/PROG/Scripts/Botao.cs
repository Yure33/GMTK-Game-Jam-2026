using UnityEngine;
public class Botao : MonoBehaviour
{
    public SpriteRenderer spriteButton;
    public Sprite[] spriteButtonArray;
    public GameObject[] portaTimer;
    public ControlFPS_Script fpsControlScript;

    private bool saiu;

    public LayerMask playerLayer;
    public LayerMask caixaLayer;
    public LayerMask corpoLayer;

    private void Start()
    {
        GameObject fpsControl = GameObject.FindGameObjectWithTag("FpsController");
        fpsControlScript = fpsControl.GetComponent<ControlFPS_Script>();
    }
    private void Update()
    {
        if (saiu && fpsControlScript.ConstanteAtivo)
        {
            foreach (GameObject obj in portaTimer)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                    SoundFXManager.instance.openDoor.PlayOneShot(SoundFXManager.instance.openDoor.clip, 1f);
                }
                else
                {
                    obj.SetActive(true);
                    SoundFXManager.instance.closeDoor.PlayOneShot(SoundFXManager.instance.closeDoor.clip, 1f);
                }
                saiu = false;
            }
            spriteButton.sprite = spriteButtonArray[0];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((playerLayer.Contains(collision.gameObject.layer) || caixaLayer.Contains(collision.gameObject.layer) || corpoLayer.Contains(collision.gameObject.layer)) && fpsControlScript.ConstanteAtivo)
        {
                foreach (GameObject obj in portaTimer)
                {
                    if(obj.activeSelf)
                    {
                        obj.SetActive(false);
                        SoundFXManager.instance.openDoor.PlayOneShot(SoundFXManager.instance.openDoor.clip, 1f);
                    }
                    else
                    {
                        obj.SetActive(true);
                        SoundFXManager.instance.closeDoor.PlayOneShot(SoundFXManager.instance.closeDoor.clip, 1f);
                    }
                spriteButton.sprite = spriteButtonArray[1];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer) || caixaLayer.Contains(collision.gameObject.layer) || corpoLayer.Contains(collision.gameObject.layer))
        {
            saiu = true;
        }
    }
}
