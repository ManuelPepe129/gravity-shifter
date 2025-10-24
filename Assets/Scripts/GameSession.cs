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

    /// <summary>
    /// If reset -> reload current scene
    /// If loose -> load LOOSE scene
    /// </summary>
    /// <param name="loadUI"></param>
    public void OnPlayerDeath(bool loadUI)
    {
        // TODO: UI "You lost"
        Debug.Log("You lost!");

        if (loadUI)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoseMenu");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    // TODO: reset game at key pression
}
