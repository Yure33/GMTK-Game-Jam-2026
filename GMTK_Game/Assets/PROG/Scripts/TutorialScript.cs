using UnityEditor.Tilemaps;
using UnityEditor.Toolbars;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public float timer = 0f;
    private float timerAppear = 1.5f;
    private float timerDisappear = 10f;
    public bool awake;
    public bool appear = true;

    public Animator animator;
    void Start()
    {
        animator.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Appear", appear);
        timer += Time.deltaTime;
        if(timer > timerAppear & awake)
        {
            animator.speed = 1f;
            if (timer > timerDisappear)
            {
                appear = false;
                awake = false;
                timer = 0f;
            }
        }
    }
}
