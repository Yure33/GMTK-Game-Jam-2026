using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement player;

    public float spawnDelay = 2f;
    public bool travaSpawns = false;
    void Start()
    {
        player = Instantiate(Player, transform.position, Quaternion.identity).GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null && !travaSpawns)
        {
            travaSpawns = true;
            StartCoroutine(SpawnPlayer());
            Debug.Log("Spawnou");
        }
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay); 
        player = Instantiate(Player, transform.position, Quaternion.identity).GetComponent<PlayerMovement>();
        travaSpawns= false;
    }
}
