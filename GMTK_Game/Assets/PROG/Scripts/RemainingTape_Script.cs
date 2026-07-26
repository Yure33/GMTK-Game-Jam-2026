using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemainingTape_Script : MonoBehaviour
{
    public Slider TapeSlider;
    [SerializeField] TextMeshProUGUI TapeLeft;
    [SerializeField] int ReduçãoTESTE;

    public void StartSlider(int Maximo)
    {
        TapeSlider.maxValue = Maximo;
        TapeSlider.value = Maximo;
        TapeLeft.text = Maximo.ToString();
    }

    public void UpdateSlider(int Less){
        TapeSlider.value -= Less;
        if(TapeSlider.value <= 0){
            TapeSlider.value = 0;
            return;
        }
        TapeLeft.text = TapeSlider.value.ToString();
    }
}
