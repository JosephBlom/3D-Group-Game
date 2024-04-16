using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldHPText;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider shieldSlider;
    PlayerHealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();

        healthSlider = GetComponentInChildren<Slider>();
        shieldSlider = GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        healthText.text = healthSystem.playerHealth.ToString();
        shieldHPText.text = healthSystem.playerShield.ToString();
    }

}
