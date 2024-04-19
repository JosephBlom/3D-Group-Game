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
    [SerializeField] Slider ammoSlider;
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
        healthSystem = GetComponent<PlayerHealthSystem>();
        healthSlider.maxValue = healthSystem.playerHealth;
        shieldSlider.maxValue = healthSystem.playerShield;
    }
    private void Update()
    {
        if(gunsystem != null)
        {
            ammoSlider.value = gunsystem.ammoCount;
        }
        healthSlider.value = healthSystem.playerHealth;
        shieldSlider.value = healthSystem.playerShield;
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
