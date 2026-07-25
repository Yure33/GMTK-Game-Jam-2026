using UnityEngine;

public class Botao : MonoBehaviour
{
    public SpriteRenderer spriteButton;
    public Sprite[] spriteButtonArray;
    public GameObject portaTimer;
    public ControlFPS_Script fpsControl;

    private bool saiu;

    public LayerMask playerLayer;
    public LayerMask caixaLayer;

    private void Update()
    {
        if(saiu && fpsControl.ConstanteAtivo)
        {
            portaTimer.SetActive(true);
            saiu = false;
            spriteButton.sprite = spriteButtonArray[0];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerLayer.Contains(collision.gameObject.layer) || caixaLayer.Contains(collision.gameObject.layer))
        {
            spriteButton.sprite = spriteButtonArray[1];
            portaTimer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer) || caixaLayer.Contains(collision.gameObject.layer))
        {
            saiu = true;
        }
    }
}
