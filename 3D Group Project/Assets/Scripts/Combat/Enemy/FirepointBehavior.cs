using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirepointBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool active = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(active)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            gameObject.transform.LookAt(targetPos);
        }
    }
}
