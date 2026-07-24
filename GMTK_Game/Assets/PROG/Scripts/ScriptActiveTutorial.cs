using UnityEngine;

public class ScriptActiveTutorial : MonoBehaviour
{
    public LayerMask playerLayer;

    public TutorialScript tutorial;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(playerLayer.Contains(other.gameObject.layer))
        {
            tutorial.awake = true;
        }
    }
}
