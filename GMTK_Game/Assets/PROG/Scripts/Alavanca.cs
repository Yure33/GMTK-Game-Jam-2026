using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Alavanca : MonoBehaviour
{
    public LayerMask playerLayer;

    public PlayerMovement playerMovement;
    public GameObject[] porta;
    public SpriteRenderer sprite;
    public bool alavanca;

    void OnEnable() { PlayerMovement.OnMainFunctionCalled += ToggleDoor; }
    public void ToggleDoor()
    {
        if(alavanca)
        {
                sprite.flipX = !sprite.flipX;
            foreach (GameObject obj in porta)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                    SoundFXManager.instance.openDoor.PlayOneShot(SoundFXManager.instance.openDoor.clip,1f);
                }
                else
                {
                    obj.SetActive(true);
                    SoundFXManager.instance.closeDoor.PlayOneShot(SoundFXManager.instance.closeDoor.clip,1f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            alavanca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            alavanca = false;
        }
    }
}
