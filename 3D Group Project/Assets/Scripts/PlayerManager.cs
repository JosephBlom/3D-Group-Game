using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerCast;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.F))
        {
            Physics.Raycast(playerCast.position, transform.TransformDirection(Vector3.forward), out hit, 3);
            hit.collider.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
