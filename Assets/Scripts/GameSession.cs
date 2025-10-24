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
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinMenu");
        Physics.gravity = Vector3.down;
    }

    public void OnPlayerDeath()
    {
        // TODO: UI "You lost"
        Debug.Log("You lost!");

        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseMenu");
        Physics.gravity = Vector3.down;
    }

    // TODO: reset game at key pression
}
