using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas mainMenuGameCanvas;
    [SerializeField] private Canvas warningCanvas;

    SaveManager saveManager;

    private void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
        GetComponent<Canvas>().enabled = true;
        settingsCanvas.enabled = false;
        mainMenuGameCanvas.enabled = false;
        warningCanvas.enabled = false;
    }

    public void OpenGameOptions()
    {
        if (mainMenuGameCanvas.enabled)
        {
            mainMenuGameCanvas.enabled = false;
        }
        else
        {
            mainMenuGameCanvas.enabled = true;
        }
    }
    public void StartNewGame()
    {
        // check if there is save data
        if(!warningCanvas.enabled)
        {
            PromptWarning();
            return;
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
        saveManager.player = FindFirstObjectByType<Player>();
        saveManager.SavePlayer();
    }
    public void LoadGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
        saveManager.player = FindFirstObjectByType<Player>();
        saveManager.LoadPlayer();
    }
    public void OpenSettings()
    {
        if(settingsCanvas.enabled)
        {
            GetComponent<Canvas>().enabled = true;
            settingsCanvas.enabled = false;
        }
        else
        {
            GetComponent<Canvas>().enabled = false;
            settingsCanvas.enabled = true;
        }
        mainMenuGameCanvas.enabled = false;
    }
    public void ClosePrompt()
    {
        warningCanvas.enabled = false;
    }
    private void PromptWarning()
    {
        warningCanvas.enabled = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
