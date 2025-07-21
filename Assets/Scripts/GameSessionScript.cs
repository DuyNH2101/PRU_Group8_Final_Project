using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameSessionScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    public static GameSessionScript instance;
    public int stageScore;

    private void Start()
    {
        currentScoreText.text = "Current Score: " + stageScore;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        
    }
    public void AddScore(int score)
    {
        stageScore += score;
        currentScoreText.text = "Current score: " + stageScore.ToString();
    }
    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void Lose()
    {
        SceneManager.LoadScene("LoseScene");
    }
    public void LoadEasyLevel()
    {
        SceneManager.LoadScene("EasyLevel"); 
        stageScore = 0;
    }
    public void LoadMediumLevel()
    {
        SceneManager.LoadScene("MediumLevel");
        stageScore = 0;
    }
    public void LoadHardLevel()
    {
        SceneManager.LoadScene("HardLevel");
        stageScore = 0;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
