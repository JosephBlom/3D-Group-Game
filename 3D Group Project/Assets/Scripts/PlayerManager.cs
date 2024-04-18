using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerCast;

    public Quest quest;
    public GameObject currentNPC;

    void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.F))
        {
            Physics.Raycast(playerCast.position, transform.TransformDirection(Vector3.forward), out hit, 3);
            if (hit.collider != null)
            {
                currentNPC = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                mouseLock(true);
            }
            
        }
        
    }

    public void mouseLock(bool enable)
    {
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;
        GetComponentInChildren<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }
}
