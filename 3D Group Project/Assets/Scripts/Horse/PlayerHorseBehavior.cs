using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorseBehavior : MonoBehaviour
{
    [SerializeField] private bool onMount = false;
    PlayerHealthSystem healthSystem;

    public void Awake()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();
    }

    private void Update()
    {
        if(healthSystem == null)
        {
            Debug.LogError("No health system found! Will not run :3");
            return;
        }
        if(Input.GetKeyDown(KeyCode.E) && !onMount && healthSystem.isAlive)
        {
            HorseRaycast();
        }
        if (Input.GetKeyDown(KeyCode.X) && onMount)
        {
            if (gameObject.transform.parent.GetComponentInChildren<HorseMountScript>() != null)
            {
                gameObject.transform.parent.GetComponentInChildren<HorseMountScript>().UnmountHorse();
            }
            onMount = false;
        }
    }
    private void HorseRaycast()
    {
        Ray shootDirection = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(shootDirection, out RaycastHit hit, 3))
        {
            if (hit.collider.gameObject.GetComponentInParent<HorseMountScript>() != null)
            {
                hit.collider.gameObject.GetComponentInParent<HorseMountScript>().player = gameObject;
                hit.collider.gameObject.GetComponentInParent<HorseMountScript>().MountHorse();
                onMount = true;
            }
        }
    }

}
