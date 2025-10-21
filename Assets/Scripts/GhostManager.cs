using UnityEngine;

public class GhostManager : MonoBehaviour
{
    SceneManager sceneManager;
    [SerializeField] GameObject ghost;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
    }

    /// <summary>
    /// When the player collides with the trigger
    /// collider of the grave, the ghost is spawned
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (sceneManager != null && other.gameObject.CompareTag("Player"))
        {
            ghost.SetActive(true);
        }
    }
}
