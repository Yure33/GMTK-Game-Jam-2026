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
            foreach (GameObject obj in porta)
            {
                sprite.flipX = !sprite.flipX;
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
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
