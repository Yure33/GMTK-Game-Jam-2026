using UnityEngine;

public class SpawnPorta : MonoBehaviour
{
    public GameObject porta;
    public bool initState;
    void Start()
    {
        porta.SetActive(initState);
    }
}
