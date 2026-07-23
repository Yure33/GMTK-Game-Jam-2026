using UnityEngine;

public class TapeEffectSpeed : MonoBehaviour
{
    private Animator animator;

    public ControlFPS_Script fpsControl;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!fpsControl.ConstanteAtivo)
        {
            animator.speed = 0f;
            return;
        }
        if(fpsControl.targetFPS == 5f)
        {
            animator.speed = 1f;
        }
        if (fpsControl.targetFPS == 15f)
        {
            animator.speed = 2f;
        }
        if (fpsControl.targetFPS == 30f)
        {
            animator.speed = 3f;
        }
    }
}
