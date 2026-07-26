using UnityEngine;
using UnityEngine.SceneManagement;

public class TV : MonoBehaviour
{
    public LayerMask playerLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerLayer.Contains(collision.gameObject.layer))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LifesVariables.deathCount = 0;
        DestroyAllCorpses();
        SceneManager.LoadScene(currentSceneIndex + 1);
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
