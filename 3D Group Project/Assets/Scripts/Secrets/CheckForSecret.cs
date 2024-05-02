using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckForSecret : MonoBehaviour
{
    [Header("Raycast")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;
    public Image crosshair;
    public TMP_Text itemHoverText;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void itemRaycast(bool hasClicked = false)
    {
        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
        {
            if (hit.collider != null)
            {
                if (hasClicked) //pick up
                {
                    Secret newSecret = hit.collider.GetComponent<Secret>();
                    if (newSecret)
                    {
                        player.foundSecrets.Add(newSecret);
                        player.foundSecretsNames.Add(newSecret.name);
                    }
                }
                else //show name
                {
                    Item newItem = hit.collider.GetComponent<Item>();

                    if (newItem)
                    {
                        itemHoverText.text = newItem.name;
                    }
                }
            }
        }
    }
}
