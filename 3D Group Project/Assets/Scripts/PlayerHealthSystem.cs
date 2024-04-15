using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("Player Health Settings")]
    [Min(1), SerializeField] public int playerHealth = 100;
    [SerializeField] private float playerRegenTick = 0.05f;
    [SerializeField] private int playerRegenDelay = 5;

    [Header("Player Shield Settings")]
    [Min(1), SerializeField] public int playerShield = 100;
    [SerializeField] private float playerShieldRegenTick = 0.05f;
    [SerializeField] private int playerShieldRegenDelay = 5;

    [Header("Misc Settings")]
    [SerializeField] public bool extraHealthActive = false;

    private int maxHealth;
    private int maxShield;
    private float shieldTimer;
    private float shieldTickTimer;
    private float regenTimer;
    private float healthTimer;

    private bool isAlive = true;
    private bool shieldRegen = false;
    private bool regenActive = false;
    private void Awake()
    {
        maxHealth = playerHealth;
        maxShield = playerShield;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerTakeDamage(5);
        }

        PlayerHealthRegen();
        PlayerShieldRegen();
        HealthFix();
    }
    // external functionality
    public void PlayerTakeDamage(int damage)
    {
        if (!isAlive) { return; }
        if(playerShield > 0)
        {
            int damagetoSubtract = playerShield;
            playerShield -= damage;
            damage -= damagetoSubtract;
            shieldTimer = 0;
            if(damagetoSubtract > 0)
            {
                playerHealth -= damage;
                regenTimer = 0;
            }
        }
        else
        {
            playerHealth -= damage;
            regenTimer = 0;
        }
    }
    public void PlayerHeal(int health)
    {
        if (!isAlive || playerHealth == maxHealth) { return; }
        playerHealth += health;
        if (playerHealth > maxHealth && !extraHealthActive)
        {
            playerHealth = maxHealth;
        }
    }

    // natural regen
    public void PlayerHealthRegen()
    {
        if (!isAlive || playerHealth >= maxHealth) {return;}

        healthTimer += Time.deltaTime;
        regenTimer += Time.deltaTime;
        if (regenTimer < playerRegenDelay)
        {
            regenActive = false;
        }
        else
        {
            regenActive = true;
        }
        
        if(regenActive)
        {
            bool regenTick = healthTimer >= playerRegenTick;
            if (regenTick)
            {
                playerHealth++;
                Debug.Log(playerHealth);
                healthTimer = 0;
            }
        }
    }
    public void PlayerShieldRegen()
    {
        if (!isAlive || playerShield >= maxShield) { return; }

        shieldTickTimer += Time.deltaTime;
        shieldTimer += Time.deltaTime;
        if (shieldTimer < playerRegenDelay)
        {
            shieldRegen = false;
        }
        else
        {
            shieldRegen = true;
        }

        if (shieldRegen)
        {
            bool shieldTick = shieldTickTimer >= playerShieldRegenTick;
            if (shieldTick)
            {
                playerShield++;
                shieldTickTimer = 0;
            }
        }
    }
    public void HealthFix()
    {
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        if (playerShield < 0)
        {
            playerShield = 0;
        }
    }
}
