using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement player;
    public RemainingTape_Script Tape;
    public ControlFPS_Script fpsControl;
    public float spawnDelay = 2f;
    public bool travaSpawns = false;
    public int scenePlayerLifes;

    private void Awake()
    {
        LifesVariables.playerLifes = scenePlayerLifes - LifesVariables.deathCount;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = Instantiate(Player, transform.position, Quaternion.identity).GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Respawn").Length > 1) Destroy(GameObject.FindGameObjectWithTag("Respawn"));
        if (player == null && !travaSpawns)
        {
            travaSpawns = true;
            StartCoroutine(SpawnPlayer());
            Debug.Log("Spawnou");
        }
    }

    private IEnumerator SpawnPlayer()
    {
        if (LifesVariables.playerLifes > 0)
        {
            yield return new WaitForSeconds(spawnDelay);
            Tape.TapeSlider.value = fpsControl.StartTape;
            travaSpawns = false;
            LifesVariables.deathCount++;
            ReloadCurrentScene();
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
            LifesVariables.playerLifes = scenePlayerLifes;
            DestroyAllCorpses();
            ReloadCurrentScene();
            LifesVariables.deathCount = 0;
        }
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void DestroyAllCorpses()
    {
        // Loop through and delete each object from the scene
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Corpo"))
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
    }
}
