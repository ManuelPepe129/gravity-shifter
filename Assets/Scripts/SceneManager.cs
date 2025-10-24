using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// To manage the active scene
/// </summary>
public class SceneManager : MonoBehaviour
{
    [SerializeField] Collider exitCollider;
    [SerializeField] AudioSource gameOverAudio;
    [SerializeField] AudioSource winAudio;
    [SerializeField] AudioSource portalAudio;

    public GameSession session;
    GameObject exit;   

    private int totalCandies;
    private int collectedCandies = 0;

    private void Awake()
    {
        session = new GameSession();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalCandies = GameObject.FindGameObjectsWithTag("Collectable").Length;
        exit = GameObject.FindGameObjectWithTag("Finish");
    }

    public void OnCandyCollected() 
    {
        collectedCandies++;

        // TODO: update UI to visualize total
        Debug.Log($"Candies collected: {collectedCandies}");

        if (collectedCandies == totalCandies)
        {
            // TODO: UI "congrats: you collected all the candies!"
            Debug.Log("Congrats: you collected all the candies!");
        }
    }

    /// <summary>
    /// Activates portal collider to end game
    /// and open the portal
    /// </summary>
    public void OnLeverActivated()
    {
        portalAudio.Play();
        exitCollider.enabled = true;
        exit.SetActive(false);
    }

    /// <summary>
    /// </summary>
    /// <param name="isDeath">true in case of death, false in case of reset</param>
    public void OnPlayerDeath(bool isDeath)
    {
        StartCoroutine(TriggerSequenceDeath(isDeath));     
    }

    private IEnumerator TriggerSequenceDeath(bool death)
    {
        if (death && !gameOverAudio.isPlaying)
        {
            gameOverAudio.Play();
            yield return new WaitForSeconds(2f);
        }

        session.OnPlayerDeath(death);
    }

    public void OnLevelCompleted()
    {
        StartCoroutine(TriggerSequenceLevelCompleted());
    }

    private IEnumerator TriggerSequenceLevelCompleted()
    {
        if (!winAudio.isPlaying)
        {
            winAudio.Play();
        }

        yield return new WaitForSeconds(4f);
        session.OnLevelCompleted();
    }
}
