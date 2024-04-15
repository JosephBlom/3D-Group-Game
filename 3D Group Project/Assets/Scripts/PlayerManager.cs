using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerCast;

    public bool inMenu = false;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.F))
        {
            Physics.Raycast(playerCast.position, transform.TransformDirection(Vector3.forward), out hit, 3);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                inMenu = true;
            }
            
        }
    }
}
