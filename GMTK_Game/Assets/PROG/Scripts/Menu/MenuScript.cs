using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] string NomePrimeiraCena;
    [SerializeField] GameObject[] HUDS;
    [SerializeField] Vector3[] PossiblePositions;
    [SerializeField] RectTransform Tape;
    bool Options;
    int Selected;

    public void ChangeSelected(InputAction.CallbackContext context)
    {
        if(context.performed && context.ReadValue<Vector2>().y != 0 && !Options)
        {
            Selected -= (int)context.ReadValue<Vector2>().y;
            if(Selected > 2){
                Selected = 0;
            }
            else if(Selected < 0){
                Selected = 2;
            }
        }
    }

    public void OnSelected(InputAction.CallbackContext context){
        if(context.performed && !Options)
        {
            switch(Selected){
                case 0:
                    //START
                    OnPlay();
                    break;
                case 1:
                    //OPTIONs
                    OnOpt();
                    Options = true;
                    Selected = 3;
                    break;
                case 2:
                    //EXIT
                    OnExit(false);
                    break;
            }
        }
        else if(context.performed)
        {
            OnExit(true);
            Options = false;
            Selected = 1;
        }
    }

    void Update()
    {
        Tape.anchoredPosition = Vector2.Lerp(Tape.anchoredPosition, PossiblePositions[Selected], 0.3f);
    }

    public void OnPlay()
    {
        //FAZER UMA PEQUENA TRANSIÇÃO PRIMEIRO
        //DEPOIS CARREGAR CENA
        SceneManager.LoadScene(NomePrimeiraCena);
    }
    public void OnOpt()
    {
        //DESATIVAR HUD QUE TEM O PLAY
        HUDS[0].SetActive(false);  
        //ATIVAR HUD DAS OPÇÕES
        HUDS[1].SetActive(true);
    }
    public void OnExit(bool Opt)
    {
        //OPT == NAS OPÇÕES?
        switch(Opt){
            case false:
                //SAIR DO JOGO
                Debug.Log("Saindo...");
                Application.Quit();
                break;
            case true:
                //OnOpt inverso
                HUDS[0].SetActive(true);
                HUDS[1].SetActive(false);
                break;
        }
    }
}
