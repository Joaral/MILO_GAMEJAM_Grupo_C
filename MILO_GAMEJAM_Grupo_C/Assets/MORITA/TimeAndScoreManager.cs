using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAndScoreManager : MonoBehaviour
{
    public static TimeAndScoreManager Instance;

    [Header("Timer Settings")]
    public float startTime = 60f; // starting time in seconds
    private float currentTime;
    public bool isRunning = true;

    [Header("Score")]
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endText;
    public GameObject gameOverPanel;
    public GameObject gamePanel;

    [Header("Backend")]
    public minigame minigame;

    void Start()
    {
        currentTime = startTime;
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        UpdateUI();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimeUp();
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("N0");
        }

        if (scoreText != null)
        {
            scoreText.text = "SCORE       " + score.ToString();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score <= 0) score = 0;

        UpdateUI();
    }

    void OnTimeUp()
    {
        Debug.Log("Time's up!");
        minigame.isPlaying = false;
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);

        endText.text = score.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // Stop play mode in editor
#else
        Application.Quit();                                // Close the game in a build
#endif
    }

}
