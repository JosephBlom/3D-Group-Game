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
    }
    private void Update()
    {
        if(player != null && gameObject.transform.parent == player.transform.parent.transform)
        {
            StartCoroutine(LocationFix());
        }
    }

    public void MountHorse()
    {
        if (player != null)
        {
            passivenav.GetComponent<NavMeshAgent>().ResetPath();
            player.transform.parent.transform.position = playerMountPoint.transform.position + new Vector3(0, 0, 1);
            player.transform.parent.parent.GetComponent<Player>().horse.gameObject.SetActive(true);
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
        player.transform.parent.parent.GetComponent<Player>().horse.gameObject.SetActive(false);
        player.transform.parent.GetComponent<CharacterController>().height = player.GetComponent<PlayerHealthSystem>().playerHeight;
        player.transform.parent.GetComponent<FirstPersonController>().MoveSpeed = player.GetComponent<PlayerHealthSystem>().playerMoveSpeed;
        player.transform.parent.GetComponent<FirstPersonController>().GroundedOffset = player.GetComponent<PlayerHealthSystem>().playerOffset;
        gameObject.transform.parent = null;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    private IEnumerator LocationFix()
    {
        bool location = true;
        yield return new WaitForSecondsRealtime(1);
        location = false;

        if(!location)
        {
            if (gameObject.transform.parent == player.transform.parent.transform)
            {
                gameObject.transform.position = player.transform.position;
                gameObject.transform.rotation = player.transform.rotation;
            }
        }
        StopAllCoroutines();
    }

}
