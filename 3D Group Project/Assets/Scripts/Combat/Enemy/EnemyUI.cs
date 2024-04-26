using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    EnemyHealthSystem healthSystem;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider shieldSlider;

    private void Awake()
    {
        healthSystem = GetComponent<EnemyHealthSystem>();
        hpSlider.maxValue = healthSystem.enemyHealth;
        if(healthSystem.enableShield)
        {
            shieldSlider.maxValue = healthSystem.enemyShield;
        }
        else
        {
            shieldSlider.gameObject.SetActive(false);
            shieldText.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        enemyName.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = healthSystem.enemyHealth;
        shieldSlider.value = healthSystem.enemyShield;
        healthText.text = healthSystem.enemyHealth.ToString();
        shieldText.text = healthSystem.enemyShield.ToString();
    }
}
