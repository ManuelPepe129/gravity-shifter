using UnityEngine;

/// <summary>
/// To manage the active scene
/// </summary>
public class SceneManager : MonoBehaviour
{
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

    public void OnLeverActivated()
    {
        // TODO: change material to door: from wood to Ire's shader
        // TODO: suono + particellare intorno al portale??
        exit.GetComponent<Collider>().enabled = true;
    }

    public void OnPlayerDeath()
    {
        // TODO: reset gravity!
        session.OnPlayerDeath();
    }
}
