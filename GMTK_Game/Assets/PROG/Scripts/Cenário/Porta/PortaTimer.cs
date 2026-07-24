using UnityEngine;

public class PortaTimer : MonoBehaviour
{
    public float TimerPorta = 0f;
    public float MaxTimerPorta = 5f;
    public bool timerRodando;

    public GameObject portaTimer;
    public ControlFPS_Script fpsControl;

    void Update()
    {
        if(timerRodando && fpsControl.ConstanteAtivo)
        {
            TimerPorta += Time.deltaTime;
            if(TimerPorta >= MaxTimerPorta)
            {
                portaTimer.SetActive(true);
                timerRodando = false;
                TimerPorta = 0f;
            }
        }
    }

}
