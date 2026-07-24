using UnityEngine;
using UnityEngine.UI;

public class ControlFPS_Script : MonoBehaviour
{
    public Camera BaseCam;
    public Camera MainCam;

    public PlayerMovement player;
    public Image velocimero;
    public Sprite[] velocimeroArray;

    public bool ConstanteAtivo;
    public float targetFPS;
    [SerializeField] RemainingTape_Script Tape;
    [SerializeField] int StartTape;
    [SerializeField] ConfigsPersist config;

    float IntervaloUpt;
    float timer;

    void Awake()
    {
        IntervaloUpt = 1f/targetFPS;
        Tape.StartSlider(StartTape);
    }
    void Start()
    {
        ChangeFPS(30);
        BaseCam.enabled = false;
        BaseCam.Render();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ConstanteAtivo){
            return;
        }

        BaseCam.enabled = false;

        IntervaloUpt = 1f/Mathf.Max(1f, targetFPS);

        timer += Time.unscaledDeltaTime;
        if(timer >= IntervaloUpt){
            Tape.UpdateSlider(1);
            BaseCam.Render();
            timer = 0f;
        }
    }

    public void ChangeFPS(int FPS)
    {
        targetFPS = FPS;
        config.FPS = FPS;
        velocimero.sprite = velocimeroArray[(int)(targetFPS / 15)];
    }
}
