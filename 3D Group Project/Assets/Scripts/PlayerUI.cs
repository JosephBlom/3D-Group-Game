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

        healthSlider = GetComponent<Slider>();
        shieldSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        healthText.text = healthSystem.playerHealth.ToString();
        healthSlider.value = healthSystem.playerHealth;
        shieldHPText.text = healthSystem.playerShield.ToString();
        shieldSlider.value = healthSystem.playerShield;
    }

}
