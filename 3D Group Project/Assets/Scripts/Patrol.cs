using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public bool chasePlayer;
    public int currentPoint;

    private void Start()
    {
        currentPoint = 0;
    }

    private void Update()
    {
        if (!chasePlayer)
        {
            Vector3 distance = (patrolPoints[currentPoint].position - transform.position);
            if (distance.magnitude >= 0.5)
            {
                Debug.Log(distance.magnitude);
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
            }
            else
            {
                currentPoint++;
                if (currentPoint > patrolPoints.Length -1 )
                    currentPoint = 0;
            }
        }
    }
}
