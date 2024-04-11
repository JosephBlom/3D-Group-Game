using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] float chaseDistance;
    Vector3 home;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
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
}
