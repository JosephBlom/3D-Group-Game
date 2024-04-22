using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GunSystem : MonoBehaviour
{
    [Header("Weapon Variants")]
    [SerializeField] private bool automatic = true;

    [Header("Weapon Variant Settings")]
    [SerializeField] private GameObject bullet;
    [Min(1), SerializeField] private int bulletCount = 1;

    [Header("General Weapon Settings")]
    [Min(0), SerializeField] private float weaponInaccuracy = 0.1f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float reloadSpeed = 0.5f;
    [Min(0), SerializeField] private float rangedDamage = 0.5f;
    [Min(0.1f), SerializeField] private float gunRange = 5f;

    [Header("Weapon Ammo Settings")]
    [SerializeField] private int ammoType = 1;
    [Min(0), SerializeField] private int ammoConsumed = 1;
    [Min(0), SerializeField] public int maxAmmoSize = 30;

    [Header("Debug Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private GameObject firepoint;
    [SerializeField] private PlayerAmmoManager playerAmmoManager;

    private bool canAttack = true;
    private bool reloading = false;
    public int ammoCount;

    private void Awake()
    {
        if (transform.parent == Camera.main.transform)
        {
            playerAmmoManager = Camera.main.transform.parent.GetComponentInChildren<PlayerAmmoManager>();
        }
        ammoCount = maxAmmoSize;
    }
    private void Update()
    {
        if (transform.parent != Camera.main.transform)
        {
            Debug.Log("ploogy");
            return;
        }

        if (Input.GetButton("Fire1") && automatic)
        {
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && !automatic)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && ammoCount < maxAmmoSize)
        {
            if (playerAmmoManager.FindAmmoType(ammoType) <= 0)
            {
                Debug.Log(playerAmmoManager.FindAmmoType(ammoType));
                Debug.Log("not enough ammo");
                return;
            }
            if (canAttack)
            {
                IEnumerator reload = Reload(reloadSpeed);
                StopCoroutine(reload);
                StartCoroutine(reload);
            }
        }
    }
    private void Shoot()
    {
        if (!canAttack || ammoCount <= 0 || reloading || playerAmmoManager == null)
        {
            return;
        }
        IEnumerator cooldown = WeaponCooldown(fireRate);

        for (int x = 0; x < bulletCount; x++)
        {
            GameObject realBullet = Instantiate(bullet, firepoint.transform.position + new Vector3((Random.Range(0, weaponInaccuracy)), (Random.Range(0, weaponInaccuracy)), (Random.Range(0, weaponInaccuracy))), Camera.main.transform.rotation);
            realBullet.GetComponent<ProjectileBehavior>().Fire(gunRange, Camera.main.transform.forward);
        }
        StartCoroutine(cooldown);
        ammoCount -= ammoConsumed;
    }
    private IEnumerator WeaponCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
    private IEnumerator Reload(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        if(ammoCount > 0)
        {
            int ammoAdded = ammoCount;
            ammoCount += playerAmmoManager.FindAmmoType(ammoType);
            if(ammoCount > maxAmmoSize) {ammoCount = maxAmmoSize;}
            ammoAdded = Mathf.Abs(ammoAdded - ammoCount);
            playerAmmoManager.RemoveAmmo(ammoAdded, ammoType);
        }
        else
        {
            ammoCount += playerAmmoManager.FindAmmoType(ammoType);
            if (ammoCount > maxAmmoSize) { ammoCount = maxAmmoSize; }
            playerAmmoManager.RemoveAmmo(ammoCount, ammoType);
        }
        Debug.Log("Reloaded. new ammo is " + ammoCount);
        canAttack = true;
    }
}