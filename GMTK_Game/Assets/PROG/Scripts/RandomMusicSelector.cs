using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomMusicPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private List<AudioClip> playlist = new List<AudioClip>();

    private AudioSource audioSource;
    private List<AudioClip> playQueue = new List<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(audioSource);
        // Automatically fetch or configure the required AudioSource component
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // The script handles track progression, loop must be false
        audioSource.playOnAwake = false;

        if (playlist.Count == 0)
        {
            Debug.LogWarning("RandomMusicPlayer: The playlist is empty! Add AudioClips in the Inspector.");
            return;
        }

        PlayNextTrack();
    }

    void Update()
    {
        // Check if the current song has finished playing completely
        if (!audioSource.isPlaying && playlist.Count > 0)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        // Regenerate and shuffle the queue if all songs have been played
        if (playQueue.Count == 0)
        {
            InitializeQueue();
        }

        // Pull the first song from our shuffled deck
        AudioClip nextTrack = playQueue[0];
        playQueue.RemoveAt(0);

        // Assign and play the track
        audioSource.clip = nextTrack;
        audioSource.Play();
        Debug.Log($"Now Playing: {nextTrack.name}");
    }

    private void InitializeQueue()
    {
        // Clone the original playlist into the active queue
        playQueue = new List<AudioClip>(playlist);

        // Fisher-Yates Shuffle algorithm to thoroughly randomize the order
        for (int i = playQueue.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = playQueue[i];
            playQueue[i] = playQueue[randomIndex];
            playQueue[randomIndex] = temp;
        }
    }
}
