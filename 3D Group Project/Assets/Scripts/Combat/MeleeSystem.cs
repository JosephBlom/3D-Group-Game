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

    [Header("General Weapon Settings")]
    [SerializeField] private float swingSpeed = 0.5f;
    [Min(0), SerializeField] private int meleeDamage = 1;
    [Min(0.1f), SerializeField] private float meleeRange = 2.5f;

    [Header("Debug Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private GameObject firepoint;

    private bool canAttack = true;
    [SerializeField] private GameObject meleeDebug;

    private void Awake()
    {

    }
    private void Update()
    {
        if (transform.parent != Camera.main.transform)
        {
            Debug.Log("ploogy");
            return;
        }

        if (Input.GetButton("Fire1") && autoSwing)
        {
            Swing();
        }
        else if (Input.GetButtonDown("Fire1") && !autoSwing)
        {
            Swing();
        }
    }
    private void Swing()
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
        MeleeBehavior projectileBehavior = meleeHitbox.GetComponent<MeleeBehavior>();
        projectileBehavior.shooter = gameObject.transform.parent.parent.name;
        projectileBehavior.damage = meleeDamage;
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