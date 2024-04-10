using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShooting : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debug = true;
    [SerializeField] private GameObject firepoint;
    [SerializeField] private GameObject bullet;

    [Header("Weapon Variables")]
    [SerializeField] private bool canAttack = true;

    [Header("Weapon Settings")]
    [SerializeField] private float rangedCooldown = 0.5f;
    [Min(0), SerializeField] private float rangedDamage = 0.5f;
    [Min(0.1f), SerializeField] private float rangedRange = 5f;
    [Min(1), SerializeField] private int numberofShots = 1;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI pickupText;

    private LineRenderer lineRenderer;
    public bool canPickup = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //pickupText.enabled = false;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!canAttack)
        {
            return;
        }
        IEnumerator cooldown = WeaponCooldown(rangedCooldown);
        for (int i = 0; i < numberofShots; i++)
        {
            GameObject realBullet = Instantiate(bullet, firepoint.transform.position + new Vector3((Random.Range(0, 0.1f)), (Random.Range(0, 0.1f)), (Random.Range(0, 0.1f))), Camera.main.transform.rotation);
            realBullet.GetComponent<ProjectileBehavior>().Fire(rangedRange, Camera.main.transform.forward);
            Destroy(realBullet, 1);
        }
        StartCoroutine(cooldown);
    }
    private IEnumerator WeaponCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}