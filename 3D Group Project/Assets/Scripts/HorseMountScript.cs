using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class HorseMountScript : MonoBehaviour
{
    [SerializeField] GameObject playerMountPoint;
    HorseCollision horseCollision;
    EnemyHealthSystem enemyHealthSystem;
    PassiveNavigation passivenav;
    public GameObject player;
    
    private float playerHeight;
    private float playerMovespeed;
    private float playerOffset;

    private void Start()
    {
        horseCollision = GetComponentInChildren<HorseCollision>();
        enemyHealthSystem = GetComponent<EnemyHealthSystem>();
        passivenav = GetComponent<PassiveNavigation>();
        player = GetComponentInChildren<HorseCollision>().player;
    }
    public void MountHorse()
    {
        if (player != null)
        {
            player.transform.parent.transform.position = playerMountPoint.transform.position + new Vector3(0, 0, 1);
            player.transform.parent.Find("PlayerHorse").gameObject.SetActive(true);
            playerHeight = player.transform.parent.GetComponent<CharacterController>().height;
            playerMovespeed = player.transform.parent.GetComponent<FirstPersonController>().MoveSpeed;
            playerOffset = player.transform.parent.GetComponent<FirstPersonController>().GroundedOffset;
            player.transform.parent.GetComponent<CharacterController>().height = 5;
            player.transform.parent.GetComponent<FirstPersonController>().MoveSpeed = 10;
            player.transform.parent.GetComponent<FirstPersonController>().GroundedOffset = 1.65f;
            gameObject.transform.parent = player.transform.parent.transform;
            foreach(Transform child in transform)
            {
               child.gameObject.SetActive(false);
            }
        }
    }

    public void UnmountHorse()
    {
        player.transform.parent.Find("PlayerHorse").gameObject.SetActive(false);
        player.transform.parent.GetComponent<CharacterController>().height = playerHeight;
        player.transform.parent.GetComponent<FirstPersonController>().MoveSpeed = playerMovespeed;
        player.transform.parent.GetComponent<FirstPersonController>().GroundedOffset = playerOffset;
        gameObject.transform.parent = null;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
