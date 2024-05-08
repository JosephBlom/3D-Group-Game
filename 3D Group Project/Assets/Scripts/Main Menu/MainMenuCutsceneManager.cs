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

    private void Start()
    {
        Camera.main.gameObject.transform.position = mainCameraPosition.transform.position;
        Camera.main.gameObject.transform.rotation = mainCameraPosition.transform.rotation;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(IntroCutscene());
        }
    }

    public IEnumerator IntroCutscene()
    {
        GameObject camera = Camera.main.gameObject;

        ChangeSetting(camera, cameraPosition01);
        ChangeSetting(ship, mainshipPosition);
        yield return new WaitForSeconds(3);
        ship.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 250);
        Debug.Log("Grubby Inc. presents");
        yield return new WaitForSeconds(3);
        ChangeSetting(camera, cameraPosition02);
        yield return new WaitForSeconds(5);
        ChangeSetting(camera, cameraPosition03);
        Debug.Log("Spaceline");
    }

    private void ChangeSetting(GameObject objChange, GameObject newObject)
    {
        objChange.transform.position = newObject.transform.position;
        objChange.transform.rotation = newObject.transform.rotation;
    }
}
