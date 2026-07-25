using UnityEngine;

public class Corpo : MonoBehaviour
{
    public PlayerSpawner Spawner;
    void Start()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Respawn");
        Spawner = spawnerObject.GetComponent<PlayerSpawner>();
        if(LifesVariables.playerLifes > 0)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
