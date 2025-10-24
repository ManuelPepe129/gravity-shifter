using System.Collections;
using System.Net;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] ParticleSystem collectedEffect;

    SceneManager sceneManager;
    AudioSource audioSource;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sceneManager != null && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TriggerSequence());
        }
    }

    private IEnumerator TriggerSequence()
    {
        collectedEffect.Play();
        audioSource.Play();
        sceneManager.OnCandyCollected();

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
