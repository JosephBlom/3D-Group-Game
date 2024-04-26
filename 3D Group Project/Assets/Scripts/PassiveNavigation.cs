using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PassiveNavigation : MonoBehaviour
{
    EnemyHealthSystem enemyHealthSystem;
    NavMeshAgent agent;

    [SerializeField] private float fleeDistance = 5;
    [SerializeField] private float generationDelay = 0.5f;
    [SerializeField] private float panicTime = 10;

    [SerializeField] private GameObject player;
    private bool isWandering = false;
    private bool isWalking = false;
    private bool isPanic = false;
    private string killerName = "Herobrine";
    private string weaponName = "gun";

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isPanic = false;
        enemyHealthSystem = GetComponent<EnemyHealthSystem>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckHealth();
        if(!isWandering || !isPanic)
        {
            StartCoroutine(Wander());
        }
    }

    private void Seek(Vector3 location)
    {
        agent.destination = location;
    }
    public Vector3 GetCharacterPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        bool positionFound = NavMesh.SamplePosition(position, out hit, 500, NavMesh.AllAreas);

        if (!positionFound)
            Debug.LogWarning("No valid position found !");

        return positionFound ? hit.position : position;
    }

    Vector3 wanderTarget = Vector3.zero;
    private IEnumerator Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 5;
        float wanderJitter = 2;

        wanderTarget += new Vector3(Random.Range(-0.65f, 1.0f) * wanderJitter, 0, Random.Range(-0.65f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        isWandering = true;
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        GetCharacterPositionOnNavMesh(targetWorld);
        Seek(targetWorld);
        yield return new WaitForSeconds(0.5f);
        isWandering = false;
        StopAllCoroutines();
    }
    private IEnumerator Panic()
    {
        Vector3 fleeDirection = player.transform.position - transform.position;
        if (fleeDirection.magnitude < fleeDistance)
        {
            Seek(-player.transform.position);
        }
        yield return new WaitForSeconds(0);
        StopAllCoroutines();
    }
    private void CheckHealth()
    {
        if (enemyHealthSystem.enemyHealth < 100)
        {
            isPanic = true;
        }
        else
        {
            isPanic = false;
            StopAllCoroutines();
        }
        if(isPanic)
        {
            StartCoroutine(Panic());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponent<ProjectileBehavior>() != null && !collision.gameObject.GetComponent<ProjectileBehavior>().enemyFriendly)
            {
                enemyHealthSystem.EnemyDamage(collision.gameObject.GetComponent<ProjectileBehavior>().damage);
                weaponName = collision.gameObject.GetComponent<ProjectileBehavior>().weaponName;
                killerName = collision.gameObject.GetComponent<ProjectileBehavior>().shooter;
            }
        }
    }


}