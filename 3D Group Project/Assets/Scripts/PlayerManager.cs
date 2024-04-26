using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerCast;
    [SerializeField] GameObject map;

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
        if (Input.GetKeyUp(KeyCode.M))
        {
            Debug.Log("here");
            if (map.active)
            {
                Debug.Log("off");
                toggleMap(false);
            }
            else
            {
                Debug.Log("on");
                toggleMap(true);
            }
        }
        
    }

    public void mouseLock(bool enable)
    {
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;
        GetComponentInChildren<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }

    public void toggleMap(bool open)
    {
        map.SetActive(open);
    }
}
