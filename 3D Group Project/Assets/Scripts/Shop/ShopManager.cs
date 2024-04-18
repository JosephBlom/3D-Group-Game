using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Canvas Objects")]
    public Canvas shopCanvas;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCost;
    public TextMeshProUGUI playerGold;

    public Shop shop;

    private bool hovered;

    public void openShop()
    {
        shopCanvas.enabled = true;
        npcNameText.text = shop.npcName;
    }
    public void updateMenu(string itemDescription, int itemCost, int playerGold)
    {
        this.itemDescription.text = itemDescription;
        this.itemCost.text = itemCost.ToString();
        this.playerGold.text = playerGold.ToString();
    }
    public void purchaseItem(int itemCost, int playerGold)
    {
        this.playerGold.text = (itemCost - playerGold).ToString();
    }
}
