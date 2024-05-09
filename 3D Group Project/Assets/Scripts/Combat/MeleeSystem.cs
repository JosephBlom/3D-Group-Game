using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MeleeSystem : MonoBehaviour
{
    [Header("Weapon Variants")]
    [SerializeField] private bool autoSwing = true;
    [SerializeField] private bool heavyAttack = true;

    [Header("Weapon Variant Settings")]
    [Min(1), SerializeField] private int heavyAttackDamage = 2;
    [SerializeField] private float heavyAttackCooldown = 1;

    [Header("General Weapon Settings")]
    [SerializeField] private float swingSpeed = 0.5f;
    [Min(0), SerializeField] private int meleeDamage = 1;
    [Min(0.1f), SerializeField] private float meleeRange = 2.5f;

    [Header("Debug Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private GameObject firepoint;

    private bool canAttack = true;
    [SerializeField] private GameObject meleeDebug;
    public bool active = true;

    private void Awake()
    {

    }
    private void Update()
    {
        if (transform.parent != Camera.main.transform || !active )
        {
            Debug.Log("ploogy");
            return;
        }
        if (Input.GetButton("Fire2"))
        {
            HeavyAttack();
        }

        if (Input.GetButton("Fire1") && autoSwing)
        {
            Attack();
        }
        else if (Input.GetButtonDown("Fire1") && !autoSwing)
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        if (firepoint.transform.childCount > 0)
        {
            for (int i = 0; i < firepoint.transform.childCount; i++)
            {
                Destroy(firepoint.transform.GetChild(i).gameObject);
            }
        }
        IEnumerator cooldown = WeaponCooldown(swingSpeed);

        GameObject meleeHitbox = Instantiate(meleeDebug, firepoint.transform.position, Camera.main.transform.rotation);
        meleeHitbox.transform.localScale = new Vector3(meleeRange, meleeRange, meleeRange);
        if (transform.parent == Camera.main.transform)
        {
            meleeHitbox.transform.parent = firepoint.transform;
        }
        MeleeBehavior meleeBehavior = meleeHitbox.GetComponent<MeleeBehavior>();
        meleeBehavior.shooter = gameObject.transform.parent.parent.name;
        meleeBehavior.weaponName = gameObject.name;
        meleeBehavior.friendly = true;
        meleeBehavior.damage = meleeDamage;
        Destroy(meleeHitbox, 1);
        StartCoroutine(cooldown);
    }
    private void HeavyAttack()
    {
        if (!canAttack || !heavyAttack)
        {
            return;
        }
        if (firepoint.transform.childCount > 0)
        {
            for (int i = 0; i < firepoint.transform.childCount; i++)
            {
                Destroy(firepoint.transform.GetChild(i).gameObject);
            }
        }
        IEnumerator cooldown = WeaponCooldown(heavyAttackCooldown);

        GameObject meleeHitbox = Instantiate(meleeDebug, firepoint.transform.position, Camera.main.transform.rotation);
        meleeHitbox.transform.localScale = new Vector3(meleeRange, meleeRange, meleeRange);
        if (transform.parent == Camera.main.transform)
        {
            meleeHitbox.transform.parent = firepoint.transform;
        }
        MeleeBehavior meleeBehavior = meleeHitbox.GetComponent<MeleeBehavior>();
        meleeBehavior.shooter = gameObject.transform.parent.parent.name;
        meleeBehavior.weaponName = gameObject.name;
        meleeBehavior.friendly = true;
        meleeBehavior.damage = heavyAttackDamage;
        Destroy(meleeHitbox, 1);
        StartCoroutine(cooldown);
    }
    private IEnumerator WeaponCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}