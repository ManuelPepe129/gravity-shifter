using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        sceneManager.OnLevelCompleted();
    }
}
