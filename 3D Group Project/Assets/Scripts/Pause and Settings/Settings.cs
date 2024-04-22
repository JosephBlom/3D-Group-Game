using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public float lookSensitivity;
    public TextMeshProUGUI valueText;
    public bool timer;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerObject");
        valueText.text = player.GetComponent<StarterAssets.FirstPersonController>().RotationSpeed.ToString();
    }

    public void SetSensitivity(float sens)
    {
        lookSensitivity = sens;
        valueText.text = sens.ToString();
        player.GetComponent<StarterAssets.FirstPersonController>().RotationSpeed = lookSensitivity;
    }

    public void TimerOnOff()
    {

    }
}
