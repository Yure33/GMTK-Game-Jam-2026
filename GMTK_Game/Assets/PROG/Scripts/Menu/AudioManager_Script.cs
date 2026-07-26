using UnityEngine;
using UnityEngine.UI;

public class AudioManager_Script : MonoBehaviour
{

        // Refer�ncia aos Sliders da Scene
        public Slider musicSlider;
        public Slider sfxSlider;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            // Carrega os valores salvos anteriormente (ou usa 1 como valor padr�o se n�o existir)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

            // Adiciona eventos que ser�o chamados quando o valor do slider for alterado
            // O `delegate` chama os m�todos `SetMusicVolume` e `SetSFXVolume` quando os sliders s�o movidos
            musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
            sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
        }

        // Fun��o que ajusta o volume da m�sica usando o valor atual do slider
        public void SetMusicVolume()
        {
            // Acessa o AudioManager e define o novo volume da m�sica
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            MusicLooper.instance.ChangeMusicVolume();
        }

        // Fun��o que ajusta o volume dos efeitos sonoros usando o valor atual do slider
        public void SetSFXVolume()
        {
            // Acessa o AudioManager e define o novo volume dos efeitos sonoros
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
            SoundFXManager.instance.ChangeSFXVolume();
        }
    }
