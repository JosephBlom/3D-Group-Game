using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyNav : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] float chaseDistance;
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    [SerializeField] bool patrolling;

    [Header("Enemy Settings")]
    [SerializeField] private bool meleeEnabled = true;
    [SerializeField] private bool rangedEnabled = false;

    [Header("Melee Attack Settings")]
    [SerializeField] private float meleeRange = 2.5f;
    [SerializeField] private float meleeCooldown = 0.5f;
    [SerializeField] private float meleeDelay = 0.5f;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] GameObject meleeDebug;
    [SerializeField] GameObject meleeFirepoint;

    [Header("Ranged Attack Settings")]
    [SerializeField] private int rangedDamage = 10;
    [SerializeField] private int explosionDamage = 10;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private int bulletsToShoot = 1;
    [SerializeField] private float bulletSpeed = 2.5f;
    [SerializeField] private float bulletInaccuracy = 2.5f;
    [SerializeField] private float rangedAggroDistance = 2.5f;
    [SerializeField] private float rangedFirerate = 0.5f;
    [SerializeField] private float rangedCooldown = 0.5f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rangedFirepoint;

    private bool canAttack = true;
    private int secretAmmo;
    NavMeshAgent agent;
    Vector3 home;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerObject");
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
        secretAmmo = ammoCount;
    }
    private void Update()
    {
        if(meleeEnabled)
        {
            StartCoroutine(MeleeAttack());
        }
        else if (rangedEnabled)
        {
            RangedAttack();
        }
        Vector3 moveDirection = player.transform.position - transform.position;
        if (moveDirection.magnitude < chaseDistance)
        {
            agent.destination = player.transform.position;
            if (patrolling)
            {
                GetComponent<Patrol>().chasePlayer = true;
                animator.SetBool("Patrol", false);
            }
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);

        }
        else
        {
            agent.destination = home;
            if (patrolling)
            {
                GetComponent<Patrol>().chasePlayer = false;
                animator.SetBool("Patrol", true);
                animator.SetBool("Run", false);
            }
               

        }
    }
    private IEnumerator MeleeAttack()
    {
        Vector3 meleeRadius = player.transform.position - transform.position;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        IEnumerator cooldown = MeleeCooldown(meleeCooldown);

        if (meleeRadius.magnitude <= meleeRange && canAttack)
        {
            gameObject.transform.LookAt(targetPos);
            agent.isStopped = true;
            yield return new WaitForSeconds(meleeDelay);
            animator.SetBool("Attack", true);
            GameObject meleeHitbox = Instantiate(meleeDebug, meleeFirepoint.transform.position, Quaternion.identity);
            meleeHitbox.transform.localScale = new Vector3(meleeRange, meleeRange, meleeRange);
            meleeHitbox.transform.parent = meleeFirepoint.transform;
            meleeHitbox.GetComponent<MeleeBehavior>().damage = meleeDamage;
            meleeHitbox.GetComponent<MeleeBehavior>().enemyFriendly = true;
            meleeHitbox.GetComponent<MeleeBehavior>().friendly = false;
            Destroy(meleeHitbox, 1);
            StopAllCoroutines();
            StartCoroutine(cooldown);
        }
        else
        {
            animator.SetBool("Attack", false);
            agent.isStopped = false;
        }
    }
    private void RangedAttack()
    {
        Vector3 rangedRadius = player.transform.position - transform.position;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y + 1, player.transform.position.z);
        IEnumerator cooldown = MeleeCooldown(rangedFirerate);
        IEnumerator reloadCooldown = ReloadTime(rangedCooldown);

        if (rangedRadius.magnitude <= rangedAggroDistance && canAttack && secretAmmo != 0)
        {
            gameObject.transform.LookAt(targetPos);
            rangedFirepoint.GetComponent<FirepointBehavior>().active = true;
            agent.isStopped = true;
            for (int x = 0; x < bulletsToShoot; x++)
            {
                GameObject realBullet = Instantiate(bullet, rangedFirepoint.transform.position + new Vector3((Random.Range(-bulletInaccuracy, bulletInaccuracy)), (Random.Range(-bulletInaccuracy, bulletInaccuracy)), (Random.Range(-bulletInaccuracy, bulletInaccuracy))), Quaternion.identity);
                realBullet.GetComponent<ProjectileBehavior>().Fire(bulletSpeed, rangedFirepoint.transform.forward);
                ProjectileBehavior projectileBehavior = realBullet.GetComponent<ProjectileBehavior>();
                projectileBehavior.shooter = gameObject.name;
                projectileBehavior.enemyFriendly = true;
                projectileBehavior.passiveFriendly = false;
                projectileBehavior.damage = rangedDamage;
                projectileBehavior.explosionDamage = explosionDamage;
            }
            StartCoroutine(cooldown);
            secretAmmo -= bulletsToShoot;
        }
        else
        {
            if(secretAmmo == 0)
            {
                StartCoroutine(reloadCooldown);
            }
            agent.isStopped = false;
            rangedFirepoint.GetComponent<FirepointBehavior>().active = false;
        }
    }

    private IEnumerator MeleeCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        StopAllCoroutines();
    }
    private IEnumerator ReloadTime(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        secretAmmo = ammoCount;
        StopAllCoroutines();
    }

}
