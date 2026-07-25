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

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
