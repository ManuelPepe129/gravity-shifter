using UnityEngine;

/// <summary>
/// Temporary state of current session
/// </summary>
public class GameSession
{
    public void OnLevelCompleted()
    {
        // TODO: UI "You won"
        Debug.Log("Level completed!");
    }

    public void OnPlayerDeath()
    {
        // TODO: UI "You lost"
        Debug.Log("You lost!");

        //UnityEngine.SceneManagement.SceneManager.LoadScene(
        //    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // TODO: reset game at key pression
}
