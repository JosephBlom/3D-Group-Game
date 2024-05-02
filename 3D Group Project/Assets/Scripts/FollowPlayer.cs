using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Tooltip("This must be the player object (The one that moves!!!).")]
    [SerializeField] GameObject player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
