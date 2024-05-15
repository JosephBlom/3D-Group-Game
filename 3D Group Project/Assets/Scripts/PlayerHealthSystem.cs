using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("Player Health Settings")]
    [Min(1), SerializeField] public float playerHealth = 100;
    [SerializeField] private float playerRegenTick = 0.05f;
    [SerializeField] private int playerRegenDelay = 5;

    [Header("Player Shield Settings")]
    [Min(1), SerializeField] public float playerShield = 100;
    [SerializeField] private float playerShieldRegenTick = 0.05f;
    [SerializeField] private int playerShieldRegenDelay = 5;

    [Header("Death Settings")]
    [SerializeField] private Canvas deathCanvas;

    [Header("Misc Settings")]
    [SerializeField] public bool extraHealthActive = false;

    private PlayerUI playerUI;
    private float maxHealth;
    private float maxShield;

    public float playerMoveSpeed;
    public float playerSprintSpeed;
    public float playerJumpSpeed;
    public float playerHeight;
    public float playerOffset;

    public bool isAlive = true;
    private bool shieldRegen = false;
    private bool regenActive = false;
    private void Awake()
    {
        playerMoveSpeed = transform.parent.GetComponent<FirstPersonController>().MoveSpeed;
        playerSprintSpeed = transform.parent.GetComponent<FirstPersonController>().SprintSpeed;
        playerJumpSpeed = 1.2f;
        playerHeight = transform.parent.GetComponent<CharacterController>().height;
        playerOffset = transform.parent.GetComponent<FirstPersonController>().GroundedOffset;

        transform.parent.GetComponent<FirstPersonController>().MoveSpeed = playerMoveSpeed;
        transform.parent.GetComponent<FirstPersonController>().SprintSpeed = playerSprintSpeed;
        transform.parent.GetComponent<FirstPersonController>().JumpHeight = playerJumpSpeed;
        transform.parent.GetComponent<CharacterController>().height = playerHeight;
        transform.parent.GetComponent<FirstPersonController>().GroundedOffset = playerOffset;

        isAlive = true;
        Cursor.lockState = CursorLockMode.Locked;
        deathCanvas.enabled = false;
        playerUI = GetComponent<PlayerUI>();
        maxHealth = playerHealth;
        maxShield = playerShield;
    }
    private void Update()
    {
        PlayerHealthRegen();
        PlayerShieldRegen();
        HealthFix();
    }
    // external functionality
    public void PlayerTakeDamage(float damage)
    {
        if (!isAlive) { return; }
        if (playerShield > 0)
        {
            float damagetoSubtract = playerShield;
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
        if(playerHealth <= 0)
        {
            Die();
        }
    }
    public void PlayerHeal(float health)
    {
        if (!isAlive || playerHealth == maxHealth) { return; }
        playerHealth += health;
        if (playerHealth > maxHealth && !extraHealthActive)
        {
            playerHealth = maxHealth;
        }
    }
    public void SetMaxHealth(float newMaxHealth)
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
    public void SetMaxShield(float newMaxShield)
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
    public void addItemBuffs(Item item)
    {
        SetMaxHealth(maxHealth += item.healthBonus);
        SetMaxShield(maxShield += item.sheildBonus);
    }

    public void removeItemBuffs(Item item)
    {
        SetMaxHealth(maxHealth -= item.healthBonus);
        SetMaxShield(maxShield -= item.sheildBonus);
    }

    public void Die()
    {
        isAlive = false;
        if(transform.parent.parent.GetComponentInChildren<GunSystem>() != null)
        {
            transform.parent.parent.GetComponentInChildren<GunSystem>().active = false;
        }
        else if(transform.parent.parent.GetComponentInChildren<MeleeSystem>() != null)
        {
           transform.parent.parent.GetComponentInChildren<MeleeSystem>().active = false;
        }

        Cursor.lockState = CursorLockMode.None;
        transform.parent.parent.GetComponentInChildren<CinemachineBrain>().enabled = false;
        gameObject.transform.parent.parent.GetComponent<Inventory>().canUse = false;
        transform.parent.GetComponent<FirstPersonController>().MoveSpeed = 0;
        transform.parent.GetComponent<FirstPersonController>().SprintSpeed = 0;
        transform.parent.GetComponent<FirstPersonController>().JumpHeight = 0;
        playerHealth = 0;
        deathCanvas.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
