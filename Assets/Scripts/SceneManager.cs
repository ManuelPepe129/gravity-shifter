using System;
using UnityEngine;

/**
 * 
tenere traccia se la leva è stata utilizzata o meno

  - rendere il livello “terminabile”

*/

/// <summary>
/// To manage the active scene
/// </summary>
public class SceneManager : MonoBehaviour
{
    public GameSession session;

    private int totalCandies;
    private int collectedCandies = 0;

    GameObject exit;

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

    public void OnPlayerDeath()
    {
        session.OnPlayerDeath();
    }
}
