using System.Collections;
using UnityEngine;

public class ObjetosDinamicos_Controlador : MonoBehaviour
{
    [SerializeField] int FPS_Exigido;
    [SerializeField] float WaitTime;
    [SerializeField] ConfigsPersist fpsGame;
    [SerializeField] Color[] invisible_visible;
    int savePastFps;
    bool piscaPisca;

    void Start()
    {
        StartCoroutine(UpdateRetardado());
    }

    IEnumerator UpdateRetardado()
    {
        while(true){
            if(savePastFps != fpsGame.FPS && fpsGame.FPS > FPS_Exigido){
                //DEIXAR TRANSPARENTE AS CRIANÇAS
                foreach(Transform nenem in transform)
                {
                    nenem.gameObject.SetActive(true);
                    nenem.GetComponent<Collider2D>().enabled = false;
                    nenem.GetComponent<SpriteRenderer>().color = invisible_visible[0];
                }
                Debug.Log("Transparente");
            }
            else if(savePastFps != fpsGame.FPS && fpsGame.FPS == FPS_Exigido){
                //MOSTRAR CRIANÇAS
                foreach(Transform nenem in transform)
                {
                    nenem.gameObject.SetActive(true);
                    nenem.GetComponent<Collider2D>().enabled = true;
                    nenem.GetComponent<SpriteRenderer>().color = invisible_visible[1];
                }
                Debug.Log("Mostrei");
            }
            else if(savePastFps != fpsGame.FPS || fpsGame.FPS < FPS_Exigido){
                //PISCAR OBJS
                foreach(Transform nenem in transform)
                {
                    nenem.gameObject.SetActive(piscaPisca);
                    nenem.GetComponent<Collider2D>().enabled = false;
                    nenem.GetComponent<SpriteRenderer>().color = invisible_visible[0];
                }
                Debug.Log("Piscando");
                //INVERTER ATIVAÇÃO
                piscaPisca = !piscaPisca;
            }
            savePastFps = fpsGame.FPS;
            yield return new WaitForSeconds(WaitTime);
        }
    }
}
