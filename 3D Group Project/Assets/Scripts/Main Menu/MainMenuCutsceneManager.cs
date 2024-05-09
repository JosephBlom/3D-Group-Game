using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuCutsceneManager : MonoBehaviour
{
    [Header("Cutscene Objects")]
    [SerializeField] private GameObject ship;

    [Header("Camera Positions")]
    [SerializeField] private GameObject mainCameraPosition;
    [SerializeField] private GameObject cameraPosition01;
    [SerializeField] private GameObject cameraPosition02;
    [SerializeField] private GameObject cameraPosition03;
    [SerializeField] private GameObject cameraPosition04;

    [Header("Ship Positions")]
    [SerializeField] private GameObject mainshipPosition;
    [SerializeField] private GameObject shipPosition01;
    [SerializeField] private GameObject shipPosition02;
    [SerializeField] private GameObject shipPosition03;

    MainMenuScript mainMenu;
    private bool cutsceneActive = false;
    [SerializeField] private float skipTimer = 0;

    private void Start()
    {
        mainMenu = FindFirstObjectByType<MainMenuScript>();
        Camera.main.gameObject.transform.position = mainCameraPosition.transform.position;
        Camera.main.gameObject.transform.rotation = mainCameraPosition.transform.rotation;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X) && cutsceneActive)
        {
            skipTimer += Time.deltaTime;

            if(skipTimer >= 5)
            {
                StopAllCoroutines();
                mainMenu.StartNewGame();
            }
        }
    }

    public void StartCutscene()
    {
        StartCoroutine(IntroCutscene());
    }

    public IEnumerator IntroCutscene()
    {
        GameObject camera = Camera.main.gameObject;

        if(!cutsceneActive)
        {
            cutsceneActive = true;
            mainMenu.mainMenuGameCanvas.enabled = false;
            mainMenu.warningCanvas.enabled = false;

            yield return new WaitForSeconds(1.5f);
            mainMenu.GetComponent<Canvas>().enabled = false;

            mainMenu.cutsceneCanvas.enabled = true;
            mainMenu.titleText.text = "";

            ChangeSetting(camera, cameraPosition01);
            ChangeSetting(ship, mainshipPosition);
            yield return new WaitForSeconds(3);
            mainMenu.titleText.text = "Grubby Inc. presents";
            yield return new WaitForSeconds(1);
            ship.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 250);
            yield return new WaitForSeconds(3);
            mainMenu.titleText.text = "";
            ChangeSetting(camera, cameraPosition02);
            yield return new WaitForSeconds(5);
            ChangeSetting(camera, cameraPosition03);
            yield return new WaitForSeconds(2);
            mainMenu.titleText.text = "Spaceline";
            yield return new WaitForSeconds(9);
            mainMenu.StartNewGame();
        }
    }

    private void ChangeSetting(GameObject objChange, GameObject newObject)
    {
        objChange.transform.position = newObject.transform.position;
        objChange.transform.rotation = newObject.transform.rotation;
    }
}
