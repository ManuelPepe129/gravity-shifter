using UnityEngine;

/// <summary>
/// To manage the active scene
/// </summary>
public class SceneManager : MonoBehaviour
{
    [SerializeField] Collider exitCollider;
    [SerializeField] AudioSource gameOverAudio;
    [SerializeField] AudioSource winAudio;
    [SerializeField] AudioSource portalAudio;
    [SerializeField] GameObject inGameUI;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    public GameSession session;
    GameObject exit;

    private int totalCandies;
    private int collectedCandies = 0;

    private CandiesText[] _candiesTexts;

    private void Awake()
    {
        session = new GameSession();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        _candiesTexts = FindObjectsByType<CandiesText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        totalCandies = GameObject.FindGameObjectsWithTag("Collectable").Length;
        foreach (var candyText in _candiesTexts)
        {
            candyText.SetTotalCandies(totalCandies);
        }
        
        exit = GameObject.FindGameObjectWithTag("Finish");
    }

    public void OnCandyCollected()
    {
        collectedCandies++;

        foreach (var candyText in _candiesTexts)
        {
            candyText.UpdateCandiesCollected(collectedCandies);
        }

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
        if (isDeath && !gameOverAudio.isPlaying)
        {
            inGameUI.SetActive(false);
            gameOverAudio.Play();
            loseMenu.SetActive(true);
            Time.timeScale = 0; // Freeze the game
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        // session.OnPlayerDeath(death);  
    }

    public void OnLevelCompleted()
    {
        inGameUI.SetActive(false); 

        if (!winAudio.isPlaying)
        {
            winAudio.Play();
        }

        winMenu.SetActive(true);
        Time.timeScale = 0; // Freeze the game
        // session.OnLevelCompleted();
    }
}