using System.Collections;
using TMPro;
using UnityEngine;

public class PassiveNavigation : MonoBehaviour
{
    EnemyHealthSystem _enemyHealthSystem;
    private bool isWandering = false;
    private bool isWalking = false;

    private int timesinceLastAttack = 10;

    private void Awake()
    {
        _enemyHealthSystem = GetComponent<EnemyHealthSystem>();
    }

    void Update()
    {
        CheckHealth();
        if(!isWandering && timesinceLastAttack >= 10)
        {
            Wander();
        }
        else if (timesinceLastAttack < 10)
        {
            Panic();
        }
    }

    Vector3 wanderTarget = Vector3.zero;
    private void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;
    }
    private void Panic()
    {

    }
    private void CheckHealth()
    {
        if(_enemyHealthSystem.enemyHealth < 100)
        {
            timesinceLastAttack = 0;
        }
    }

}