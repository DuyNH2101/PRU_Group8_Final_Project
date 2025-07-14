using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu"); 
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Reset()
    {
        
    }

}
