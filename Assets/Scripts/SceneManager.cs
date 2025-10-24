using UnityEngine;

/// <summary>
/// To manage the active scene
/// </summary>
public class SceneManager : MonoBehaviour
{
    [SerializeField] Collider exitCollider;

    public GameSession session;

    private int totalCandies;
    private int collectedCandies = 0;

    GameObject exit;

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
        exitCollider.enabled = true;
        exit.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        session.OnPlayerDeath();
    }

    public void OnLevelCompleted()
    {
        session.OnLevelCompleted();
    }
}
