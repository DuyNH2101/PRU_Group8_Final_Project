using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenChoiceLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject GameMenu;
    public GameObject GameChoiceLevel;
    public GameObject GameSetting;
    public GameObject GameInstruction;
    void Start()
    {
        GameMenu.SetActive(true);
    }
    public void clickLevel()
    {
        GameMenu.SetActive(false);
        GameChoiceLevel.SetActive(true);
    }

    public void exitChoiceMenu()
    {
        GameMenu.SetActive(true);
        GameChoiceLevel.SetActive(false);
    }

    public void exitSettingMenu()
    {
        GameMenu.SetActive(true);
        GameSetting.SetActive(false);
    }
    public void exitInstructionMenu()
    {
        GameMenu.SetActive(true);
        GameChoiceLevel.SetActive(false);
        GameInstruction.SetActive(false);
        GameSetting.SetActive(false);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("");
    }    // Update is called once per frame

    // ghi tên của scene Level 1 vào đây nhé
    public void loadLevel1()
    {
        SceneManager.LoadScene("Easy Level");
    }
    // ghi tên của scene Level 2 vào đây nhé
    public void loadLevel2()
    {
        SceneManager.LoadScene("");
    }
    public void QuitGame()
    {
        Debug.Log("Thoát game...");
        Application.Quit();
    }
    public void OpenSetting()
    {
        GameSetting.SetActive(true);
        GameMenu.SetActive(false);
        GameChoiceLevel.SetActive(false);
        GameInstruction.SetActive(false);
    }
    public void OpenInstruction()
    {
        GameSetting.SetActive(false);
        GameInstruction.SetActive(true);
        GameMenu.SetActive(false);
        GameChoiceLevel.SetActive(false);
    }
}
