using UnityEngine;
using UnityEngine.UI;

public class FitasCassetes : MonoBehaviour
{
    public Image sprite;
    public Sprite[] spriteArray;
    public PlayerSpawner spawner;
    void Start()
    {
        
    }
    void Update()
    {
        sprite.sprite = spriteArray[LifesVariables.playerLifes];
    }
}
