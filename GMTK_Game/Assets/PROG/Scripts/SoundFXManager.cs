using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [Header("Audio Sources")]
    [SerializeField] public AudioSource deathSound;
    [SerializeField] public AudioSource jumpSound;
    [SerializeField] public AudioSource openDoor;
    [SerializeField] public AudioSource closeDoor;

    private AudioSource[] sourceList;
    private float[] volumeList;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Pega os volumes default das sources
        sourceList = gameObject.GetComponents<AudioSource>();
        List<float> list = new List<float>();
        for (int i = 0; i < sourceList.Length; i++)
        {
            list.Add(sourceList[i].volume * PlayerPrefs.GetFloat("MusicVolume", 1f));
        }
        volumeList = list.ToArray();
        ChangeSFXVolume();
    }

    public void ChangeSFXVolume()
    {
        for (int i = 0; i < sourceList.Length; i++)
        {
            sourceList[i].volume = volumeList[i] * PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
        // Debug.Log(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }
}
