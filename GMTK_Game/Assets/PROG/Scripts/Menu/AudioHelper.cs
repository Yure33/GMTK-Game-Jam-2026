using UnityEngine;
using System.Collections;
public class AudioHelper : MonoBehaviour
{
    // Da o FadeOut parando a musica
    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * (Time.deltaTime / fadeTime);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    // Da o FadeOut sem parar a musica
    public static IEnumerator FadeOutVolume(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * (Time.deltaTime / fadeTime);
            yield return null;
        }
    }
    // Da o FadeIn com o play
    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float maxVolume = audioSource.volume;
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += maxVolume * (Time.deltaTime / fadeTime);
            yield return null;
        }
    }
    // Da o FadeIn sem o play
    public static IEnumerator FadeInVolume(AudioSource audioSource, float fadeTime, float maxVolume)
    {
        audioSource.volume = 0f;
        float audioMultiplier = 0f;
        while (audioMultiplier < maxVolume & maxVolume > 1f)
        {
            audioMultiplier += maxVolume * (Time.deltaTime / fadeTime);
            audioSource.volume = audioMultiplier * PlayerPrefs.GetFloat("MusicVolume", 1f);
            yield return null;
        }
        audioSource.volume = audioMultiplier * PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

}
