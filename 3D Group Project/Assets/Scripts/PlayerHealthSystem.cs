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
        if (playerShield > 0)
        {
            int damagetoSubtract = playerShield;
            playerShield -= damage;
            damage -= damagetoSubtract;
            shieldRegen = false;
            if (damage > 0)
            {
                playerHealth -= damage;
                regenActive = false;
            }
        }
        else
        {
            playerHealth -= damage;
            regenActive = false;
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
    private void PlayerHealthRegen()
    {
        if (!isAlive || playerHealth >= maxHealth) { return; }

        StartCoroutine(Regen());
    }
    private void PlayerShieldRegen()
    {
        if (!isAlive || playerShield >= maxShield) { return; }

        StartCoroutine(ShieldRegen());
    }
    private void HealthFix()
    {
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        if (playerShield < 0)
        {
            playerShield = 0;
        }
        if(playerHealth > maxHealth && !extraHealthActive)
        {
            playerHealth = maxHealth;
        }
    }
    private IEnumerator Regen()
    {
        if (!regenActive)
        {
            yield return new WaitForSeconds(playerRegenDelay);
            regenActive = true;
        }

        yield return new WaitForSeconds(playerRegenTick);
        playerHealth++;
        StopAllCoroutines();
    }
    private IEnumerator ShieldRegen()
    {
        if(!shieldRegen)
        {
            yield return new WaitForSeconds(playerShieldRegenDelay);
            shieldRegen = true;
        }

        yield return new WaitForSeconds(playerShieldRegenTick);
        playerShield++;
        StopAllCoroutines();
    }
}
