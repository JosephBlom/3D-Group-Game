using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerManager playerManager;
    public Player player;

    public bool hovered;
    public Item heldItem;

    private Color opaque = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);

    public Image thisSlotImage;

    private void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>().GetComponent<PlayerManager>();
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
    }

    public void intialiseSlot()
    {
        thisSlotImage.sprite = heldItem.icon;
        thisSlotImage.color = opaque;
    }

    public Item getItem()
    {
        return heldItem;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(heldItem != null)
        {
            playerManager.currentNPC.GetComponent<ShopManager>().updateMenu(heldItem.description, heldItem.cost, player.gold, heldItem.name);
        }
        hovered = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        playerManager.currentNPC.GetComponent<ShopManager>().updateMenu("", 0, player.gold, "");
        hovered = false;
    }
}
