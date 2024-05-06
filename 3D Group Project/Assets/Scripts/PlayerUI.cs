using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldHPText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI ammoText;

    // main hp/shield UI
    [SerializeField] public Slider healthSlider;
    [SerializeField] public Slider shieldSlider;

    // tiny crosshair bar UI
    [SerializeField] Slider ammoSlider;
    [SerializeField] public Slider tinyhealthSlider;
    [SerializeField] public Slider tinyshieldSlider;

    Player player;
    PlayerHealthSystem healthSystem;
    GunSystem gunsystem;

    private void Start()
    {
        healthSystem = GetComponentInChildren<PlayerHealthSystem>();
        player = GetComponent<Player>();
        healthSlider.maxValue = healthSystem.playerHealth;
        shieldSlider.maxValue = healthSystem.playerShield;
        tinyhealthSlider.maxValue = healthSystem.playerHealth;
        tinyshieldSlider.maxValue = healthSystem.playerShield;
    }
    private void Update()
    {
        float uiTimer = Mathf.Round(player.timer * 100.0f) * 0.01f;

        EnableAmmoUI();
        if (gunsystem != null)
        {
            ChangeMaxAmmo();
        }
        else
        {
            ammoSlider.GetComponentInChildren<Image>().enabled = false;
        }
        healthSlider.value = healthSystem.playerHealth;
        shieldSlider.value = healthSystem.playerShield;
        tinyhealthSlider.value = healthSystem.playerHealth;
        tinyshieldSlider.value = healthSystem.playerShield;
        healthText.text = healthSystem.playerHealth.ToString();
        shieldHPText.text = healthSystem.playerShield.ToString();
        timerText.text = uiTimer.ToString("F2");
    }
    // call these 2 when picking up/equipping gun
    public void ChangeMaxAmmo()
    {
        gunsystem = FindFirstObjectByType<GunSystem>();
        ammoSlider.maxValue = gunsystem.maxAmmoSize;
        ammoSlider.value = gunsystem.ammoCount;
        ammoText.text = gunsystem.ammoCount.ToString() + " / " + GetComponent<PlayerAmmoManager>().FindAmmoType(gunsystem.ammoType);
    }
    public void EnableAmmoUI()
    {
        gunsystem = GetComponentInChildren<GunSystem>();
        if (gunsystem != null)
        {
            ammoSlider.GetComponentInChildren<Image>().enabled = true;
        }
    }

}
