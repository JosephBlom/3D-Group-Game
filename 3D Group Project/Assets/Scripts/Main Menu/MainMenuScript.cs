using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Cutscene UI")]
    [SerializeField] public TextMeshProUGUI titleText;
    [SerializeField] public Canvas cutsceneCanvas;
    [SerializeField] public TextMeshProUGUI fastTimeTxt;

    [Header("Canvas Objects")]
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] public Canvas mainMenuGameCanvas;
    [SerializeField] public Canvas warningCanvas;

    [Header("misc")]
    [SerializeField] private CanvasGroup fadegroup;
    public Player player;
    public bool fadeIn = false;
    public bool fadeOut = false;

    SaveManager saveManager;
    MainMenuCutsceneManager cutsceneManager;

    private void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
        cutsceneManager = FindFirstObjectByType<MainMenuCutsceneManager>();
        GetComponent<Canvas>().enabled = true;
        cutsceneCanvas.enabled = false;
        settingsCanvas.enabled = false;
        mainMenuGameCanvas.enabled = false;
        warningCanvas.enabled = false;

        fastTimeTxt.text = "Fastest Time: " + player.fastestTime;
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
        SaveSystem.StartNewGame();
    }
    public void LoadGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
        //saveManager.player = FindFirstObjectByType<Player>();
        //saveManager.LoadPlayer();
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
            mainMenuGameCanvas.enabled = false;
            warningCanvas.enabled = false;
            settingsCanvas.enabled = true;
        }
        mainMenuGameCanvas.enabled = false;
        warningCanvas.enabled = false;
    }
    public void PromptWarning()
    {
        // check if there is save data
        if (!warningCanvas.enabled)
        {
            warningCanvas.enabled = true;
            return;
        }

        warningCanvas.enabled = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadegroup.alpha = 0;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    public void Update()
    {
        if(fadeIn)
        {
            if (fadegroup.alpha < 1)
            {
                fadegroup.alpha += Time.deltaTime;
                if (fadegroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (fadegroup.alpha >= 0)
            {
                fadegroup.alpha -= Time.deltaTime;
                if (fadegroup.alpha >= 1)
                {
                    fadeOut = false;
                }
            }
        }
    }

}
