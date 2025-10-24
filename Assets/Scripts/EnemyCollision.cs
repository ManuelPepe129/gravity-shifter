using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneManager.OnPlayerDeath(true);
        }
    }
}
