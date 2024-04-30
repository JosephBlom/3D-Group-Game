using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseMountScript : MonoBehaviour
{
    [SerializeField] GameObject playerMountPoint;
    HorseCollision horseCollision;
    EnemyHealthSystem enemyHealthSystem;
    PassiveNavigation passivenav;
    public GameObject player;

    private void Start()
    {
        horseCollision = GetComponentInChildren<HorseCollision>();
        enemyHealthSystem = GetComponent<EnemyHealthSystem>();
        passivenav = GetComponent<PassiveNavigation>();
        player = GetComponentInChildren<HorseCollision>().player;
    }

    private void Update()
    {
        Debug.Log(player);
    }
    public void MountHorse()
    {
        if (player != null)
        {
            player.transform.parent.transform.position = playerMountPoint.transform.position + new Vector3(0, 0, 1);
            player.transform.parent.Find("PlayerHorse").gameObject.SetActive(true);
            player.transform.parent.GetComponent<CharacterController>().height = 5;
            gameObject.Find("").SetActive(false);
        }
    }
}
