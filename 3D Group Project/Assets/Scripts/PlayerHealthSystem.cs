using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private PlayerUI playerUI;
    private int maxHealth;
    private int maxShield;

    private bool isAlive = true;
    private bool mountHorsie = false;
    private bool shieldRegen = false;
    private bool regenActive = false;
    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
        maxHealth = playerHealth;
        maxShield = playerShield;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerTakeDamage(5);
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            PlayerHeal(5);
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            SetMaxHealth(50);
            SetMaxShield(5);
        }
        else if(Input.GetKeyDown(KeyCode.E) && mountHorsie)
        {
            gameObject.transform.parent.GetComponentInChildren<HorseMountScript>().UnmountHorse();
            mountHorsie = false;
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
    public void SetMaxHealth(int newMaxHealth)
    {
        if(!isAlive || playerUI == null) { return; }
        maxHealth = newMaxHealth;
        playerUI.healthSlider.maxValue = newMaxHealth;
        playerUI.tinyhealthSlider.maxValue = newMaxHealth;
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
    }
    public void SetMaxShield(int newMaxShield)
    {
        if (!isAlive || playerUI == null) { return; }
        maxShield = newMaxShield;
        playerUI.shieldSlider.maxValue = newMaxShield;
        playerUI.tinyshieldSlider.maxValue = newMaxShield;
        if (playerShield > maxShield)
        {
            playerShield = maxShield;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(collision.gameObject.GetComponent<ProjectileBehavior>() != null && !collision.gameObject.GetComponent<ProjectileBehavior>().friendly)
            {
                PlayerTakeDamage(collision.gameObject.GetComponent<ProjectileBehavior>().damage);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if(other.gameObject.GetComponent<ExplosionBehavior>() != null)
            {
                if (!other.gameObject.GetComponent<ExplosionBehavior>().friendly)
                {
                    PlayerTakeDamage(other.gameObject.GetComponent<ExplosionBehavior>().damage);
                }
            }
            else if (other.gameObject.GetComponent<MeleeBehavior>() != null)
            {
                if(!other.gameObject.GetComponent<MeleeBehavior>().friendly)
                {
                    PlayerTakeDamage(other.gameObject.GetComponent<MeleeBehavior>().damage);
                }
            }
        }
        if(other.gameObject.GetComponent<HorseCollision>() != null)
        {
            other.gameObject.transform.parent.GetComponent<HorseMountScript>().player = gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<HorseCollision>() != null)
        {
            other.gameObject.transform.parent.GetComponent<HorseMountScript>().player = gameObject;
            if (Input.GetKeyDown(KeyCode.E) && !mountHorsie)
            {
                other.gameObject.transform.parent.GetComponent<HorseMountScript>().MountHorse();
                mountHorsie = true;
            }
        }
    }

}
