using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldHPText;

    // main hp/shield UI
    [SerializeField] public Slider healthSlider;
    [SerializeField] public Slider shieldSlider;

    // tiny crosshair bar UI
    [SerializeField] Slider ammoSlider;
    [SerializeField] public Slider tinyhealthSlider;
    [SerializeField] public Slider tinyshieldSlider;

    PlayerHealthSystem healthSystem;
    GunSystem gunsystem;

    private void Awake()
    {
        EnableAmmoUI();
        if(gunsystem != null)
        {
            ChangeMaxAmmo();
        }
        else
        {
            ammoSlider.GetComponentInChildren<Image>().enabled = false; 
        }
        healthSystem = GetComponentInChildren<PlayerHealthSystem>();
        healthSlider.maxValue = healthSystem.playerHealth;
        shieldSlider.maxValue = healthSystem.playerShield;
        tinyhealthSlider.maxValue = healthSystem.playerHealth;
        tinyshieldSlider.maxValue = healthSystem.playerShield;
    }
    private void Update()
    {
        if(gunsystem != null)
        {
            ammoSlider.value = gunsystem.ammoCount;
        }
        healthSlider.value = healthSystem.playerHealth;
        shieldSlider.value = healthSystem.playerShield;
        tinyhealthSlider.value = healthSystem.playerHealth;
        tinyshieldSlider.value = healthSystem.playerShield;
        healthText.text = healthSystem.playerHealth.ToString();
        shieldHPText.text = healthSystem.playerShield.ToString();
    }
    // call these 2 when picking up/equipping gun
    public void ChangeMaxAmmo()
    {
        gunsystem = FindFirstObjectByType<GunSystem>();
        ammoSlider.maxValue = gunsystem.maxAmmoSize;
    }
    public void EnableAmmoUI()
    {
        gunsystem = FindFirstObjectByType<GunSystem>();
        if (gunsystem != null)
        {
            ammoSlider.GetComponentInChildren<Image>().enabled = true;
        }
    }

}
