using UnityEngine;

public class Botao : MonoBehaviour
{
    public SpriteRenderer spriteButton;
    public Sprite[] spriteButtonArray;
    public PortaTimer portaTimerScript;

    public LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerLayer.Contains(collision.gameObject.layer))
        {
            spriteButton.sprite = spriteButtonArray[1];
            portaTimerScript.portaTimer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            spriteButton.sprite = spriteButtonArray[0];
            portaTimerScript.timerRodando = true;
        }
    }
}
