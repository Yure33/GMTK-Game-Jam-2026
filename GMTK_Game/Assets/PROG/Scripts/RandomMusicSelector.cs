using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    [Header("Playlist Settings")]
    [Tooltip("Add all your background music tracks here.")]
    [SerializeField] private List<AudioClip> playlist = new List<AudioClip>();

    [Tooltip("Enable if you want the playlist to reshuffle endlessly.")]
    [SerializeField] private bool loopPlaylist = true;

    private AudioSource audioSource;
    private List<AudioClip> playQueue = new List<AudioClip>();
    private AudioClip lastPlayedClip;

    public static MusicLooper instance;
    [SerializeField] private AudioSource music;
    private float musicVolume;

    [SerializeField] private float fadeLength = 12f;

    private AudioSource[] sourceList;
    private float[] volumeList;
    private bool isAppPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Pega os volumes default das sources
        sourceList = gameObject.GetComponents<AudioSource>();
        List<float> list = new List<float>();
        for (int i = 0; i < sourceList.Length; i++)
        {
            list.Add(sourceList[i].volume);
        }
        volumeList = list.ToArray();

        
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // The script handles track progression, loop must be false
        audioSource.playOnAwake = false;

        if (playlist == null || playlist.Count == 0)
        {
            Debug.LogError("RandomMusicPlayer: Playlist is empty! Please assign AudioClips in the Inspector.", this);
            return;
        }

        // Initialize and begin the music loop
        GenerateNewQueue();
        StartCoroutine(MusicPlaylistRoutine());
        StartCoroutine(SoundTrackLooper());
        ChangeMusicVolume();
    }
    private IEnumerator SoundTrackLooper()
    {
        musicVolume = volumeList[0];
        while (true)
        {
            StartCoroutine(AudioHelper.FadeInVolume(music, fadeLength, musicVolume));
            Debug.Log("Musica Fade In");
            yield return new WaitForSeconds(Random.Range(50f, 70f));
            StartCoroutine(AudioHelper.FadeOutVolume(music, fadeLength));
            Debug.Log("Musica Fade Out");
            yield return new WaitForSeconds(Random.Range(50f, 80f));
            StartCoroutine(AudioHelper.FadeInVolume(music, fadeLength, musicVolume));
            Debug.Log("Musica Fade In");
        }
    }

    public void ChangeMusicVolume()
    {
        for (int i = 0; i < sourceList.Length; i++)
        {
            sourceList[i].volume = volumeList[i] * PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
    }

    private System.Collections.IEnumerator MusicPlaylistRoutine()
    {
        while (loopPlaylist && playQueue.Count > 0)
        {
            // Pull the next track from the randomized queue
            AudioClip currentClip = playQueue[0];
            playQueue.RemoveAt(0);

            // Assign and play the audio track
            audioSource.clip = currentClip;
            lastPlayedClip = currentClip;
            audioSource.Play();

            Debug.Log($"Playing track: {currentClip.name}");

            // Wait until the current track finishes playing entirely
            yield return new WaitWhile(() => audioSource.isPlaying || isAppPaused);

            // If the queue runs out, rebuild a fresh shuffled list
            if (playQueue.Count == 0 && loopPlaylist)
            {
                GenerateNewQueue();
            }
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        isAppPaused = pauseStatus;
    }

    private void GenerateNewQueue()
    {
        // Copy original playlist into the temporary play queue
        playQueue = new List<AudioClip>(playlist);

        // Fisher-Yates Shuffle Algorithm for unbiased randomization
        for (int i = playQueue.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = playQueue[i];
            playQueue[i] = playQueue[randomIndex];
            playQueue[randomIndex] = temp;
            if (playQueue.Count > i && playQueue[0] == lastPlayedClip)
            {
                // Swap the first track with the last track in the queue
                temp = playQueue[0];
                playQueue[0] = playQueue[playQueue.Count - 1];
                playQueue[playQueue.Count - 1] = temp;
            }
        }

        // Prevention check: Avoid playing the exact same track twice back-to-back on reshuffle
        //if (playQueue.Count > i && playQueue[0] == lastPlayedClip)
        //{
        //    // Swap the first track with the last track in the queue
        //    AudioClip temp = playQueue[0];
        //    playQueue[0] = playQueue[playQueue.Count - 1];
        //    playQueue[playQueue.Count - 1] = temp;
        //}
    }

    // Public method if you want to force skip to a new random track via UI button or event
    public void SkipTrack()
    {
        audioSource.Stop(); // The Coroutine will naturally wake up and advance
    }
}
