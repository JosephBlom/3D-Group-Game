using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas settingsCanvas;

    private void Start()
    {
        pauseCanvas.enabled = false;
        settingsCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.enabled || settingsCanvas.enabled)
            {
                Time.timeScale = 1;
                lockMouse(false);
                pauseCanvas.enabled = false;
                settingsCanvas.enabled = false;
            }
            else
            {
                Time.timeScale = 0;
                lockMouse(true);
                pauseCanvas.enabled = true;
            }   
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        lockMouse(false);
        pauseCanvas.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsCanvas.enabled = true;
        pauseCanvas.enabled = false;
    }

    public void CloseSettings()
    {
        pauseCanvas.enabled = true;
        settingsCanvas.enabled = false;
    }

    void lockMouse(bool enable)
    {
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;
        FindFirstObjectByType<StarterAssets.StarterAssetsInputs>().gameObject.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }
}
