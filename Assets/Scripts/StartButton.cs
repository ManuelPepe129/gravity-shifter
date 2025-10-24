using UnityEngine;
using UnityEngine.SceneManagement; // serve per cambiare scena

public class StartButton : MonoBehaviour
{
    public string gameSceneName; // nome della scena di gioco

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}

