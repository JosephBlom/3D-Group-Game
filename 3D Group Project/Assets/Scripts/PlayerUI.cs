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
        healthSlider.maxValue = healthSystem.playerHealth;
        shieldSlider.maxValue = healthSystem.playerShield;
    }
    private void Update()
    {
        healthSlider.value = healthSystem.playerHealth;
        shieldSlider.value = healthSystem.playerShield;
        healthText.text = healthSystem.playerHealth.ToString();
        shieldHPText.text = healthSystem.playerShield.ToString();
    }
    private void HPTextManager()
    {
        
    }


}
