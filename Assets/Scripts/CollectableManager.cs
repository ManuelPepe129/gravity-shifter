using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sceneManager != null && other.gameObject.CompareTag("Player"))
        {
            sceneManager.OnCandyCollected();
            Destroy(gameObject);
        }
    }
}
