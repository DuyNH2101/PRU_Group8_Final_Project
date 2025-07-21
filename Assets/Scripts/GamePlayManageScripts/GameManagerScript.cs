using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] GameObject gameMenuIngame;
    [SerializeField] GameObject soundSettingIngame;
    [SerializeField] GameObject pauseButton;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (gameMenuIngame != null) gameMenuIngame.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            if (gameMenuIngame != null) gameMenuIngame.SetActive(false);
            pauseButton.SetActive(true);
        }
    }
    public void ToggleSoundSetting()
    {
        gameMenuIngame.SetActive(false);
        soundSettingIngame.SetActive(true);
    }
    public void ToggleExitSoundSetting()
    {
        gameMenuIngame.SetActive(true);
        soundSettingIngame.SetActive(false);
    }
    public void ToggleGoToMainMenu()
    {
        GameSessionScript.instance.LoadMainMenu();
    }
}
