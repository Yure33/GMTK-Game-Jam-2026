using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [Header("Audio Sources")]
    public AudioSource deathSound;
    public AudioSource jumpSound;
    public AudioSource closeDoor;
    public AudioSource openDoor;


    private AudioSource[] sourceList;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sourceList = gameObject.GetComponents<AudioSource>();
        List<float> list = new List<float>();
        for (int i = 0; i < sourceList.Length; i++)
        {
            list.Add(sourceList[i].volume * PlayerPrefs.GetFloat("MusicVolume", 1f));
        }
    }

    void Update()
    {
        
    }
}
