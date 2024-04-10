using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShooting : MonoBehaviour
{
    [Header("General Weapon Settings")]
    [SerializeField] private float weaponCooldown = 0.5f;
    [Min(0), SerializeField] private float rangedDamage = 0.5f;
    [Min(0.1f), SerializeField] private float gunRange = 5f;
    [Min(1), SerializeField] private int bulletCount = 1;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject firepoint;

    [Header("Weapon Ammo Settings")]
    [SerializeField] private int ammoType = 1;
    [Min(0), SerializeField] private int ammoConsumed = 1;
    [Min(0), SerializeField] private int magazineSize = 30;

    [Header("Debug Settings")]
    [SerializeField] private bool debug = true;

    private bool canAttack = true;
    private int ammoCount;

    private void Awake()
    {
        ammoCount = magazineSize;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && ammoCount < magazineSize)
        {
            ammoCount = magazineSize;
            Debug.Log("Reloaded. new ammo is " + ammoCount);
        }
    }
    private void Shoot()
    {
        if (!canAttack || ammoCount <= 0)
        {
            return;
        }
        IEnumerator cooldown = WeaponCooldown(weaponCooldown);
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject realBullet = Instantiate(bullet, firepoint.transform.position + new Vector3((Random.Range(0, 0.1f)), (Random.Range(0, 0.1f)), (Random.Range(0, 0.1f))), Camera.main.transform.rotation);
            realBullet.GetComponent<ProjectileBehavior>().Fire(gunRange, Camera.main.transform.forward);
            Destroy(realBullet, 1);
        }
        StartCoroutine(cooldown);
        ammoCount -= ammoConsumed;
        Debug.Log(ammoCount);
    }
    private IEnumerator WeaponCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}