using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] string NomePrimeiraCena;
    [SerializeField] GameObject[] HUDS;

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
