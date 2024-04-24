using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyNav : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] float chaseDistance;
    [SerializeField] GameObject player;

    [Header("Melee Attack Settings")]
    [SerializeField] private bool meleeEnabled = true;
    [SerializeField] private float meleeRange = 2.5f;
    [SerializeField] private float meleeCooldown = 0.5f;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] GameObject meleeDebug;
    [SerializeField] GameObject meleeFirepoint;

    private bool canAttack = true;
    NavMeshAgent agent;
    Vector3 home;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(meleeEnabled)
        {
            MeleeAttack();
        }
        Vector3 moveDirection = player.transform.position - transform.position;
        if (moveDirection.magnitude < chaseDistance)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            agent.destination = home;
        }
    }
    private void MeleeAttack()
    {
        Vector3 meleeRadius = player.transform.position - transform.position;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        IEnumerator cooldown = MeleeCooldown(meleeCooldown);

        if (meleeRadius.magnitude <= meleeRange)
        {
            if(!canAttack) { return; }
            gameObject.transform.LookAt(targetPos);
            agent.isStopped = true;
            GameObject meleeHitbox = Instantiate(meleeDebug, meleeFirepoint.transform.position, Quaternion.identity);
            meleeHitbox.transform.localScale = new Vector3(meleeRange, meleeRange, meleeRange);
            meleeHitbox.GetComponent<MeleeBehavior>().damage = meleeDamage;
            meleeHitbox.GetComponent<MeleeBehavior>().enemyFriendly = true;
            meleeHitbox.GetComponent<MeleeBehavior>().friendly = false;
            Destroy(meleeHitbox, 1);
            StartCoroutine(cooldown);
        }
        else
        {
            agent.isStopped = false;
        }
    }
    private IEnumerator MeleeCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        StopAllCoroutines();
    }

}
